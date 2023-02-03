using AutoMapper;
using eCommerce.DataAccess;
using eCommerce.Models.ProductVM;
using System.Collections.Generic;

namespace eCommerce.Code.Mappings
{
    public class ProductCommentProfile : Profile
    {
        public ProductCommentProfile()
        {
            CreateMap<ProductComment, ProductCommentVM>();
            CreateMap<ProductCommentVM, ProductComment>();
            CreateMap<List<ProductComment>, List<ProductCommentVM>>();
        }
    }
}
