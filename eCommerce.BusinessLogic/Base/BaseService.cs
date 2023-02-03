using eCommerce.Data;
using eCommerce.Entities.DTOs;
using eCommerce.Entities.DTOs.AdminDtos;
using System;
using System.Transactions;

namespace eCommerce.BusinessLogic.Base
{
    public class BaseService
    {
        protected readonly UnitOfWork UnitOfWork;
        protected readonly CurrentUserDto CurrentUser;
        protected readonly ProductDto ProductDto;

        public BaseService(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected TResult ExecuteInTransaction<TResult>(Func<UnitOfWork, TResult> func)
        {
            using (var transactionScope = new TransactionScope())
            {
                var result = func(UnitOfWork);

                transactionScope.Complete();

                return result;
            }
        }

        
    }
}
