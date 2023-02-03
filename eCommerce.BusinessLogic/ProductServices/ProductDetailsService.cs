using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.BusinessLogic.ProductServices
{
    public class ProductDetailsService : BaseService
    {
        public ProductDetailsService(UnitOfWork uow) 
            :base(uow)
        {
        }


        public ProductDetail GetProductDetailsByProductId(int productId)
        {
            return UnitOfWork.ProductDetails.Get()
                   .Include(pt => pt.ProductCategory)
                   .Include(pr => pr.Property)
                   .FirstOrDefault(a => a.ProductId == productId);
        }

        public IEnumerable<ProductDetail> GetAllProductDetailsNotDeleted(int productId, int categoryId)
        {
            return UnitOfWork.ProductDetails.Get()
                   .Include(pt => pt.ProductCategory)
                   .Include(pr => pr.Property)
                   .Where(a => a.ProductId == productId && 
                               a.ProductCategoryId == categoryId && 
                               a.Product.IsDeleted == false)
                   .OrderBy(cnd => cnd.PropertyId)
                   .ToList();
        }

        public IEnumerable<ProductDetail> GetAllProductDetailsByProductId(int productId)
        {
            return UnitOfWork.ProductDetails.Get()
                   .Include(pt => pt.ProductCategory)
                   .Include(pr => pr.Property)
                   .Where(a => a.ProductId == productId &&
                               a.Product.IsDeleted == false)
                   .ToList();
        }
    }
}
