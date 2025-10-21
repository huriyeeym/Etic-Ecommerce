using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    public class BasketService : IBasketService
    {
        private IBasketDal _basketDal;

        public BasketService(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public void CreateBasket(Guid guid)
        {
            Basket basket = new Basket();
            basket.CreateDate = DateTime.Now;
            basket.Id = guid;
            basket.UserId = 0;

            _basketDal.Add(basket);
        }

        public bool CheckBasket(Guid guid)
        {
            var basket = _basketDal.Any(x => x.Id == guid && x.Status != false);
            return basket;
        }
    }

    public interface IBasketService
    {
        void CreateBasket(Guid guid);
        bool CheckBasket(Guid guid);
    }
}
