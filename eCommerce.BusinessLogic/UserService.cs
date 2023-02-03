using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using System;
using System.Linq;

namespace eCommerce.BusinessLogic
{
    public class UserService : BaseService
    {
        private readonly CurrentUserDto CurrentUserDto;

        public UserService(UnitOfWork uow,
            CurrentUserDto currentUserDto) 
            : base(uow)
        {
            CurrentUserDto = currentUserDto;
        }

        public UserT GetUserByUserId(int userId)
        {
            return UnitOfWork.Users.Get()
                .FirstOrDefault(x => x.UserId == userId);
        }

        public UserT GetCurrentUser()
        {
            return UnitOfWork.Users.Get()
                .FirstOrDefault(x => x.UserId == Int32.Parse(CurrentUserDto.Id));
        }
    }
}
