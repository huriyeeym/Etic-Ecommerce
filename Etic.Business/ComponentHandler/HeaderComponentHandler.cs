using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Etic.Business.Helpers;
using Etic.Business.Services;
using Etic.Entities;
using Microsoft.AspNetCore.Http;

namespace Etic.Business.ComponentHandler
{
    public class HeaderComponentHandler : IHeaderComponentHandler
    {
        ICookieHelper _cookieHelper; 
        IUserService _userService;

        public HeaderComponentHandler(ICookieHelper cookieHelper, IUserService userService)
        {
            _cookieHelper = cookieHelper;
            _userService = userService;
        }

        public User GetUserData(string guidKey, HttpRequest request)
        {
            //Bug : cookie silindi ise null
            var cookie = _cookieHelper.Read(CookieTypes.User, request);
            if (cookie == null)
            {
                return null;
            }
            var user = _userService.GetUserDataWithGuidKey(cookie);
            return user;
        }
    }

    public interface IHeaderComponentHandler
    {
        User GetUserData(string guidKey, HttpRequest request);
    }
}
