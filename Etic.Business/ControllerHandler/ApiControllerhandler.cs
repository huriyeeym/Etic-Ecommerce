using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Business.Helpers;
using Etic.Business.Services;
using Etic.Entities;
using Microsoft.AspNetCore.Http;

namespace Etic.Business.ControllerHandler
{
    public class ApiControllerhandler : IApiControllerHandler
    {
        private IBasketService _basketService;
        IBasketProductService _basketProductService;
        ICookieHelper _cookieHelper;
        IProductService _productService;
        IProductImageService _productImageService;

        public ApiControllerhandler(IBasketService basketService, IBasketProductService basketProductService, ICookieHelper cookieHelper, IProductService productService, IProductImageService productImageService)
        {
            _basketService = basketService;
            _basketProductService = basketProductService;
            _cookieHelper = cookieHelper;
            _productService = productService;
            _productImageService = productImageService;
        }

        public Guid CheckBasket(HttpRequest request, HttpResponse response)
        {
            var cookie = _cookieHelper.Read(CookieTypes.Basket, request);
            if (cookie == null)
            {
                Guid guid = Guid.NewGuid();
                CreateBasket(guid);
                _cookieHelper.Create(CookieTypes.Basket, guid.ToString(), DateTime.Now.AddYears(1), response);
                return guid;
            }
            else
            {
                var checkBasketForDb = _basketService.CheckBasket(Guid.Parse(cookie));
                if (!checkBasketForDb)
                {
                    Guid guid = Guid.NewGuid();
                    CreateBasket(guid);
                    _cookieHelper.Create(CookieTypes.Basket, guid.ToString(), DateTime.Now.AddYears(1), response);
                    return guid;
                }
                else
                {
                    return Guid.Parse(cookie);
                }
            }
        }

        public void CreateBasket(Guid guid)
        {
            _basketService.CreateBasket(guid);
        }

        public void AddBasketProduct(int productId, int quantity, string guidKey)
        {
            _basketProductService.Add(productId, quantity, guidKey);
        }

        public IList<BasketProduct> GetBasketProducts(string guidKey)
        {
            return _basketProductService.List(guidKey);
        }

        public IList<Product> GetBasketProductCalculatePrice(int[] products)
        {
            IList<Product> result = new List<Product>();
            foreach (var productId in products)
            {
                var product = _productService.GetById(productId);
                result.Add(product);
            }
            return result;


        }
        //rasim
        public string GetProductImage(int productId)
        {
            var image = _productImageService.GetById(productId);
            return "/"+image.ImageUrl;
        }
    }

    public interface IApiControllerHandler
    {
        Guid CheckBasket(HttpRequest request, HttpResponse response);
        void CreateBasket(Guid guid);
        void AddBasketProduct(int productId, int quantity, string guidKey);
        IList<BasketProduct> GetBasketProducts(string guidKey);
        IList<Product> GetBasketProductCalculatePrice(int[] products);
        string GetProductImage(int productId);

    }
}
