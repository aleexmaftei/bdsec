using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace eCommerce.BusinessLogic
{
    public class ManufacturerService : BaseService
    {
        public ManufacturerService(UnitOfWork uow)
            :base(uow)
        {

        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            return UnitOfWork.Manufacturers.Get()
                .ToList();
        }

        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            return UnitOfWork.Manufacturers.Get()
                .FirstOrDefault(cnd => cnd.ManufacturerId == manufacturerId); 
        }

        public IEnumerable<Manufacturer> GetManufacturersByProductCategory(int productCategory)
        {
             return UnitOfWork.ProductDetails.Get()
                .Include(pd => pd.ProductCategory)
                .Include(prod => prod.Product)
                    .ThenInclude(man => man.Manufacturer)
                .Where(cnd => cnd.ProductCategoryId == productCategory)
                .GroupBy(grp => new { grp.Product.ManufacturerId,
                                      grp.Product.Manufacturer.ManufacturerLogo,
                                      grp.Product.Manufacturer.ManufacturerName })
                .Select(s => new Manufacturer {
                                                ManufacturerId = s.Key.ManufacturerId,
                                                ManufacturerLogo = s.Key.ManufacturerLogo,
                                                ManufacturerName = s.Key.ManufacturerName,
                                              })
                .ToList();
        }
    }
}
