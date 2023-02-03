using AutoMapper;
using eCommerce.DataAccess;
using eCommerce.Models.MyProfileVM.DeliveryLocation;
using eCommerce.Models.UserAccountVM;

namespace eCommerce.Code.Mappings
{
    public class DeliveryLocationProfile : Profile
    {
        public DeliveryLocationProfile()
        {
            CreateMap<RegisterVM, DeliveryLocation>();
            CreateMap<DeliveryLocation, DeliveryLocationVm>();
                
        }
    }
}
