using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace RightPoint.Controllers
{
    [AllowAnonymous]
    public class CommonController : Controller
    {
        #region Common
        [HttpGet]
        public JsonResult GetClasses()
        {
            var list = new RPB.Class().All().Select(i => new { Value = i.ID.ToString(), Text = i.ClassNo.ToString() });
            String json = JsonConvert.SerializeObject(list);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetChannel()
        {
            var list = new RPB.Channel().All().Select(i => new { Value = i.ID.ToString(), Text = i.Title.ToString() });
            String json = JsonConvert.SerializeObject(list);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuestionnaire()
        {
            var list = new RPB.Questionnaire().All();
            String json = JsonConvert.SerializeObject(list);
            return Json(json, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult GetChannels(long questionnaireID)
        {
            var list = new RPB.Program().FindByQuestionnaire(questionnaireID);
            String json = JsonConvert.SerializeObject(list);
            return Json(json, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public List<String> GetImages()
        {
            string fullPath = Server.MapPath("/Icons");
            string site = String.Empty;
            String[] files = null;
            try
            {
                site = "../Icons";
                files = System.IO.Directory.GetFiles(fullPath);
            }
            catch { }
            List<String> list = new List<String>();
            if (files != null)
            {
                foreach (String f in files)
                {

                    String ff = System.IO.Path.GetFileName(f);
                    list.Add(String.Format("{0}{1}{2}", site, ff));
                }
            }
            return list;

        }
        #endregion
    }
}