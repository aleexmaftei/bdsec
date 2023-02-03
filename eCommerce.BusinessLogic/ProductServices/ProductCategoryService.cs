using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace eCommerce.BusinessLogic
{
    public class ProductCategoryService : BaseService
    {
        public ProductCategoryService(UnitOfWork uow) 
            : base(uow)
        {
        }
        
        public IEnumerable<ProductCategory> GetAllBaseCategories()
        {
            return UnitOfWork.ProductCategories.Get()
                .Include(pd => pd.ProductDetail)
                    .ThenInclude(pr => pr.Property)
                .Where(pt => pt.ParentProductCategoryId == null)
                .ToList();
        }
        
        public IEnumerable<ProductCategory> GetAllSubcategoriesByParentId(int parentId)
        {
            return UnitOfWork.ProductCategories.Get()
                .Include(pd => pd.ProductDetail)
                    .ThenInclude(pr => pr.Property)
                .Where(pt => pt.ParentProductCategoryId == parentId)
                .ToList();
        }

        public ProductCategory GetCategoryById(int id)
        {
            return UnitOfWork.ProductCategories.Get()
                 .FirstOrDefault(pt => pt.ProductCategoryId == id);
        }
    }
}
