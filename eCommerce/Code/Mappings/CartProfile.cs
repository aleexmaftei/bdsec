using AutoMapper;
using eCommerce.DataAccess;
using eCommerce.Models.CartVM;

namespace eCommerce.Code.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartVM, Cart>();
            CreateMap<Cart, CartVM>()
                .ForMember(dest => dest.ProductName, m => m.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductImage, m => m.MapFrom(src => src.Product.ProductImage))
                .ForMember(dest => dest.ProductPrice, m => m.MapFrom(src => src.Product.ProductPrice));
        }
    }
}
