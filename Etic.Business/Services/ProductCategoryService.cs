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

        public IList<ProductCategory> GetCategoriesByProductId(int productId)
        {
            return _productCategoryDal.GetAll(x => x.ProductId == productId);
        }

        public IList<ProductCategory> GetAll()
        {
            return _productCategoryDal.GetAll();
        }

        public void Add(ProductCategory productCategory)
        {
            _productCategoryDal.Add(productCategory);
        }

        public void DeleteByProductId(int productId)
        {
            var productCategories = GetCategoriesByProductId(productId);
            foreach (var pc in productCategories)
            {
                _productCategoryDal.Delete(pc);
            }
        }

        public void UpdateProductCategories(int productId, List<int> categoryIds)
        {
            // Önce eski kategorileri sil
            DeleteByProductId(productId);

            // Yeni kategorileri ekle
            foreach (var categoryId in categoryIds)
            {
                Add(new ProductCategory
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });
            }
        }
    }

    public interface IProductCategoryService
    {
        IList<ProductCategory> GetProductsWithCategory(int categoryId);
        IList<ProductCategory> GetCategoriesByProductId(int productId);
        IList<ProductCategory> GetAll();
        void Add(ProductCategory productCategory);
        void DeleteByProductId(int productId);
        void UpdateProductCategories(int productId, List<int> categoryIds);
    }
}
