using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RightPoint.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           
            if (RPC.Common.CurrentUser != null)
                if (!RPC.Common.CurrentUser.FirstVisit)
                {
                    new RPB.User().Firstvisit(RPC.Common.CurrentUser.ID, true);
                }
            return View();
        }

        public ActionResult Campaign()
        {
            List<RPC.Foretaste> list = new RPB.Foretaste().All();
            List<RightPoint.Models.ForetasteModel> model = new List<Models.ForetasteModel>();
            if (list.Count == 0)
            {
                model.Add(new Models.ForetasteModel());
                model.Add(new Models.ForetasteModel());
                model.Add(new Models.ForetasteModel());

            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Models.ForetasteModel f = new Models.ForetasteModel();
                    f.toConvert(list[i]);
                    model.Add(f);
                }
            }
            return PartialView("_Home", model);
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}