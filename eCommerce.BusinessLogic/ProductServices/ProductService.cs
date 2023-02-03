using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.Entities;
using eCommerce.Entities.Entities.ProductAdmin;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.BusinessLogic.ProductServices
{
    public class ProductService : BaseService
    {
        public ProductService(UnitOfWork uow)
            : base(uow)
        {
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return UnitOfWork.Products.Get()
                   .Include(pd => pd.ProductDetail)
                   .Include(man => man.Manufacturer)
                   .ToList();
        }

        public IEnumerable<Product> GetAllProductsNotDeleted()
        {
            return UnitOfWork.Products.Get()
                   .Include(pd => pd.ProductDetail)
                   .Include(man => man.Manufacturer)
                   .Where(cond => cond.IsDeleted == false)
                   .ToList();
        }

        public IEnumerable<Product> GetAllProductsNotDeleted(int currentPage, int categoryId)
        {
            const int pageSize = 5;
            if(currentPage <= 0)
            {
                currentPage = 1;
            }

            return UnitOfWork.ProductDetails.Get()
                   .Include(pt => pt.ProductCategory)
                   .Include(pr => pr.Property)
                   .Include(prod => prod.Product)
                        .ThenInclude(man => man.Manufacturer)
                   .Where(cond => cond.Product.IsDeleted == false &&
                                  cond.ProductCategoryId == categoryId)
                   .GroupBy(grp => new {
                                           grp.Product.ProductId,
                                           grp.Product.ManufacturerId,
                                           grp.Product.ProductName,
                                           grp.Product.ProductImage,
                                           grp.Product.ProductPrice,
                                           grp.Product.IsDeleted,
                                           grp.Product.Quantity,
                                           
                   })
                   .Select(sel => new Product {
                                                ProductId = sel.Key.ProductId,
                                                ManufacturerId = sel.Key.ManufacturerId,
                                                ProductName = sel.Key.ProductName,
                                                ProductImage = sel.Key.ProductImage,
                                                ProductPrice = sel.Key.ProductPrice,
                                                IsDeleted = sel.Key.IsDeleted,
                                                Quantity = sel.Key.Quantity,
                                                
                                              })
                   .Skip((currentPage - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
        
        public IEnumerable<Product> GetAllProductsFilteredBy(Filters filters)
        {
            if(filters.ManufacturersFilter.Count == 0)
            {
                if(filters.MaxPriceFilter == -1)
                {
                    return UnitOfWork.Products.Get()
                        .Include(mn => mn.Manufacturer)
                        .Include(pd => pd.ProductDetail)
                            .ThenInclude(pc => pc.ProductCategory)
                        .Where(cnd => filters.MinPriceFilter <= cnd.ProductPrice && 
                                      cnd.IsDeleted == false)
                        .ToList();
                }
                
                if(filters.MinPriceFilter != -1)
                {
                    return UnitOfWork.Products.Get()
                        .Include(mn => mn.Manufacturer)
                        .Where(cnd => filters.MinPriceFilter <= cnd.ProductPrice &&
                                      cnd.ProductPrice <= filters.MaxPriceFilter &&
                                      cnd.IsDeleted == false)
                        .ToList();
                }
            }

            if(filters.MinPriceFilter == 0 && filters.MaxPriceFilter == 0)
            {
                return UnitOfWork.Products.Get()
                    .Include(mn => mn.Manufacturer)
                    .Where(cnd => filters.ManufacturersFilter.Contains(cnd.Manufacturer.ManufacturerName) &&
                                  cnd.IsDeleted == false)
                    .ToList();
            }

            if(filters.MaxPriceFilter == -1)
            {
                return UnitOfWork.Products.Get()
                .Include(mn => mn.Manufacturer)
                .Where(cnd => filters.ManufacturersFilter.Contains(cnd.Manufacturer.ManufacturerName) &&
                              filters.MinPriceFilter <= cnd.ProductPrice &&
                              cnd.IsDeleted == false)
                .ToList();
            }
            
            
            return UnitOfWork.Products.Get()
                .Include(mn => mn.Manufacturer)
                .Where(cnd => filters.ManufacturersFilter.Contains(cnd.Manufacturer.ManufacturerName) &&
                              filters.MinPriceFilter <= cnd.ProductPrice &&
                              cnd.ProductPrice <= filters.MaxPriceFilter &&
                              cnd.IsDeleted == false)
                .ToList();
        }

        public List<MostWantedProducts> GetMostWantedProducts(int numberOfProductsToView)
        {
            return UnitOfWork.UserInvoices.Get()
                .Include(ui => ui.Product)
                    .ThenInclude(man => man.Manufacturer)
                .GroupBy(x =>  new { x.ProductId, x.Product.ProductName, x.Product.ProductImage })
                .OrderByDescending(s => s.Count())
                .Select(s => new MostWantedProducts { ProductId = s.Key.ProductId, 
                                                      ProductName = s.Key.ProductName,
                                                      ProductImage = s.Key.ProductImage,
                                                      ProductsSold = s.Count()})
                .Take(numberOfProductsToView)
                .ToList();
        }

        public Product GetProductById(int productId)
        {
            return UnitOfWork.Products.Get()
                .Include(man => man.Manufacturer)
                .Include(pc => pc.ProductComment)
                .Include(pd => pd.ProductDetail)
                    .ThenInclude(prodcat => prodcat.ProductCategory)
                .FirstOrDefault(cond => cond.ProductId == productId);
        }
    }
}
