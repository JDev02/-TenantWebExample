using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenantWebExample
{
    public class Tenant
    {
        public string Empresa
        {
            get
            {
                //en este punto es donde sería bueno inyectar un dataProvider...
                string currentEmpresa = CookieUtils.GetCurrentCookieCustomerValue();
                if (currentEmpresa == null)
                {
                    throw new NullReferenceException("currentEmpresa");
                }
                //si el tenant usa algun tipo de encriptacion, en este punto se debe desencriptar la cookie.
                return currentEmpresa;
            }
        }

        public string Usuario
        {
            get
            {
                string currentUser = CookieUtils.GetCurrentCookieUserValue();
                if (currentUser == null)
                {
                    throw new NullReferenceException("currentUsuario");
                }
                //si el tenant usa algun tipo de encriptacion, en este punto se debe desencriptar la cookie.

                return currentUser;
            }
        }

        private Tenant()
        {

        }

        public static Tenant Current
        {
            get
            {
                return GetCurrent();
            }
        }

        private static Tenant GetCurrent()
        {
            //Objeto poblado para cuando sea necesario, para este ejemplo no hacia falta ya que las properties son
            //lazyloading... para optimziar es necesairo no traer el objeto poblado completamente.
            return new Tenant();
        }

        public static void LogOut()
        {
            var cookieList = TenantConstants.CookieList;
            foreach (var item in cookieList)
            {
                System.Web.HttpContext.Current.Response.Cookies.Remove(item);
                System.Web.HttpContext.Current.Request.Cookies.Remove(item);
            }

            System.Web.HttpContext.Current.Response.Cookies.Clear();
            System.Web.HttpContext.Current.Request.Cookies.Clear();
        }

        public static bool IsLogged()
        {
            return !string.IsNullOrEmpty(CookieUtils.GetCurrentCookieCustomerValue());
        }

        /// <summary>
        /// Desde este metodo será la unica forma de crear una nueva session para un tenant logeado
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static Tenant Create(string empresa, string usuario)
        {
            //validaciones
            if (empresa == null) throw new ArgumentNullException("empresa");
            if (usuario == null) throw new ArgumentNullException("usuario");

            string tenantCustomerCookieValue = CookieUtils.GetCurrentCookieCustomerValue();
            bool empresaCargada = !string.IsNullOrEmpty(tenantCustomerCookieValue);
            var tenantUserCookieValue = CookieUtils.GetCookieValue(TenantWebExample.TenantConstants.CookieUser);
            bool userCargado = !string.IsNullOrEmpty(tenantUserCookieValue);
            if (empresaCargada || userCargado)
            {
                throw new Exception(string.Format("El usuario {0} ya se encuentra logeado en la empresa {1}", tenantCustomerCookieValue ?? string.Empty, tenantUserCookieValue ?? string.Empty));
            }

            //aqui pueden encriptar los nombres de la cookie.
            CookieUtils.CreateCookie(TenantWebExample.TenantConstants.CookieEmpresa, empresa);
            CookieUtils.CreateCookie(TenantWebExample.TenantConstants.CookieUser, usuario);

            //Fluent por si se quisiera hacer algo con el tenant actual creado....
            return new Tenant();
        }
    }
}