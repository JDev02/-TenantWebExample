using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TenantWebExample.Controllers
{
    public class HomeController : Controller
    {

        //si es necesario podriamos inyectar el Tenant en el tenant factory, o con por ejemplo, ninject
        //para este ejemplo no es necesario
        public HomeController()
        {
                
        }

        public ActionResult Index()
        {
            string currentCustomerTenantRequest= Tenant.Current.Empresa;
            string currentUserTenantRequest = Tenant.Current.Empresa;

            //logic...
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
