using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eCommerce.BusinessLogic
{
    public class DeliveryLocationService : BaseService
    {
        private readonly CurrentUserDto CurrentUserDto;
        private readonly UserService UserService;
        public DeliveryLocationService(UnitOfWork uow,
                                       CurrentUserDto currentUserCartDto,
                                       UserService userService)
            : base(uow)
        {
            CurrentUserDto = currentUserCartDto;
            UserService = userService;
        }

        public IEnumerable<DeliveryLocation> GetDeliveryLocationsCurrentUser()
        {
            return UnitOfWork.DeliveryLocations.Get()
                .Include(user => user.User)
                .Where(cnd => cnd.UserId == Int32.Parse(CurrentUserDto.Id))
                .ToList();
        }

        public DeliveryLocation GetDeliveryLocationById(int deliveryLocation)
        {
            return UnitOfWork.DeliveryLocations.Get()
                .Include(user => user.User)
                .FirstOrDefault(cnd => cnd.DeliveryLocationId == deliveryLocation && 
                                       cnd.UserId == Int32.Parse(CurrentUserDto.Id));
        }
        
        public DeliveryLocation InsertDeliveryLocation(DeliveryLocation deliveryLocation)
        {
            if (deliveryLocation == null)
            {
                return deliveryLocation;
            }

            return ExecuteInTransaction(uow =>
            {
                deliveryLocation.UserId = Int32.Parse(CurrentUserDto.Id);
                deliveryLocation.User = UserService.GetCurrentUser();

                uow.DeliveryLocations.Insert(deliveryLocation);
                uow.SaveChanges();

                return deliveryLocation;
            });
        }

        public DeliveryLocation UpdateDeliveryLocation(DeliveryLocation oldDeliveryLocation, DeliveryLocation newDeliveryLocation)
        {
            if(oldDeliveryLocation == null)
            {
                return newDeliveryLocation;
            }

            return ExecuteInTransaction(uow => {

                oldDeliveryLocation.CountryName = newDeliveryLocation.CountryName;
                oldDeliveryLocation.RegionName = newDeliveryLocation.RegionName;
                oldDeliveryLocation.CityName = newDeliveryLocation.CityName;
                oldDeliveryLocation.AddressDetail = newDeliveryLocation.AddressDetail;
                oldDeliveryLocation.PostalCode = newDeliveryLocation.PostalCode;

                uow.DeliveryLocations.Update(oldDeliveryLocation);
                uow.SaveChanges();

                return newDeliveryLocation;
            });
        }

        public DeliveryLocation DeleteDeliveryLocation(DeliveryLocation deliveryLocation)
        {
            if(deliveryLocation == null)
            {
                return deliveryLocation;
            }

            return ExecuteInTransaction(uow => {
                
                uow.DeliveryLocations.Delete(deliveryLocation);
                uow.SaveChanges();
                
                return deliveryLocation;
            });
            
        }
    }
}
