using AutoMapper;
using eCommerce.Entities.DTOs.AdminDtos;
using eCommerce.Entities.Entities.ProductAdmin;
using eCommerce.Models.AdminVM;

namespace eCommerce.Code.Mappings
{
    public class AdminUpdateProfile : Profile
    {
        public AdminUpdateProfile()
        {
            CreateMap<ProductPropertiesWithValuesVM, ProductPropertiesWithValuesDto>();
            CreateMap<ProductPropertiesWithValuesDto, ProductPropertiesWithValues>();

            CreateMap<AdminUpdateVM, UpdateDto>();
            CreateMap<UpdateDto, AdminUpdate>();
        }
    }
}
