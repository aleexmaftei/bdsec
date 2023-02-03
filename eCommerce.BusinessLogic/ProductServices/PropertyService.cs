using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.BusinessLogic.ProductServices
{
    public class PropertyService : BaseService
    {

        public PropertyService(UnitOfWork uow)
            : base(uow)
        {
        }

        public IEnumerable<Properties> GetPropertiesByCategory(int categoryId)
        {
            return UnitOfWork.Properties.Get()
                .Where(cnd => cnd.ProductBaseCategoryId == categoryId)
                .ToList();
        }
    }
}
