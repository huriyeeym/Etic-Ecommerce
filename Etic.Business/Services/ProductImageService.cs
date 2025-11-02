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

        public ProductImage GetById(int id)
        {
            return _productImageDal.Get(x => x.Id == id);
        }

        public IList<ProductImage> GetAll()
        {
            return _productImageDal.GetAll();
        }

        public void Add(ProductImage productImage)
        {
            _productImageDal.Add(productImage);
        }

        public void Update(ProductImage productImage)
        {
            _productImageDal.Update(productImage);
        }

        public void Delete(int id)
        {
            var image = GetById(id);
            if (image != null)
            {
                _productImageDal.Delete(image);
            }
        }

        public void DeleteByProductId(int productId)
        {
            var images = GetAllByProductId(productId);
            foreach (var image in images)
            {
                _productImageDal.Delete(image);
            }
        }
    }

    public interface IProductImageService
    {
        IList<ProductImage> GetAllByProductId(int productId);
        ProductImage GetById(int id);
        IList<ProductImage> GetAll();
        void Add(ProductImage productImage);
        void Update(ProductImage productImage);
        void Delete(int id);
        void DeleteByProductId(int productId);
    }
}
