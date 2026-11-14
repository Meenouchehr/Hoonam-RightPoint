using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using RightPoint.Models;

namespace RightPoint.Controllers
{
    public class AccountController : Controller
    {




        // GET: Account
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Login(String returnUrl)
        {
            if (returnUrl != null)
            {
                if (returnUrl.Equals("/Account/LogOff"))
                {
                    returnUrl = "/";
                }
                TempData["ReturnUrl"] = returnUrl;
            }
            else
            {
                returnUrl = "/";
                TempData["ReturnUrl"] = returnUrl;
            }


            TempData.Keep("ReturnUrl");

            LoginModel model = new LoginModel();
            HttpCookie lanOld = Request.Cookies["language"];
            if (lanOld != null)
            {
                int idx = model.LanList.FindIndex(i => lanOld.Value.Contains(i.Title));                
                model.Lan.Title = model.LanList[idx].Title;
            }


            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult Athenticate(LoginModel model)
        {

            TempData.Keep("ReturnUrl");
            if (ModelState.IsValid)
            {

                if (((model.Username != null) && (model.Username.Trim().Length > 0))
                    && ((model.Password != null) && (model.Password.Trim().Length > 0)))
                {
                    bool check = new RPB.User().IsAthenticate(model.Username, model.Password);
                    if (check)
                    {

                        try
                        {

                            System.Web.Security.FormsAuthentication.SetAuthCookie(RPC.Common.CurrentUser.ID.ToString(), false);
                        }
                        catch { }
                        if (TempData["ReturnUrl"]==null)
                        {
                            TempData["ReturnUrl"] = "/";
                        }
                        if (RPC.Common.CurrentUser.FirstVisit)
                            return Json(new { Status = "Exist", Title = Resource_fa.Message, Message = LocalizedResource.ValueOf("UserRecognize"), ReturnUrl = TempData["ReturnUrl"].ToString() }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { Status = "Exist", Title = Resource_fa.Message, Message = LocalizedResource.ValueOf("UserRecognize"), ReturnUrl = Url.Action("Agreement","Account") }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { Status = "NotExist", Title = Resource_fa.Fault, Message = LocalizedResource.ValueOf("UserNotExist"), ReturnUrl = "" }, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(new { Status = "Fail", Title = Resource_fa.Error, Message = LocalizedResource.ValueOf("Call"), ReturnUrl = "" }, JsonRequestBehavior.AllowGet);


        }

        public void LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            System.Web.Configuration.SessionStateSection sessionStateSection = (System.Web.Configuration.SessionStateSection)System.Web.Configuration.WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);


            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();





        }



        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult SaveLanguage(String lan)
        {
            try
            {

                HttpCookie lanOld = Request.Cookies["language"];
                if (lanOld != null)
                {
                    if (!lanOld.Value.Contains(lan))
                    {
                        HttpCookie Cookie = new HttpCookie("language");
                        Cookie.Value = lan;
                        Cookie.Expires = DateTime.MaxValue; // never expire
                        System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                        return Json(new { Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    HttpCookie Cookie = new HttpCookie("language");
                    Cookie.Value = lan;
                    Cookie.Expires = DateTime.MaxValue; // never expire
                    System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                    return Json(new { Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Status = "1", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);

            }
            catch { }

            return Json(new { Status = "999", Title = @LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Agreement()
        {
            return View();
        }

    }
}