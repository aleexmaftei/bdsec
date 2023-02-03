using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using eCommerce.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace eCommerce.BusinessLogic
{
    public class UserAccountService : BaseService
    {
        private readonly DeliveryLocationService DeliveryLocationService;
        private readonly UserService UserService;
        private readonly PasswordManagerService PasswordManagerService;
        private readonly CurrentUserDto CurrentUserDto;

        public UserAccountService(UnitOfWork uow,
                                  DeliveryLocationService deliveryLocationService,
                                  UserService userService,
                                  PasswordManagerService passwordManagerService,
                                  CurrentUserDto currentUserDto)
            : base(uow)
        {
            DeliveryLocationService = deliveryLocationService;
            UserService = userService;
            PasswordManagerService = passwordManagerService;
            CurrentUserDto = currentUserDto;
        }

        private readonly static byte[] Key = Convert.FromBase64String("YXJhZ2F6Q3VCdXRlbGllZQ==");
        private readonly static byte[] IV = Convert.FromBase64String("YXJhZ2F6Q3hCdXRlbGllZQ==");

        private static string EncryptWithAes(string plainText)
        {
            using Aes algorithm = Aes.Create();

            algorithm.KeySize = 128;
            algorithm.Key = Key;
            algorithm.IV = IV;

            ICryptoTransform encryptor = algorithm.CreateEncryptor();

            byte[] encryptedData;

            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(plainText);
            }
            
            encryptedData = memoryStream.ToArray();

            return Convert.ToBase64String(encryptedData);
        }

        private static string DecryptAes(string encryptedText)
        {
            using Aes algorithm = Aes.Create();

            algorithm.KeySize = 128;
            algorithm.Key = Key;
            algorithm.IV = IV;

            ICryptoTransform decryptor = algorithm.CreateDecryptor();

            byte[] cipher = Convert.FromBase64String(encryptedText);

            using MemoryStream memoryStream = new MemoryStream(cipher);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }

        public UserT Login(string email, string password)
        {
            var userCheckLogin = UnitOfWork.Users.Get()
                        .Include(u => u.UserRole)
                            .ThenInclude(ur => ur.Role)
                        .FirstOrDefault(a => a.Email == email);

            if (userCheckLogin == null)
            {
                return null;
            }

            var saltString = UnitOfWork.Salts.Get()
                        .FirstOrDefault(salt => salt.UserId == userCheckLogin.UserId).SaltPass;
            var actualSalt = Convert.FromBase64String(DecryptAes(saltString.Trim()));

            if (PasswordManagerService.Match(password, userCheckLogin.PasswordHash, actualSalt))
            {
                return userCheckLogin;
            }

            return null;
        }

        public UserT RegisterNewUser(UserT user, DeliveryLocation deliveryLocation)
        {

            return ExecuteInTransaction(uow =>
            {
                string result = string.Empty;
                var salt = PasswordManagerService.HashPassword(user.PasswordHash, ref result);

                salt = Convert.FromBase64String(EncryptWithAes(Convert.ToBase64String(salt)));
                user.PasswordHash = result;

                var roleId = (int)RoleTypes.User;
                if (CurrentUserDto?.IsAdmin == true && CurrentUserDto != null)
                {
                    roleId = (int)RoleTypes.Admin;
                }

                user.UserRole = new List<UserRole>
                {
                    new UserRole
                    {
                        RoleId = roleId
                    }
                };

                uow.Users.Insert(user);

                var userSalt = new Salt()
                {
                    User = user,
                    SaltPass = Convert.ToBase64String(salt)
                };

                uow.Salts.Insert(userSalt);

                deliveryLocation.User = user;
                uow.DeliveryLocations.Insert(deliveryLocation);

                uow.SaveChanges();
                return user;
            });
        }

        public UserT DeleteUser()
        {
            return ExecuteInTransaction(uow =>
            {
                var userToDelete = UserService.GetCurrentUser();
                if (userToDelete == null)
                {
                    return userToDelete;
                }

                uow.Users.Delete(userToDelete);
                uow.SaveChanges();

                return userToDelete;
            });
        }

        public bool UpdateUserPassword(string newPassword, string oldPassword)
        {
            if (newPassword == null || oldPassword == null)
            {
                return false;
            }

            return ExecuteInTransaction(uow =>
            {
                var userToUpdate = UserService.GetCurrentUser();

                if (userToUpdate == null)
                {
                    return false;
                }

                var saltString = UnitOfWork.Salts.Get()
                                .FirstOrDefault(salt => salt.UserId == userToUpdate.UserId).SaltPass;

                var oldPassActualSalt = Convert.FromBase64String(DecryptAes(saltString.Trim()));

                if (PasswordManagerService.Match(oldPassword, userToUpdate.PasswordHash, oldPassActualSalt))
                {
                    string newPasswordHash = string.Empty;
                    PasswordManagerService.UpdatePasswordHash(newPassword, oldPassActualSalt, ref newPasswordHash);

                    userToUpdate.PasswordHash = DecryptAes(newPasswordHash.Trim());
                    uow.Users.Update(userToUpdate);
                    uow.SaveChanges();

                    return true;
                }

                return false;
            });
        }
    }
}
