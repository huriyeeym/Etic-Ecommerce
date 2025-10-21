using Etic.Business.Helpers;
using Etic.Business.Services;
using Etic.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.ControllerHandler
{
    public class CheckOutControllerHandler : ICheckOutControllerHandler
    {
        IUserService _userService;
        IBasketService _basketService;
        ICookieHelper _cookieHelper;
        IBasketProductService _basketProductService;
        public CheckOutControllerHandler(IUserService userService, IBasketService basketService, ICookieHelper cookieHelper, IBasketProductService basketProductService)
        {
            _userService = userService;
            _basketService = basketService;
            _cookieHelper = cookieHelper;
            _basketProductService = basketProductService;
        }

        public IList<VwBasketProductList> GetBasketProducts(HttpRequest request)
        {
            
            var guidKey = _cookieHelper.Read(CookieTypes.Basket, request);
          return _basketProductService.VwBasketProductList(guidKey);
        }

        public User GetUser(HttpRequest request)
        {
            var cookie = _cookieHelper.Read(CookieTypes.Basket, request);
            if (cookie != null)
            {
                var user = _userService.GetUserDataWithGuidKey(cookie);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
    public interface ICheckOutControllerHandler
    {
        User GetUser(HttpRequest request);
        IList<VwBasketProductList> GetBasketProducts(HttpRequest request);
    }
}
