using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace eCommerce.BusinessLogic
{
    public class PasswordManagerService : BaseService
    {
        public PasswordManagerService(UnitOfWork uow)
            :base(uow)
        {

        }

        public byte[] HashPassword(string inputPassword, ref string result)
        {
            byte[] salt = new byte[16];
            using (var randomNumber = RandomNumberGenerator.Create())
            {
                randomNumber.GetBytes(salt);
            }

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: inputPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256/8
                ));

            result = hashedPassword;

            return salt;
        }

        public byte[] UpdatePasswordHash(string inputPassword, byte[] salt, ref string result)
        {

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: inputPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256/8
                ));

            result = hashedPassword;

            return salt;
        }

        public bool Match(string password, string test, byte[] salts)
        {
            password = password.Trim();
            test = test.Trim();

            return HashWithSalts(password, salts) == test;
        }

        private string HashWithSalts(string password, byte[] salts)
        {
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salts,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256/8
                ));

            return hashedPassword;
        }
    }
}
