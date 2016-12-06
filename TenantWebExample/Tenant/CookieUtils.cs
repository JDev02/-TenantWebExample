using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenantWebExample
{
    internal static class CookieUtils
    {
        internal static readonly Func<string> GetCurrentCookieCustomerValue = () => GetCookieValue(TenantWebExample.TenantConstants.CookieEmpresa);

        internal static readonly Func<string> GetCurrentCookieUserValue = () => GetCookieValue(TenantWebExample.TenantConstants.CookieUser);

        internal static readonly Func<string, string> GetCookieValue = (cookieKey) =>
        {            var cookie = System.Web.HttpContext.Current.Request.Cookies.Get(cookieKey);
            return cookie != null ? cookie.Value : null;  
        };

        public static readonly Action<string, string> CreateCookie = (cookieKey, cookieContent) =>
        {
            HttpCookie cookie = new HttpCookie(cookieKey);
            cookie.Value = cookieContent;
            cookie.Expires = DateTime.Now.AddHours(Config.CookieTimeOut);
            HttpContext.Current.Response.Cookies.Add(cookie);
        };
    }
}