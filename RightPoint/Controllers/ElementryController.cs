using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Generic;
using System.IO;
using System.Web.Security;

namespace RightPoint.Controllers
{

    [CustomAuthorize(Models.Access.Admin, Models.Access.Manager, Models.Access.User)]
    public class ElementryController : Controller
    {


        #region Program

        // GET: Elementry

        public ActionResult ProgramList()
        {

            return View();
        }

        public ActionResult Program()
        {
            Models.ProgramModel model = new Models.ProgramModel();
            model.Limit = String.Empty;
            model.WeekRep = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            if (TempData["EditProgram"] != null)
            {
                long id = (long)TempData["EditProgram"];
                RPC.Program obj = new RPB.Program().FindByID(id);
                model.After = (obj.After == true) ? true : false;
                model.Before = (obj.Before == true) ? true : false;
                model.Channel = (obj.Channel == null) ? 0 : (long)obj.Channel;
                model.Media = (byte)((obj.Media == null) ? 0 : obj.Media);
                model.ChannelName = obj.ChannelName;

                model.EndTime = obj.EndTime.ToPersian();
                model.ID = obj.ID;
                model.IsEnabled = (obj.Enable == true) ? true : false;
                model.Middle = (obj.Middle == true) ? true : false;
                model.PlayTime = obj.PlayTime;
                model.StartTime = obj.StartTime.ToPersian();
                model.Title = obj.Title;
                model.EnglishTitle = obj.EnglishTitle;
                model.WeekRep = new RPB.WeekRep().FindByProgram(obj.ID).Select(i => (int)i.Day).ToArray();
                model.LimitList = new RPB.Limit().FindByProgram(obj.ID);
                foreach (RPC.Limit limit in model.LimitList)
                {
                    model.Limit += "{" + limit.Date.ToPersian() + "}";
                }
            }


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveProgram(Models.ProgramModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("ProgramNameNotEnter") }, JsonRequestBehavior.AllowGet);

                else if (model.Title.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("ProgramNameNotEnter") }, JsonRequestBehavior.AllowGet);

