using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.BusinessLogic
{
    public class UserInvoiceService : BaseService
    {
        private readonly CurrentUserDto CurrentUserDto;

        public UserInvoiceService(UnitOfWork uow,
                                  CurrentUserDto currentUserDto)
            :base(uow)
        {
            CurrentUserDto = currentUserDto;
        }

        public int GetNumberOfInvoices()
        {
            return UnitOfWork.UserInvoices.Get()
                .Select(slct => slct.UserInvoiceId)
                .Distinct()
                .Count();
        }

        public UserInvoice LastInvoiceEmmitedForCurrentUser()
        {
            return UnitOfWork.UserInvoices.Get()
                .Include(cnd => cnd.DeliveryLocation)
                .OrderByDescending(ordb => ordb.DateBuy)
                .FirstOrDefault(cnd => cnd.DeliveryLocation.UserId == Int32.Parse(CurrentUserDto.Id));
        }

        public IEnumerable<UserInvoice> GetInvoices()
        {
            return UnitOfWork.UserInvoices.Get()
                .Include(prd => prd.Product)
                .Include(dl => dl.DeliveryLocation)
                    .ThenInclude(user => user.User)
                .Where(cnd => cnd.DeliveryLocation.User.UserId == Int32.Parse(CurrentUserDto.Id))
                .Take(10)
                .OrderByDescending(cnd => cnd.UserInvoiceId)
                .ToList();
        }

        public IEnumerable<UserInvoice> GetInvoicesById(int userInvoiceId)
        {
            return UnitOfWork.UserInvoices.Get()
                .Include(prd => prd.Product)
                .Include(dl => dl.DeliveryLocation)
                    .ThenInclude(user => user.User)
                .Where(cnd => cnd.DeliveryLocation.User.UserId == Int32.Parse(CurrentUserDto.Id) &&
                              cnd.UserInvoiceId == userInvoiceId)
                .Take(10)
                .OrderByDescending(cnd => cnd.UserInvoiceId)
                .ToList();
                
        }
    }
}
