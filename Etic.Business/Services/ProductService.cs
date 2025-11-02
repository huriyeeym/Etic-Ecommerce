using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IList<Product> GetAll()
        {
           return _productDal.GetAll();
        }

        public IList<Product> GetAllActive()
        {
            return _productDal.GetAll(x => !x.IsDeleted && x.IsActive);
        }

        public Product GetBySeoUrl(string seoUrl)
        {
            return _productDal.Get(x => x.SeoLink == seoUrl);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(x => x.Id == productId);
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                product.IsDeleted = true;
                product.DeletedDate = DateTime.Now;
                _productDal.Update(product);
            }
        }

        public int GetTotalCount()
        {
            return _productDal.GetAll(x => !x.IsDeleted).Count;
        }

        public int GetActiveCount()
        {
            return _productDal.GetAll(x => !x.IsDeleted && x.IsActive).Count;
        }

        public int GetOutOfStockCount()
        {
            return _productDal.GetAll(x => !x.IsDeleted && x.Stock == 0).Count;
        }

        public void UpdateStock(int productId, int newStock)
        {
            var product = GetById(productId);
            if (product != null)
            {
                product.Stock = newStock;
                product.UpdatedDate = DateTime.Now;
                _productDal.Update(product);
            }
        }

        public void UpdatePrice(int productId, decimal newPrice)
        {
            var product = GetById(productId);
            if (product != null)
            {
                product.Price = newPrice;
                product.UpdatedDate = DateTime.Now;
                _productDal.Update(product);
            }
        }
    }

    public interface IProductService
    {
        IList<Product> GetAll();
        IList<Product> GetAllActive();
        Product GetBySeoUrl(string seoUrl);
        Product GetById(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        int GetTotalCount();
        int GetActiveCount();
        int GetOutOfStockCount();
        void UpdateStock(int productId, int newStock);
        void UpdatePrice(int productId, decimal newPrice);
    }
}