                if (model.StartTime == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("StartTimeNotEnter") }, JsonRequestBehavior.AllowGet);

                else if (model.StartTime.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("StartTimeNotEnter") }, JsonRequestBehavior.AllowGet);



                RPB.Program business = new RPB.Program();
                business.Changed += ProgramBusiness_Changed;
                business.Save(model.toConvert());

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = LocalizedResource.ValueOf("Message"), Message = LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult EditProgram(long id)
        {

            TempData["EditProgram"] = id;
            TempData.Keep("EditProgram");
            return RedirectToAction("Program");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteProgram(List<Models.ProgramModel> model, string run)
        {
            RPB.Program business = new RPB.Program();
            business.Changed += ProgramBusiness_Changed;


            if (run == "1")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Program() { ID = i.ID }).ToList(), true);
            }
            else if (run == "2")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Program() { ID = i.ID }).ToList(), false);
            }

            else if (run == "3")
            {
                business.Delete(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Program() { ID = i.ID }).ToList());
            }


            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }

                else if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.RelationError)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "899", Title = LocalizedResource.ValueOf("Warning"), Message = "برنامه در پاسخنامه استفاده شده است و قابل  نمی باشد" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        private void ProgramBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult ProgramsSearch(String keyword)
        {
            List<Models.ProgramModel> list = new RPB.Program().FilterBySearch(keyword).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();
            return PartialView("_ProgramGrid", list);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Programs(int display, int start, int rowNo)
        {
            List<Models.ProgramModel> list = null;
            switch (display)
            {
                case 0:

                    if (rowNo == 0)
                        list = new RPB.Program().All().Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();

                    else

                        list = new RPB.Program().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();


                    break;
                case 1:

                    if (rowNo == 0)
                        list = new RPB.Program().FilterByPublish(true).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();

                    else

                        list = new RPB.Program().FilterByPublish(true, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();

                    break;
                case 2:

                    if (rowNo == 0)
                        list = new RPB.Program().FilterByPublish(false).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();

                    else

                        list = new RPB.Program().FilterByPublish(false, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false, EnglishTitle = i.EnglishTitle }).ToList();


                    break;


            }




            return PartialView("_ProgramGrid", list);
        }





        [HttpGet]
        [AllowAnonymous]
        public ActionResult ProgramGrid(int start, int rowNo)
        {

            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
            List<Models.ProgramModel> list = null;
            if (rowNo == 0)
                list = new RPB.Program().All().Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, EnglishTitle = i.EnglishTitle, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false }).ToList();

            else

                list = new RPB.Program().SelectRange(((start - 1) * rowNo), rowNo).Select(i => new Models.ProgramModel() { ID = i.ID, Title = i.Title, EnglishTitle = i.EnglishTitle, ChannelName = i.ChannelName, StartTime = i.StartTime.ToPersian(), EndTime = i.EndTime.ToPersian(), IsEnabled = (i.Enable == true) ? true : false, IsDeleted = false }).ToList();

            return PartialView("_ProgramGrid", list);
        }

        [HttpGet]
        [AllowAnonymous]
        public int ProgramGridButton(int rowNo)
        {
            if (rowNo == 0)
                return 0;

            long count = new RPB.Program().Count();
            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
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

        [AllowAnonymous]
        public ActionResult GetGroups(long id, byte media, byte month)
        {

            List<RPC.OwnerGroup> list = new RPB.OwnerGroup().All();
            List<RPC.ProgramSeries> seriesList = new RPB.ProgramSeries().All();
            List<RPC.Location> locationList = new RPB.Location().FilterByMedia(media);
            List<RPC.Class> clist = new RPB.Class().FilterByMedia(media);
            List<Models.ClassModel> cllist = new List<Models.ClassModel>();
            RPC.Program obj = null;
            Models.ProgramModel p = new Models.ProgramModel();
            p.LocationList = locationList;
            if (id != 0)
            {
                obj = new RPB.Program().FindByID(id);
            }

            foreach (RPC.Class cls in clist)
            {
                cllist.Add(new Models.ClassModel() { ID = cls.ID, Title = cls.ClassNo.ToString() });
            }



            for (byte mm = 1; mm <= 12; mm++)
            {
                for (int j = 0; j < locationList.Count; j++)
                {

                    for (int i = 0; i < list.Count; i++)
                    {
                        Models.ProgramAccessModel pa = new Models.ProgramAccessModel() { Group = list[i].ID, GroupName = list[i].Title, ClassList = cllist, SeriesList = seriesList, Location = locationList[j].ID, Month = mm, SelectMonth = month };
                        if (obj != null)
                        {
                            if (obj.ProgramAccessList != null)
                            {
                                int idx = obj.ProgramAccessList.FindIndex(m => m.Group == list[i].ID && m.Location == locationList[j].ID && m.Month == month);

                                if (idx > -1)
                                {
                                    pa.Class = (long)((obj.ProgramAccessList[idx].Class == null) ? 0 : obj.ProgramAccessList[idx].Class);
                                    pa.Title = obj.ProgramAccessList[idx].ClassNo.ToString();
                                    pa.Series = (long)((obj.ProgramAccessList[idx].Series == null) ? 0 : obj.ProgramAccessList[idx].Series);
                                    pa.Location = (long)((obj.ProgramAccessList[idx].Location == null) ? 0 : obj.ProgramAccessList[idx].Location);
                                    pa.Extra = obj.ProgramAccessList[idx].Extra;
                                    pa.ExtraType = obj.ProgramAccessList[idx].ExtraType;
                                    pa.ClassList = cllist;
                                    pa.SeriesList = seriesList;
                                    pa.Index = i;
                                }
                            }

                        }


                        p.ProgramAccessList.Add(pa);
                    }
                }
            }






            return PartialView("_GroupList", p);

        }

        [AllowAnonymous]
        public ActionResult GetChannels(long id, byte media)
        {
            RPC.Program obj = null;
            Models.ProgramModel p = new Models.ProgramModel();
            List<RPC.Channel> list = new RPB.Channel().FilterByMedia(media);
            if (id != 0)
            {
                obj = new RPB.Program().FindByID(id);
                p.Channel = (byte)((obj.Channel == null) ? 0 : obj.Channel);
                p.Media = (byte)((obj.Media == null) ? 0 : obj.Media);


            }
            p.ChannelList = list;


            return PartialView("_ChannelList", p);
        }

        [AllowAnonymous]
        public ActionResult GetLocation(byte media)
        {

            List<RPC.Location> list = new RPB.Location().FilterByMedia(media);
            return PartialView("_LocationList", list);
        }

        #endregion

        #region Questionnaire

        [AllowAnonymous]
        public ActionResult GetProgramList(long questionnaireID)
        {


            List<RPC.Channel> cList = new RPB.Channel().All();
            List<RPC.Program> list = null;
            if (questionnaireID == 0)
                list = new RPB.Program().FilterByDate(DateTime.Now).Where(i => i.Channel != null).ToList();
            else
            {

                DateTime? date = new RPB.Questionnaire().FindByID(questionnaireID).CreatDate;
                if (date == null)
                    list = new RPB.Program().FilterByDate(DateTime.Now).Where(i => i.Channel != null).ToList();
                else
                    list = new RPB.Program().FilterByDate((DateTime)date).Where(i => i.Channel != null).ToList();

            }
            Models.QuestionnaireModel model = new Models.QuestionnaireModel();
            model.ChannelList = cList.Select(i => new Models.Channel() { ChannelID = i.ID, ChannelTitle = i.Title }).ToList();
            model.ProgramList = list.Select(i => new Models.Program() { ProgramID = i.ID, ProgramTitle = i.Title, Channel = (long)((i.Channel == null) ? 0 : i.Channel), ChannelName = i.ChannelName }).ToList();

            List<RPC.Program> list2 = new RPB.Program().FindByQuestionnaire(questionnaireID);
            for (int idx = 0; idx < list2.Count; idx++)
            {
                int ix = model.ProgramList.FindIndex(i => i.ProgramID == list2[idx].ID);
                if (ix != -1)
                {
                    model.ProgramList[ix].IsChecked = true;
                }
            }



            return PartialView("_Program", model);
        }

        public ActionResult QuestionnaireList()
        {

            return View();
        }

        public ActionResult Questionnaire()
        {
            Models.QuestionnaireModel model = new Models.QuestionnaireModel();
            model.IsPublish = true;
            model.ChannelList = new RPB.Channel().All().Select(i => new Models.Channel() { ChannelID = i.ID, ChannelTitle = i.Title }).ToList();
            model.CreateDate = DateTime.Now.ToPersian();
            if (TempData["EditQuestionnaire"] != null)
            {
                long id = (long)TempData["EditQuestionnaire"];
                RPC.Questionnaire obj = new RPB.Questionnaire().FindByID(id);
                model.Code = obj.QNo;
                model.ID = obj.ID;
                model.IsPublish = (obj.IsPublish == true) ? true : false;
                model.Title = obj.Title;
                model.EnglishTitle = obj.EnglishTitle;
                model.CreateDate = obj.CreatDate.ToPersian();

                List<RPC.Question> list = new RPB.Question().FindByQuestionnaire(id);
                {
                    foreach (RPC.Question item in list)
                    {
                        int index = model.ProgramList.FindIndex(i => i.ProgramID == item.Program);
                        if (index != -1)
                        {
                            model.ProgramList[index].IsChecked = true;
                        }
                    }

                }

            }
            return View(model);
        }

        public ActionResult Statistics(long? id)
        {
            if (id != null)
            {
                var prgs = new RPB.Program().FindByQuestionnaire((long)id);
                Models.RespondentModel model = new Models.RespondentModel();
                model.Questionnaire = (long)id;
                model.BestClass = new RPB.Respondent().Count((long)id);
                model.ProgramList = prgs.Select(i => new Models.ProgramStatusModel() { ID = i.ID, Title = i.Title }).ToList();
                RPB.Answer b = new RPB.Answer();
                String items = "[";
                for (int i = 0; i < model.ProgramList.Count; i++)
                {
                    model.ProgramList[i].Status = b.FilterByScore((long)id, model.ProgramList[i].ID).ToString();
                    if (i > 0)
                        items += ",";
                    items += model.ProgramList[i].Status;

                }
                items += "]";
                model.BestCompany = items;
                return View(model);
            }

            return null;
        }

        public JsonResult Published(long id)
        {
            RPB.Respondent business = new RPB.Respondent();
            business.Changed += QuestionnaireBusiness_Changed;
            business.PublishByQuestionnaire(id, true);
            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult QuestionnaireGrid(int start, int rowNo)
        {
            List<Models.QuestionnaireModel> list = null;
            if (rowNo == 0)

                //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
                list = new RPB.Questionnaire().All().Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();
            else
                list = new RPB.Questionnaire().SelectRange(((start - 1) * rowNo), rowNo).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

            return PartialView("_QuestionnaireGrid", list);
        }

        [HttpGet]
        [AllowAnonymous]
        public int QuestionnaireGridButton(int rowNo)
        {

            if (rowNo == 0)
                return 0;


            long count = new RPB.Questionnaire().Count();
            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);

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
        [AllowAnonymous]
        public ActionResult QuestionnairesSearch(String keyword)
        {
            List<Models.QuestionnaireModel> list = new RPB.Questionnaire().FilterBySearch(keyword).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();
            return PartialView("_QuestionnaireGrid", list);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Questionnaires(int display, int start, int rowNo)
        {
            List<Models.QuestionnaireModel> list = null;
            switch (display)
            {
                case 0:

                    if (rowNo == 0)
                        list = new RPB.Questionnaire().All().Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    else

                        list = new RPB.Questionnaire().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();


                    break;
                case 1:

                    if (rowNo == 0)
                        list = new RPB.Questionnaire().FilterByPublish(true).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    else

                        list = new RPB.Questionnaire().FilterByPublish(true, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    break;
                case 2:

                    if (rowNo == 0)
                        list = new RPB.Questionnaire().FilterByPublish(false).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();

                    else

                        list = new RPB.Questionnaire().FilterByPublish(false, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.QuestionnaireModel() { ID = i.ID, Title = i.Title, Code = i.QNo, IsPublish = (i.IsPublish == true) ? true : false, IsDeleted = false }).ToList();


                    break;


            }




            return PartialView("_QuestionnaireGrid", list);
        }




        [HttpGet]
        public int QuestionnairesButton(int display, int rowNo)
        {
            if (rowNo == 0)
                return 0;

            long count = 0;
            switch (display)
            {
                case 0:
                    count = new RPB.Questionnaire().Count();
                    break;

                case 1:
                    count = new RPB.Questionnaire().Count(true);
                    break;

                case 2:
                    count = new RPB.Questionnaire().Count(false);
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveQuestionnaire(Models.QuestionnaireModel model)
        {
            if (ModelState.IsValid)
            {


                if (model.Title == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("QTitleError") }, JsonRequestBehavior.AllowGet);

                else if (model.Title.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("QTitleError") }, JsonRequestBehavior.AllowGet);

                if (model.Code == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("QCodeError") }, JsonRequestBehavior.AllowGet);
                else if (model.Code.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("QCodeError") }, JsonRequestBehavior.AllowGet);




                RPB.Questionnaire business = new RPB.Questionnaire();
                business.Changed += QuestionnaireBusiness_Changed;
                business.Save(model.toConvert());

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }





            }
            return Json(new { ID = 0, Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);

        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult EditQuestionnaire(long id)
        {

            TempData["EditQuestionnaire"] = id;
            return RedirectToAction("Questionnaire");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteQuestionnaire(List<Models.QuestionnaireModel> model, string run)
        {
            RPB.Questionnaire business = new RPB.Questionnaire();
            business.Changed += QuestionnaireBusiness_Changed;
            if (run == "1")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Questionnaire() { ID = i.ID }).ToList(), true);
            }
            else if (run == "2")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Questionnaire() { ID = i.ID }).ToList(), false);
            }

            else if (run == "3")
            {
                business.Delete(model.Where(i => i.IsDeleted == true).Select(i => new RPC.Questionnaire() { ID = i.ID }).ToList());
            }





            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        protected void QuestionnaireBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }
        #endregion

        #region User
        public ActionResult UserList()
        {

            return View();
        }
        public ActionResult Users()
        {
            Models.UserModel model = new Models.UserModel();
            model.CompanyList = new RPB.Company().All();
            model.IsEnabled = true;
            if (TempData["EditUser"] != null)
            {
                long id = (long)TempData["EditUser"];
                RPC.User obj = new RPB.User().FindByID(id);
                model.Access = (byte)((obj.Access == null) ? 0 : obj.Access);
                model.Cell = obj.Cell;
                model.Company = (long)((obj.Company == null) ? 0 : obj.Company);
                model.CreateDate = obj.CreateDate.ToPersian();
                model.DisableDate = obj.DisableDate.ToPersian();
                model.Email = obj.Email;
                model.Gender = (byte)((obj.Gender == null) ? 2 : 1);
                model.ID = obj.ID;
                model.IsEnabled = (bool)((obj.IsEnabled == true) ? true : false);
                model.Name = obj.Name;
                model.Password = obj.Password;
                model.RePassword = obj.Password;
                model.Phone = obj.Phone;
                model.Username = obj.Username;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUser(Models.UserModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Name == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("UserNameNotEnter") }, JsonRequestBehavior.AllowGet);


                else if (model.Name.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("UserNameNotEnter") }, JsonRequestBehavior.AllowGet);



                if (model.Username == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("Username_NotEnter") }, JsonRequestBehavior.AllowGet);

                else if (model.Username.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("Username_NotEnter") }, JsonRequestBehavior.AllowGet);


                if (model.Password == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("Password_NotEnter") }, JsonRequestBehavior.AllowGet);

                else if (model.Password.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("Password_NotEnter") }, JsonRequestBehavior.AllowGet);

                if (model.RePassword == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("RePassword_NotEnter") }, JsonRequestBehavior.AllowGet);


                else if (model.RePassword.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("RePassword_NotEnter") }, JsonRequestBehavior.AllowGet);

                if (!model.Password.Equals(model.RePassword))
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("PasswordError") }, JsonRequestBehavior.AllowGet);




                RPB.User business = new RPB.User();
                business.Changed += UserBusiness_Changed;
                business.Save(model.ToConvert());

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult EditUser(long id)
        {

            TempData["EditUser"] = id;
            return RedirectToAction("Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteUser(List<Models.UserModel> model, string run)
        {
            RPB.User business = new RPB.User();
            business.Changed += UserBusiness_Changed;

            if (run == "1")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.User() { ID = i.ID }).ToList(), true);
            }
            else if (run == "2")
            {
                business.Publish(model.Where(i => i.IsDeleted == true).Select(i => new RPC.User() { ID = i.ID }).ToList(), false);
            }

            else if (run == "3")
            {
                business.Delete(model.Where(i => i.IsDeleted == true).Select(i => new RPC.User() { ID = i.ID }).ToList());
            }

            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }


        private void UserBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }



        [HttpGet]
        public ActionResult UsersSearch(String keyword)
        {
            List<Models.UserModel> list = new RPB.User().FilterBySearch(keyword).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();
            return PartialView("_UserGrid", list);
        }


        [HttpGet]
        public ActionResult Userss(int display, int start, int rowNo)
        {
            List<Models.UserModel> list = null;
            switch (display)
            {
                case 0:

                    if (rowNo == 0)
                        list = new RPB.User().All().Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();

                    else

                        list = new RPB.User().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();


                    break;
                case 1:

                    if (rowNo == 0)
                        list = new RPB.User().FilterByPublish(true).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();

                    else

                        list = new RPB.User().FilterByPublish(true, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();

                    break;
                case 2:

                    if (rowNo == 0)
                        list = new RPB.User().FilterByPublish(false).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();

                    else

                        list = new RPB.User().FilterByPublish(false, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();


                    break;


            }




            return PartialView("_UserGrid", list);
        }


        [HttpGet]
        public ActionResult UserGrid(int start, int rowNo)
        {

            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
            List<Models.UserModel> list = null;
            if (rowNo == 0)
                list = new RPB.User().All().Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();
            else
                list = new RPB.User().SelectRange(((start - 1) * rowNo), rowNo).Select(i => new Models.UserModel() { CompanyTitle = i.CompanyTitle, ID = i.ID, Name = i.Name, Email = i.Email, IsEnabled = (i.IsEnabled == true) ? true : false }).ToList();

            return PartialView("_UserGrid", list);
        }

        [HttpGet]
        public int UserGridButton(int rowNo)
        {
            if (rowNo == 0)
                return 0;

            long count = new RPB.User().Count();
            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
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


        [AllowAnonymous]
        public ActionResult GetCampaign(long Company, long id)
        {

            List<RPC.Campaign> list = new RPB.Campaign().FindByCompany(Company);
            RPC.User obj = null;
            if (id != 0)
                obj = new RPB.User().FindByID(id);



            Models.UserModel c = new Models.UserModel();
            if (list != null)
            {
                foreach (RPC.Campaign item in list)
                {

                    Models.CampaignAccessModel tmp = new Models.CampaignAccessModel() { Campaign = item.ID, Name = item.Title };
                    if (obj != null)
                        if (obj.CampaignAccessList.FindIndex(i => i.Campaign == item.ID) > -1)
                        {
                            tmp.IsSelected = true;
                        }

                    c.CampaignAccessList.Add(tmp);

                }
            }

            return PartialView("_CampaignList", c);

        }

        #endregion

        #region Campaign


        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult CampaignList()
        {
            return View();
        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult Campaign()
        {

            Models.CampaignModel model = new Models.CampaignModel();
            model.LottaryList = new RPB.Lottary().FilterByMedia(1);
            model.LottaryList.Insert(0, new RPC.Lottary() { ID = 0, Title = "قرعه کشی ندارد" });
            model.ACCList = new RPB.ACC().FilterByMedia(1);
            model.CompanyList = new RPB.Company().All();
            model.QuestionaireList = new RPB.Questionnaire().All();
            model.QuestionaireList.Insert(0, new RPC.Questionnaire() { ID = 0, QNo = LocalizedResource.ValueOf("General"), Title = LocalizedResource.ValueOf("General") });
            if (TempData["EditCampaign"] != null)
            {
                long id = (long)TempData["EditCampaign"];

                RPC.Campaign obj = new RPB.Campaign().FindByID(id);
                model.ACC = obj.ACC;
                model.Comment = obj.Comment;
                model.EndDate = Generic.Date.ToPersian(obj.EndDate);
                model.ID = obj.ID;
                model.IsActive = (obj.IsActive == true) ? true : false;
                model.Lottary = (obj.Lottary == null) ? 0 : obj.Lottary;
                model.StartDate = Generic.Date.ToPersian(obj.StartDate);
                model.Title = obj.Title;
                model.EnglishTitle = obj.EnglishTitle;
                model.Company = obj.Company;
                model.Questionaire = obj.Questionnaire;
                model.Description = obj.Description;
                model.TimeRange = (byte)((obj.TimeRange == null) ? 0 : obj.TimeRange);

                foreach (var item in obj.AccessList)
                {
                    model.AccessList.Add(new Models.CampaignAccessModel() { User = item.User, IsSelected = true });
                }

            }
            return View(model);
        }



        [HttpGet]
        public ActionResult CampaignsSearch(String keyword)
        {
            List<Models.CampaignModel> list = new RPB.Campaign().FilterBySearch(keyword).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title }).ToList();
            return PartialView("_CampaignGrid", list);
        }


        [HttpGet]
        public ActionResult Campaigns(int display, int start, int rowNo)
        {
            List<Models.CampaignModel> list = null;
            switch (display)
            {
                case 0:

                    if (rowNo == 0)
                        list = new RPB.Campaign().All().Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();

                    else

                        list = new RPB.Campaign().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();


                    break;
                case 1:

                    if (rowNo == 0)
                        list = new RPB.Campaign().FilterByPublish(true).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();

                    else

                        list = new RPB.Campaign().FilterByPublish(true, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();

                    break;
                case 2:

                    if (rowNo == 0)
                        list = new RPB.Campaign().FilterByPublish(false).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();

                    else

                        list = new RPB.Campaign().FilterByPublish(false, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();


                    break;


            }




            return PartialView("_CampaignGrid", list);
        }



        [HttpGet]
        public ActionResult CampaignGrid(int start, int rowNo)
        {
            List<Models.CampaignModel> list = null;
            if (rowNo == 0)

                //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
                list = new RPB.Campaign().All().Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();
            else
                list = new RPB.Campaign().SelectRange(((start - 1) * rowNo), rowNo).Select(i => new Models.CampaignModel() { Comment = i.Comment, EndDate = Generic.Date.ToPersian(i.EndDate), ID = i.ID, IsActive = (i.IsActive == true) ? true : false, IsDelete = false, StartDate = Generic.Date.ToPersian(i.StartDate), Title = i.Title, EnglishTitle = i.EnglishTitle }).ToList();





            return PartialView("_CampaignGrid", list);
        }

        [HttpGet]
        public int CampaignGridButton(int rowNo)
        {

            if (rowNo == 0)
                return 0;


            long count = new RPB.Campaign().Count();
            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);

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




        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult SaveCampaign(Models.CampaignModel model)
        {
            if (ModelState.IsValid)
            {


                if (model.Title == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("CampaignNameNotEnter") }, JsonRequestBehavior.AllowGet);

                else if (model.Title.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("CampaignNameNotEnter") }, JsonRequestBehavior.AllowGet);






                RPB.Campaign business = new RPB.Campaign();
                business.Changed += CampaignBusiness_Changed;
                business.Save(model.toConvert());

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            return Json(new { ID = 0, Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult EditCampaign(long id)
        {

            TempData["EditCampaign"] = id;
            return RedirectToAction("Campaign");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteCampaign(List<Models.CampaignModel> model)
        {
            RPB.Campaign business = new RPB.Campaign();
            business.Changed += CampaignBusiness_Changed;
            business.Delete(model.Where(i => i.IsDelete == true).Select(i => new RPC.Campaign() { ID = i.ID }).ToList());

            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        private void CampaignBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }

        [AllowAnonymous]
        public ActionResult Getusers(long Company, long id)
        {

            List<RPC.User> list = new RPB.User().FindByCompany(Company);
            RPC.Campaign obj = null;
            if (id != 0)
                obj = new RPB.Campaign().FindByID(id);



            Models.CampaignModel c = new Models.CampaignModel();
            if (list != null)
            {
                foreach (RPC.User item in list)
                {

                    Models.CampaignAccessModel tmp = new Models.CampaignAccessModel() { User = item.ID, Name = item.Name };
                    if (obj != null)
                        if (obj.AccessList.FindIndex(i => i.User == item.ID) > -1)
                        {
                            tmp.IsSelected = true;
                        }

                    c.AccessList.Add(tmp);

                }
            }

            return PartialView("_UserList", c);

        }



        #endregion

        #region Company
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult CompanyList()
        {
            return View();
        }
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult Company()
        {
            Models.CompanyModel model = new Models.CompanyModel();
            model.GroupList = new RPB.OwnerGroup().All();
            if (TempData["EditCompany"] != null)
            {
                long id = (long)TempData["EditCompany"];

                RPC.Company obj = new RPB.Company().FindByID(id);
                model.GroupComment = obj.GroupComment;
                model.GroupID = obj.Group;
                model.GroupTitle = obj.GroupName;
                model.ID = obj.ID;
                model.Image = obj.Image;
                model.Tel = obj.Phone;
                model.Title = obj.Title;
                model.EnglishTitle = obj.EnglishTitle;



            }
            return View(model);
        }



        [HttpGet]
        public ActionResult CompaniesSearch(String keyword)
        {
            List<Models.CompanyModel> list = new RPB.Company().FilterBySearch(keyword).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();
            return PartialView("_CompanyGrid", list);
        }


        [HttpGet]
        public ActionResult Companies(int display, int start, int rowNo)
        {
            List<Models.CompanyModel> list = null;
            switch (display)
            {
                case 0:

                    if (rowNo == 0)
                        list = new RPB.Company().All().Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();

                    else

                        list = new RPB.Company().SelectRange(((start - 1) * rowNo + 1), rowNo).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();


                    break;
                case 1:

                    if (rowNo == 0)
                        list = new RPB.Company().FilterByPublish(true).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();

                    else

                        list = new RPB.Company().FilterByPublish(true, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();

                    break;
                case 2:

                    if (rowNo == 0)
                        list = new RPB.Company().FilterByPublish(false).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();

                    else

                        list = new RPB.Company().FilterByPublish(false, ((start - 1) * rowNo + 1), rowNo).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();


                    break;


            }




            return PartialView("_CompanyGrid", list);
        }


        [HttpGet]
        public ActionResult CompanyGrid(int start, int rowNo)
        {
            List<Models.CompanyModel> list = null;
            if (rowNo == 0)

                //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
                list = new RPB.Company().All().Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();
            else
                list = new RPB.Company().SelectRange(((start - 1) * rowNo), rowNo).Select(i => new Models.CompanyModel() { ID = i.ID, GroupID = i.Group, Image = i.Image, IsDelete = false, Tel = i.Phone, Title = i.Title }).ToList();


            return PartialView("_CompanyGrid", list);
        }

        [HttpGet]
        public int CompanyGridButton(int rowNo)
        {

            if (rowNo == 0)
                return 0;


            long count = new RPB.Company().Count();
            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);

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




        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult SaveCompany(Models.CompanyModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Title == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("CompanyNameNotEnter") }, JsonRequestBehavior.AllowGet);
                else if (model.Title.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("CompanyNameNotEnter") }, JsonRequestBehavior.AllowGet);



                RPB.Company business = new RPB.Company();
                business.Changed += CompanyBusiness_Changed;
                business.Save(model.toConvert());

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            return Json(new { ID = 0, Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult EditCompany(long id)
        {

            TempData["EditCompany"] = id;
            return RedirectToAction("Company");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteCompany(List<Models.CompanyModel> model)
        {
            RPB.Company business = new RPB.Company();
            business.Changed += CompanyBusiness_Changed;
            business.Delete(model.Where(i => i.IsDelete == true).Select(i => new RPC.Company() { ID = i.ID }).ToList());

            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        private void CompanyBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }

        #endregion

        #region Channel
        public ActionResult ChannelList()
        {
            return View();
        }
        public ActionResult Channel()
        {

            Models.ChannelModel model = new Models.ChannelModel();
            model.ProvinceList = new RPB.Province().All();
            model.ProvinceList.Insert(0, new RPC.Province { ID = 0, Title = LocalizedResource.ValueOf("Global") });

            if (TempData["EditChannel"] != null)
            {
                long id = (long)TempData["EditChannel"];

                RPC.Channel obj = new RPB.Channel().FindByID(id);
                model.Broadcast = (byte)((obj.Broadcast == null) ? 0 : obj.Broadcast);
                model.ID = obj.ID;
                model.Image = obj.Image;
                model.Title = obj.Title;
                model.EnglishTitle = obj.EnglishTitle;
                model.Media = (byte)((obj.Media == null) ? 0 : obj.Media);
                model.Province = (long)((obj.Province == null) ? 0 : obj.Province);

            }
            return View(model);


        }




        [HttpGet]
        public ActionResult ChannelsSearch(String keyword)
        {
            List<Models.ChannelModel> list = new RPB.Channel().FilterBySearch(keyword).Select(i => new Models.ChannelModel() { ID = i.ID, Image = i.Image, ImageAlt = "لوگوی کانال", Title = i.Title, IsDelete = false, EnglishTitle = i.EnglishTitle }).ToList();
            return PartialView("_ChannelGrid", list);
        }






        [HttpGet]
        public ActionResult ChannelGrid(int start, int rowNo)
        {
            List<Models.ChannelModel> list = null;
            if (rowNo == 0)

                //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);
                list = new RPB.Channel().All().Select(i => new Models.ChannelModel() { ID = i.ID, Image = i.Image, ImageAlt = LocalizedResource.ValueOf("ChannelLogo"), Title = i.Title, IsDelete = false, EnglishTitle = i.EnglishTitle }).ToList();
            else
                list = new RPB.Channel().SelectRange(((start - 1) * rowNo), rowNo).Select(i => new Models.ChannelModel() { ID = i.ID, Image = i.Image, ImageAlt = LocalizedResource.ValueOf("ChannelLogo"), Title = i.Title, IsDelete = false, EnglishTitle = i.EnglishTitle }).ToList();


            return PartialView("_ChannelGrid", list);
        }

        [HttpGet]
        public int ChannelGridButton(int rowNo)
        {

            if (rowNo == 0)
                return 0;


            long count = new RPB.Channel().Count();
            //int rowNo = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowNumber"]);

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




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveChannel(Models.ChannelModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Title == null)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("ChannelNameNotEnter") }, JsonRequestBehavior.AllowGet);
                else if (model.Title.Trim().Length == 0)
                    return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = LocalizedResource.ValueOf("ChannelNameNotEnter") }, JsonRequestBehavior.AllowGet);



                RPB.Channel business = new RPB.Channel();
                business.Changed += ChannelBusiness_Changed;
                business.Save(model.toConvert());

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            return Json(new { ID = 0, Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult EditChannel(long id)
        {

            TempData["EditChannel"] = id;
            return RedirectToAction("Channel");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public JsonResult DeleteChannel(List<Models.ChannelModel> model)
        {
            RPB.Channel business = new RPB.Channel();
            business.Changed += ChannelBusiness_Changed;
            business.Delete(model.Where(i => i.IsDelete == true).Select(i => new RPC.Channel() { ID = i.ID }).ToList());

            if (TempData["Result"] != null)
            {
                if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                {
                    return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }

        private void ChannelBusiness_Changed(object sender, RPC.Result result)
        {
            TempData["Result"] = result;
            TempData.Keep();
        }

        #endregion

        #region Common

        //private List<Models.FilesResult> GetImages(String path)
        //{
        //    string fullPath = Server.MapPath(String.Format("~{0}", path));
        //    string site = String.Empty;
        //    String[] files = null;
        //    try
        //    {
        //        site = new Business.Properties().FilterByProperty("Site").Value;
        //        files = System.IO.Directory.GetFiles(fullPath);
        //    }
        //    catch { }
        //    List<Models.FilesResult> list = new List<Models.FilesResult>();
        //    if (files != null)
        //    {
        //        foreach (String f in files)
        //        {

        //            String ff = System.IO.Path.GetFileName(f);
        //            list.Add(new Models.FilesResult() { Path = String.Format("{0}{1}{2}", site, path, ff) });
        //        }
        //    }
        //    return list;
        //}

        private String SaveIcons()
        {
            string fullPath = Server.MapPath("/Icons");
            string site = "../Icons";
            String fileName = String.Empty;
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {

                    var file = Request.Files[i];

                    fileName = Path.GetFileName(file.FileName);
                    String[] ff = fileName.Split('.');

                    int count = GetIcons().Count;


                    fileName = String.Format("{0} ({1}).{2}", "Icon", ++count, ff[ff.Length - 1]);


                    String full = Path.Combine(fullPath, fileName);
                    bool cont = true;
                    while (cont)
                    {
                        try
                        {
                            file.SaveAs(full);
                            cont = false;
                        }
                        catch { cont = true; }
                    }

                    String fff = System.IO.Path.GetFileName(full);
                    fff = String.Format("{0}/{1}", site, fff);

                    fileName = fff;


                }

            }
            catch { fileName = String.Empty; }
            return fileName;
        }

        public ActionResult IconGrid()
        {
            List<string> list = GetIcons();

            return PartialView("_ImageViewer", list);
        }
        public int IconGridButton()
        {

            int rowNo = 10;


            long count = GetIcons().Count;

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


        private String SaveImages()
        {
            string fullPath = Server.MapPath("/Image");
            string site = "../Image";
            String fileName = String.Empty;
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {

                    var file = Request.Files[i];

                    fileName = Path.GetFileName(file.FileName);
                    String[] ff = fileName.Split('.');

                    int count = GetIcons().Count;


                    fileName = String.Format("{0} ({1}).{2}", "Image", ++count, ff[ff.Length - 1]);


                    String full = Path.Combine(fullPath, fileName);
                    bool cont = true;
                    while (cont)
                    {
                        try
                        {
                            file.SaveAs(full);
                            cont = false;
                        }
                        catch { cont = true; }
                    }

                    String fff = System.IO.Path.GetFileName(full);
                    fff = String.Format("{0}/{1}", site, fff);

                    fileName = fff;


                }

            }
            catch { fileName = String.Empty; }
            return fileName;
        }


        public ActionResult ImageGrid()
        {
            List<string> list = GetImages();

            return PartialView("_ImageViewer", list);
        }
        public int ImageGridButton()
        {

            int rowNo = 10;


            long count = GetImages().Count;

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


        [HttpPost]
        public String UploadFile()
        {

            return SaveIcons();
        }

        public String UploadImage()
        {

            return SaveImages();
        }

        public List<String> GetIcons()
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
                    list.Add(String.Format("{0}/{1}", site, ff));
                }
            }
            return list;

        }


        public List<String> GetImages()
        {
            string fullPath = Server.MapPath("/Image");
            string site = String.Empty;
            String[] files = null;
            try
            {
                site = "../Image";
                files = System.IO.Directory.GetFiles(fullPath);
            }
            catch { }
            List<String> list = new List<String>();
            if (files != null)
            {
                foreach (String f in files)
                {

                    String ff = System.IO.Path.GetFileName(f);
                    list.Add(String.Format("{0}/{1}", site, ff));
                }
            }
            return list;

        }
        #endregion


        #region Foretaste
        [CustomAuthorize(Models.Access.Admin, Models.Access.Manager)]
        public ActionResult Foretaste()
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
                for (int i=0;i<list.Count;i++)
                {
                    Models.ForetasteModel f = new Models.ForetasteModel();
                    f.toConvert(list[i]);
                    model.Add(f);
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveForetaste(List<Models.ForetasteModel> model)
        {
            if (ModelState.IsValid)
            {
                RPB.Foretaste business = new RPB.Foretaste();
                List<RPC.Foretaste> list = new List<RPC.Foretaste>();
                foreach(Models.ForetasteModel item in model)
                {
                    list.Add(item.toConvert());
                }
                business.Save(list);
                business.Changed += UserBusiness_Changed;              

                if (TempData["Result"] != null)
                {
                    if (((RPC.Result)TempData["Result"]).RetUpshot == RPC.Upshot.Success)
                    {
                        return Json(new { ID = ((RPC.Result)TempData["Result"]).RetID, Status = "0", Title = @LocalizedResource.ValueOf("Message"), Message = @LocalizedResource.ValueOf("Success") }, JsonRequestBehavior.AllowGet);
                    }
                }
                
            }
            return Json(new { Status = "999", Title = LocalizedResource.ValueOf("Warning"), Message = @LocalizedResource.ValueOf("Fault") }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}