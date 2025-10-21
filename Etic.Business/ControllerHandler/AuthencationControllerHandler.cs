using Etic.Business.Helpers;
using Etic.Business.Models;
using Etic.Business.Services;
using Etic.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.ControllerHandler
{
    public class AuthenticationControllerHandler : IAuthenticationControllerHandler
    {
        private readonly ILoginService _loginService;
        private readonly ICookieHelper _cookieHelper;
        private readonly IUserService _userService;

        public AuthenticationControllerHandler(ILoginService loginService, ICookieHelper cookieHelper, IUserService userService)
        {
            _loginService = loginService;
            _cookieHelper = cookieHelper;
            _userService = userService;
        }
        //private bunu dışarıdan kullanmayacağız sadece bu class içerisinde kullanacağız.
        private User Login(string email, string password)
        {
            User result = _loginService.Login(email, password);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public bool UserLogin(string email, string password, HttpResponse httpResponse)
        {
            password = StringHelper.ToMd5(password).ToLower();
            var loginResult = Login(email, password);

            if (loginResult is not null)
            {
                var key = _userService.ChangeGuidKey(loginResult);

                _cookieHelper.Create(CookieTypes.User, key, DateTime.Now.AddYears(1), httpResponse);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exit(HttpRequest httpRequest)
        {
            try
            {
                var cookie = _cookieHelper.Read(CookieTypes.User, httpRequest);
                if (cookie == null)
                {
                    return false;
                }
                _userService.ResetUserGuidKey(cookie);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
    public interface IAuthenticationControllerHandler
    {
        // Login methodu interface içerisinde yer almaz sebebi ise login methodu private yani sadece class içerisinde erişilebilirdir. Interface içerisine eklenen method imzaları sadece dışarıda erişilebilen methodlar içindir.
        public bool UserLogin(string email, string password, HttpResponse httpResponse);
        public bool Exit(HttpRequest httpRequest);
    }
}
