using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace RightPoint
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa-IR");


        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {

                string url = HttpContext.Current.Request.Url.AbsolutePath;

               
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string UserName = ticket.Name;
                RPC.Common.CurrentUser = new RPC.User();
                if (!String.IsNullOrEmpty(UserName))
                {
                    RPC.Common.CurrentUser = new RPB.User().FindByID(long.Parse(UserName));
                }

            }
            catch { }

            try {

                HttpCookie lan = Request.Cookies["language"];
                if (lan != null)
                {
                    RPC.Common.Language = lan.Value.ToString();
                }
                else
                {
                    HttpCookie Cookie = new HttpCookie("language");
                    Cookie.Value = System.Globalization.CultureInfo.CurrentCulture.EnglishName.ToString();
                    Cookie.Expires = DateTime.MaxValue; // never expire
                    HttpContext.Current.Response.Cookies.Add(Cookie);
                }
            }
            catch { }
        }
    }
}
