using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    public class ProductImageService : IProductImageService
    {
        private IProductImageDal _productImageDal;

        public ProductImageService(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

        public IList<ProductImage> GetAllByProductId(int productId)
        {
            return _productImageDal.GetAll(x => x.ProductId == productId).OrderBy(o => o.Sort).ToList();

        }

        public ProductImage GetById(int productId)
        {
            return _productImageDal.Get(x => x.ProductId == productId);
        }

        public IList<ProductImage> GetAll()
        {
            return _productImageDal.GetAll();
        }
    }

    public interface IProductImageService
    {
        IList<ProductImage> GetAllByProductId(int productId);
        ProductImage GetById(int productId);

        IList<ProductImage> GetAll();//bu
    }
}
