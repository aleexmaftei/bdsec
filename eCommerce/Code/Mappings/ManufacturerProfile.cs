using AutoMapper;
using eCommerce.DataAccess;
using eCommerce.Models.ProductVM;

namespace eCommerce.Code.Mappings
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<Manufacturer, ManufacturerVM>();
        }
    }
}
