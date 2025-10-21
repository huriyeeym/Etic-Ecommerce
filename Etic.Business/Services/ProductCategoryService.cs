using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryDal _productCategoryDal;

        public ProductCategoryService(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }
        public IList<ProductCategory> GetProductsWithCategory(int categoryId)
        {
            return _productCategoryDal.GetAll(x => x.CategoryId == categoryId);
        }

        public IList<ProductCategory> GetAll()
        {
            return _productCategoryDal.GetAll();
        }
    }

    public interface IProductCategoryService
    {
        IList<ProductCategory> GetProductsWithCategory(int categoryId);
        IList<ProductCategory> GetAll();
    }
}
