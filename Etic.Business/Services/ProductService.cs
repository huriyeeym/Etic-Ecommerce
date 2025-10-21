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

        public Product GetBySeoUrl(string seoUrl)
        {
            return _productDal.Get(x => x.SeoLink == seoUrl);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(x => x.Id == productId);
        }
    }

    public interface IProductService
    {
        IList<Product> GetAll();
        Product GetBySeoUrl(string seoUrl);

        Product GetById(int productId); // buuuuu
    }
}
