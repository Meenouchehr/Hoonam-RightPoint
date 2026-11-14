using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RightPoint.Controllers
{
    [CustomAuthorize(Models.Access.Admin, Models.Access.Manager, Models.Access.User)]
    public class UserPanelController : Controller
    {
        // GET: UserPanel

        #region Respondent


        public ActionResult RespondentList()
        {

            return View();
        }


        public ActionResult Respondent()
        {
            Models.RespondentModel model = new Models.RespondentModel();
            model.ChannelList = new RPB.Channel().All();
            model.CityList = new RPB.City().All();
            model.CityList.Insert(0, new RPC.City() { ID = 0, Title = "بدون شهر", EnglishTitle = "None" });
            if (TempData["EditRespondent"] != null)
            {
                long id = (long)TempData["EditRespondent"];
                RPC.Respondent obj = new RPB.Respondent().FindByID(id);

                model.Ages = (byte)((obj.ResponderAges == null) ? 0 : obj.ResponderAges);
                model.BestAdvertising = obj.BestAdvertising;
                model.BestClass = (long)((obj.BestClass == null) ? 0 : obj.BestClass);
                model.BestCompany = obj.BestCompany;

                model.BestAdvertising2 = obj.BestAdvertising2;
                model.BestClass2 = (long)((obj.BestClass2 == null) ? 0 : obj.BestClass2);
                model.BestCompany2 = obj.BestCompany2;

                model.Fullname = obj.ResponderName;
                model.ID = obj.ID;
                model.IsPublished = (obj.IsPublish == true) ? true : false;
                model.Education = (byte)((obj.ResponderEducation == null) ? 0 : obj.ResponderEducation);
                model.FamilyIncome = (byte)((obj.ResponderFamilyIncome == null) ? 0 : obj.ResponderFamilyIncome);
                model.Gender = (byte)((obj.ResponderGender == null) ? 0 : (obj.ResponderGender == true) ? 1 : 2);
                model.Occupation = (byte)((obj.ResponderOccupation == null) ? 0 : obj.ResponderOccupation);
                model.MaritalStatus = (byte)((obj.ResponderMaritalStatus == null) ? 0 : obj.ResponderMaritalStatus);
                model.ResponderID = obj.Responder;

                model.Option = (byte)((obj.WatchTV == null) ? 0 : obj.WatchTV);
                model.WorstAdvertising = obj.WorstAdvertising;
                model.WorstClass = (long)((obj.WorstClass == null) ? 0 : obj.WorstClass);
                model.WorstCompany = obj.WorstCompany;

                model.WorstAdvertising2 = obj.WorstAdvertising2;
                model.WorstClass2 = (long)((obj.WorstClass2 == null) ? 0 : obj.WorstClass2);
                model.WorstCompany2 = obj.WorstCompany2;


                model.Questionnaire = (long)((obj.Questionnaire == null) ? 0 : obj.Questionnaire);
                model.ProgramList = new RPB.Answer().FindByRespondent(id).Select(i => new Models.ProgramStatusModel()
                {
                    ID = (long)((i.Program == null) ? 0 : i.Program)
                    ,
                    Status = ((byte)((i.Option == null) ? 0 : i.Option)).ToString()
                    ,
                    Title = i.ProgramTitle
                }).ToList();
                model.City = obj.ResponderCity;
               
            }

            return View(model);
        }



        [ValidateAntiForgeryToken]
        public JsonResult SaveRespondent(Models.RespondentModel model)
        {


            if (model.Fullname == null)
                return Json(new { Status = "999", Title = "هشدار", Message = "نام پاسخ دهنده وارد نشده است دوباره تلاش کنید." }, JsonRequestBehavior.AllowGet);
            else if (model.Fullname.Trim().Length == 0)
                return Json(new { Status = "999", Title = "هشدار", Message = "نام پاسخ دهنده وارد نشده است دوباره تلاش کنید." }, JsonRequestBehavior.AllowGet);


            RPB.Respondent business = new RPB.Respondent();
            business.Changed += RespondentBusiness_Changed;
            business.Save(model.toConvert());

            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = "پیام", Message = "موفقیت" }, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(new { Status = "999", Title = "هشدار", Message = "مشکل" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditRespondent(long id)
        {

            DateTime now = DateTime.Now;

            RPC.Respondent r = new RPB.Respondent().FindByID(id);







            TempData["EditRespondent"] = id;
            return RedirectToAction("Respondent");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteRespondent(List<Models.RespondentListModel> model, string run)
        {
            RPB.Respondent business = new RPB.Respondent();
            business.Changed += RespondentBusiness_Changed;
            if (run == "1")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Respondent() { ID = i.ID }).ToList(), true);
            }
            else if (run == "2")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Respondent() { ID = i.ID }).ToList(), false);
            }

            else if (run == "3")
            {
                business.Delete(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Respondent() { ID = i.ID }).ToList());

            }

            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = "پیام", Message = "موفقیت" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = "هشدار", Message = "مشکل" }, JsonRequestBehavior.AllowGet);
        }


        private void RespondentBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }



        [HttpGet]
        public ActionResult RespondentGrid(int start, int rowNo)
        {

            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);


            List<Models.RespondentListModel> list = null;

            if (RPC.Common.CurrentUser.Access == 1 || RPC.Common.CurrentUser.Access == 2)
            {
                if (rowNo == 0)
                    list = new RPB.Respondent().All().Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                else

                    list = new RPB.Respondent().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();
            }
            else if (RPC.Common.CurrentUser.Access == 3)
            {
                if (rowNo == 0)
                    list = new RPB.Respondent().FilterByUser(RPC.Common.CurrentUser.ID).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                else

                    list = new RPB.Respondent().SelectRange(RPC.Common.CurrentUser.ID, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();
            }
            return PartialView("_RespondentGrid", list);
        }


        [HttpGet]
        public ActionResult RespondentsSearch(String keyword)
        {
            List<Models.RespondentListModel> list = new RPB.Respondent().FilterBySearch(keyword).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();
            return PartialView("_RespondentGrid", list);
        }


        [HttpGet]
        public ActionResult Respondents(int display, int start, int rowNo)
        {
            List<Models.RespondentListModel> list = null;
            switch (display)
            {
                case 0:

                    if (rowNo == 0)
                        list = new RPB.Respondent().All().Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    else

                        list = new RPB.Respondent().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();


                    break;
                case 1:

                    if (rowNo == 0)
                        list = new RPB.Respondent().FilterByPublish(true).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    else

                        list = new RPB.Respondent().FilterByPublish(true, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    break;
                case 2:

                    if (rowNo == 0)
                        list = new RPB.Respondent().FilterByPublish(false).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    else

                        list = new RPB.Respondent().FilterByPublish(false, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.RespondentListModel() { ID = i.ID, QTitle = i.QTitle, QNo = i.QNo, ResponderName = i.ResponderName, IsPublished = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();


                    break;


            }




            return PartialView("_RespondentGrid", list);
        }

        [HttpGet]
        public int RespondentGridButton(int rowNo)
        {
            if (rowNo == 0)
                return 0;

            long count = new RPB.Respondent().Count();
            // int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
            if (count > rowNo)
            {
                if (count % rowNo != 0)
                {
                    return ((int)(count / rowNo)) + 1;
                }
                else
                {
                    return (int)(count / rowNo);
                }
            }
            return 0;
        }


        [HttpGet]
        public int RespondentsButton(int display, int rowNo)
        {
            if (rowNo == 0)
                return 0;

            long count = 0;
            switch (display)
            {
                case 0:
                    count = new RPB.Respondent().Count();
                    break;

                case 1:
                    count = new RPB.Respondent().Count(true);
                    break;

                case 2:
                    count = new RPB.Respondent().Count(false);
                    break;
            }

            if (count > rowNo)
            {
                if (count % rowNo != 0)
                {
                    return ((int)(count / rowNo)) + 1;
                }
                else
                {
                    return (int)(count / rowNo);
                }
            }
            return 0;
        }

        #endregion

        #region Setting


        public ActionResult Setting()
        {

            return View();
        }


        [AllowAnonymous]
        public ActionResult GetAMD(byte media)
        {
            List<RPC.AMD> list = new RPB.AMD().FilterByMedia(media);
            return PartialView("_AMD", list);
        }
        [AllowAnonymous]
        public ActionResult GetACC()
        {
            List<RPC.ACC> list = new RPB.ACC().FilterByMedia(1);
            return PartialView("_ACC", list);
        }
        [AllowAnonymous]
        public ActionResult GetLottary()
        {
            List<RPC.Lottary> list = new RPB.Lottary().FilterByMedia(1);
            return PartialView("_Lottary", list);
        }
        [AllowAnonymous]
        public ActionResult GetOwner()
        {
            List<RPC.OwnerGroup> list = new RPB.OwnerGroup().All();
            return PartialView("_Owner", list);
        }
        [AllowAnonymous]
        public ActionResult GetPosition()
        {
            List<RPC.Position> list = new RPB.Position().FilterByMedia(1);
            return PartialView("_Position", list);
        }
        [AllowAnonymous]
        public ActionResult GetContract()
        {
            List<RPC.Contract> list = new RPB.Contract().All();
            return PartialView("_Contract", list);
        }
        [AllowAnonymous]
        public ActionResult GetClass(byte media)
        {
            List<RPC.Class> list = new RPB.Class().FilterByMedia(media);
            list = list.Select(i => new RPC.Class() { ID = i.ID, ClassNo = i.ClassNo, PPS = (((double)i.PPS) / 1000) }).ToList();

            return PartialView("_Class", list);

        }

        #endregion


        #region changeLanguge
        //public ActionResult Language()
        //{
        //    Models.LanguageModel model = new Models.LanguageModel();
        //    return View(model);
        //}
        //public JsonResult SaveLanguage(Models.LanguageModel model)
        //{
        //    try
        //    {
        //        HttpCookie Cookie = new HttpCookie("language");
        //        Cookie.Value = model.Lan.Title;
        //        Cookie.Expires = DateTime.MaxValue; // never expire
        //        System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        //        return Json(new { Status = "0", Title = @LocalizedResource.ValueOf("Message, Message = @LocalizedResource.ValueOf("Success }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch { }

        //    return Json(new { Status = "999", Title = @LocalizedResource.ValueOf("Warning, Message = @LocalizedResource.ValueOf("Fault }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

    }
}