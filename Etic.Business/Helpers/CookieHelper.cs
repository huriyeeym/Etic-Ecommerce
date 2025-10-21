using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.Helpers
{
    public class CookieHelper : ICookieHelper
    {
        public void Create(CookieTypes cookieType,string value,DateTime expDate,HttpResponse response)
        {
            try
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = expDate;
                response.Cookies.Append(cookieType.ToString(), value);
            }
            catch (Exception)
            {

              
            }
        }

        public string Read(CookieTypes cookieTypes, HttpRequest request)
        {
            try
            {
                var cookies = request.Cookies[cookieTypes.ToString()];
                return cookies;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
    public interface ICookieHelper
    {
        void Create(CookieTypes cookieType, string value, DateTime expDate, HttpResponse response);
        string Read(CookieTypes cookieTypes,HttpRequest request);
    }
    public enum CookieTypes
    {
        User,
        Admin,
        Tracer,
        Basket
    }
}
