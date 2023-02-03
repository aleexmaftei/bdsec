using eCommerce.Models.AdminVM;
using AutoMapper;
using eCommerce.Entities.Entities.ProductAdmin;
using eCommerce.Entities.DTOs.AdminDtos;

namespace eCommerce.Code.Mappings
{
    public class AdminAddProfile : Profile
    {
        public AdminAddProfile()
        {
            CreateMap<ProductPropertiesWithValuesVM, ProductPropertiesWithValuesDto>();
            CreateMap<ProductPropertiesWithValuesDto, ProductPropertiesWithValues>();

            CreateMap<AdminAddVM, ProductDto>();
            CreateMap<ProductDto, AdminAdd>();
        }
    }
}
