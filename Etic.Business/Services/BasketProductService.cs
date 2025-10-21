using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    public class BasketProductService : IBasketProductService
    {
        private IBasketProductDal _basketProductDal;
        private IVwBasketProductListDal _vwBasketProductListDal;

        public BasketProductService(IBasketProductDal basketProductDal, IVwBasketProductListDal vwBasketProductListDal)
        {
            _basketProductDal = basketProductDal;
            _vwBasketProductListDal = vwBasketProductListDal;
        }

        public void Add(int productId, int quantity, string guidKey)
        {
            var checkProduct = _basketProductDal.Get(x => x.ProductId == productId && x.Status != false && x.BasketId == Guid.Parse(guidKey));
            if (checkProduct != null)
            {
                checkProduct.Quantity += quantity;
                _basketProductDal.Update(checkProduct);
                return; // if içindeki kod çalıştıktan sonra diğer kodlar çalışmasın isteniyorsa return ile method sonlandırılır.
            }

            BasketProduct product = new BasketProduct();
            product.ProductId = productId;
            product.Quantity = quantity;
            product.BasketId = Guid.Parse(guidKey);
            product.AddDate = DateTime.Now;

            _basketProductDal.Add(product);

        }

        public IList<BasketProduct> List(string guidKey)
        {
            return _basketProductDal.GetAll(x => x.BasketId == Guid.Parse(guidKey) && x.Status != false);
        }

        public IList<VwBasketProductList> VwBasketProductList(string guidKey)
        {
            var data =  _vwBasketProductListDal.GetAll(x => x.BasketId == Guid.Parse(guidKey) && x.Status != false);
            return data;
        }
    }

    public interface IBasketProductService
    {
        void Add(int productId, int quantity, string guidKey);
        IList<BasketProduct> List(string guidKey);
        IList<VwBasketProductList> VwBasketProductList(string guidKey);
    }
}
