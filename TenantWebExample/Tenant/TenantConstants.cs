using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenantWebExample
{
    public static class TenantConstants
    {
        public const string CookieEmpresa = "cookieCustomerTenantName";
        public const string CookieUser = "cookieUserTenantName";
        public static readonly List<string> CookieList = new List<string>()
        {
            "cookieCustomerTenantName",
            "cookieUserTenantName",
        };
    }
}