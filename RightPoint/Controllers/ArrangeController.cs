using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RightPoint.Controllers
{


    [CustomAuthorize(Models.Access.Manager, Models.Access.User, Models.Access.Client)]
    public class ArrangeController : Controller
    {
        // GET: Arrange



        public ActionResult Custom()
        {


            Models.CustmoModel model = new Models.CustmoModel();

            if (RPC.Common.CurrentUser != null)
            {

                model.MediaList = new RPB.Media().All();
                model.CampaignList = new RPB.Campaign().FilterByUser(RPC.Common.CurrentUser.ID);
                model.ContextTypeList = new RPB.ContextType().All();
                model.ContextTypeList.Insert(0, new RPC.ContextType() { ID = 0, Title = Resource_fa.General });
                model.CityList = new RPB.City().All();

            }
            return View(model);
        }

        public ActionResult ChangeMedia(byte media)
        {


            Models.CustmoModel model = new Models.CustmoModel();
            //model.ACCList = new RPB.ACC().FilterByMedia(1);
            //model.AMDList = new RPB.AMD().FilterByMedia(1);
            model.LocationList = new RPB.Location().FilterByMedia(media);
            model.PositionList = new RPB.Position().FilterByMedia(media);
            model.ChannelList = new RPB.Channel().FilterByMedia_Broadcast(media, (byte)Models.Broadcast.Province);
            model.ChannelList.Insert(0, new RPC.Channel() { ID = 0, Title = Resource_fa.Global });

            return PartialView("_Custom", model);


        }

        public ActionResult ChangeCampaign(long campaign)
        {
            Models.CustmoModel model = new Models.CustmoModel();
            if (campaign > 0)
            {

                RPC.Campaign item = new RPB.Campaign().FindByID(campaign);
                if (item.Questionnaire != null)
                {
                    return Content(item.Description);
                }
                else
                {

                    return PartialView("_Custom2", model);
                }

            }

            return Content(String.Empty);




        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(Models.CustmoModel model, string run)
        {
            TempData.Clear();
            if (run.Equals("1"))
            {
                TempData["Result"] = model;
                TempData.Keep("Result");

                return Content(String.Empty);
            }
            else
            {

                long v = 0;
                if (model.Result.Campaign != null)
                {

                    long campaignID = (long)model.Result.Campaign;
                    RPC.Campaign item = new RPB.Campaign().FindByID(campaignID);

                    RPC.Kernel mm = new RPB.Arrange().Viwers(campaignID, (model.Result.MaritalStatus != null) ? model.Result.MaritalStatus.ToList() : null, (model.Result.Education != null) ? model.Result.Education.ToList() : null, (model.Result.Ages != null) ? model.Result.Ages.ToList() : null, (model.Result.Occupation != null) ? model.Result.Occupation.ToList() : null, (model.Result.FamilyIncome != null) ? model.Result.FamilyIncome.ToList() : null, (model.Result.Gender != null) ? model.Result.Gender.ToList() : null);
                    v = mm.Viewer;

                    return Content(v.ToString());
                }

                return Content(String.Empty);
            }
        }


        public ActionResult AutorizeList()
        {
            List<RPC.Kernel> tmpList = new List<RPC.Kernel>();
            if (TempData["Result"] != null)
            {
                TempData.Keep("Result");
                Models.CustmoModel model = (Models.CustmoModel)TempData["Result"];
                RPB.Arrange arrange = new RPB.Arrange();

                if (model.Result.Campaign != null)
                {
                    RPC.Campaign campaign = new RPB.Campaign().FindByID((long)model.Result.Campaign);
                    List<RPC.Kernel> list = arrange.AutorizeList((long)model.Result.Media, campaign.ID, (long)model.Result.Channel, (model.Result.MaritalStatus != null) ? model.Result.MaritalStatus.ToList() : null, (model.Result.Education != null) ? model.Result.Education.ToList() : null, (model.Result.Ages != null) ? model.Result.Ages.ToList() : null, (model.Result.Occupation != null) ? model.Result.Occupation.ToList() : null, (model.Result.FamilyIncome != null) ? model.Result.FamilyIncome.ToList() : null, (model.Result.Gender != null) ? model.Result.Gender.ToList() : null, (model.Result.City != null) ? model.Result.City.ToList() : null, model.Result.Location, model.Result.DataRange, model.Result.TimeRange, model.Result.OverLap);
                    TempData["List"] = arrange.CreateList(list, model.Result.Media, model.Result.Location, campaign.ACC, model.Result.Position, campaign.Lottary, model.Result.ContextType, model.Result.TimeRange, model.Result.ClipTime, model.Result.ContextType, model.Result.Budget, model.Result.ExteraPlay);
                    TempData.Keep("List");
                }
            }

            return View();
        }

        public ActionResult Result()
        {

            Models.KernelModel tmp = new Models.KernelModel();
            List<RPC.Kernel> tmpList = new List<RPC.Kernel>();
            if (TempData["Result"] != null)
            {
                TempData.Keep("Result");
                Models.CustmoModel model = (Models.CustmoModel)TempData["Result"];
                RPB.Arrange arrange = new RPB.Arrange();

                tmp.Budget = model.Result.Budget;
                tmp.TimeRange = model.Result.TimeRange;
                tmp.DataRange = model.Result.DataRange;

                if (model.Result.Campaign != null)
                {
                    RPC.Campaign campaign = new RPB.Campaign().FindByID((long)model.Result.Campaign);

                    List<RPC.Kernel> list = null;
                    if (TempData["List"] != null)
                        list = (List<RPC.Kernel>)TempData["List"];


                    tmpList = arrange.PickUp(list, model.Result.ClipTime, model.Result.ContextType, model.Result.Budget, model.Result.ExteraPlay, model.Result.Rep);
                    if (tmpList != null)
                    {
                        foreach (RPC.Kernel item in tmpList)
                        {
                            list.RemoveAll(i => i.Program == item.Program && i.Month == item.Month);
                        }

                        foreach (RPC.Kernel item in list)
                        {
                            tmpList.Add(new RPC.Kernel()
                            {
                                ChannelName = item.ChannelName,
                                ClassNo = item.ClassNo,
                                CPS = item.CPS,
                                CPSWD = item.CPSWD,
                                Frequency = item.Frequency,
                                IsAdded = true,
                                IsDeleted = false,
                                Limit = item.Limit,
                                Month = item.Month,
                                PlayTime = item.PlayTime,
                                ProgamTitle = item.ProgamTitle,
                                ProgamEnglishTitle = item.ProgamEnglishTitle,
                                Program = item.Program,
                                Viewer = item.Viewer,
                                IsExtra = true,
                                MaxFequency = item.MaxFequency,
                                PremiumFrequency = item.PremiumFrequency,
                                Location = item.Location,
                                MonthName = item.MonthName,
                                Position = item.Position,
                                PPS = item.PPS,
                                AllViewer = item.AllViewer,
                                Reach = item.Reach,
                                Count = item.Count,
                                LocationID = item.LocationID,
                                LocationList = item.LocationList,
                                PositionID = item.PositionID,
                                PositionList = item.PositionList,
                                ClipTime = item.ClipTime,
                                Extra = item.Extra,
                                MinRatePoint = item.MinRatePoint,
                                Middle = item.Middle,
                                After = item.After,
                                Before = item.Before

                            });
                        }
                    }
                }

            }
            tmp.AddList(tmpList);


            TempData["Data"] = tmp;
            TempData.Keep("Data");
            return View(tmp);
        }


        public ActionResult ResultSearch(String keyword)
        {
            Models.KernelModel tmp = new Models.KernelModel();
            List<RPC.Kernel> tmpList = new List<RPC.Kernel>();
            if (TempData["Result"] != null)
            {
                TempData.Keep("Result");
                Models.CustmoModel model = (Models.CustmoModel)TempData["Result"];
                RPB.Arrange arrange = new RPB.Arrange();

                if (model.Result.Campaign != null)
                {
                    RPC.Campaign campaign = new RPB.Campaign().FindByID((long)model.Result.Campaign);

                    List<RPC.Kernel> list = arrange.AutorizeList((long)model.Result.Media, campaign.ID, (long)model.Result.Location, keyword, model.Result.TimeRange);

                    List<RPC.Kernel> list2 = arrange.CreateList(list, model.Result.Media, model.Result.Location, model.Result.ACC, model.Result.Position, model.Result.Lottary, model.Result.ContextType, model.Result.TimeRange, model.Result.ClipTime, model.Result.ContextType, model.Result.Budget, model.Result.ExteraPlay);

                    tmpList = list2; //arrange.CreateModel(list2, model.Result.ClipTime, model.Result.ContextType, model.Result.Budget, model.Result.ExteraPlay);
                }
            }
            tmp.AddList(tmpList);
            TempData["Search"] = tmp;
            TempData.Keep("Search");
            return PartialView("_ResultSearchGrid", tmp);

        }

        public ActionResult ReResultSearch()
        {

            if (TempData["Search"] != null)
            {

                TempData.Keep("Search");

                Models.KernelModel search = ((Models.KernelModel)TempData["Search"]);

                return PartialView("_ResultSearchGrid", search);
            }


            return null;
        }

        public ActionResult ReCalculates(long budget, byte timeRange, byte dataRange)
        {
            if (TempData["Result"] != null)
            {
                Models.CustmoModel model = (Models.CustmoModel)TempData["Result"];
                model.Result.Budget = budget;
                model.Result.TimeRange = timeRange;
                model.Result.DataRange = dataRange;
                TempData["Result"] = model;
                TempData.Keep("Result");

                RPB.Arrange arrange = new RPB.Arrange();

                if (model.Result.Campaign != null)
                {
                    RPC.Campaign campaign = new RPB.Campaign().FindByID((long)model.Result.Campaign);

                    List<RPC.Kernel> list = arrange.AutorizeList((long)model.Result.Media, campaign.ID, (long)model.Result.Channel, (model.Result.MaritalStatus == null) ? null : model.Result.MaritalStatus.ToList(), (model.Result.Education == null) ? null : model.Result.Education.ToList(), (model.Result.Ages == null) ? null : model.Result.Ages.ToList(), (model.Result.Occupation == null) ? null : model.Result.Occupation.ToList(), (model.Result.FamilyIncome == null) ? null : model.Result.FamilyIncome.ToList(), (model.Result.Gender == null) ? null : model.Result.Gender.ToList(), (model.Result.City == null) ? null : model.Result.City.ToList(), model.Result.Location, model.Result.DataRange, model.Result.TimeRange, model.Result.OverLap);

                    TempData["List"] = arrange.CreateList(list, model.Result.Media, model.Result.Location, campaign.ACC, model.Result.Position, campaign.Lottary, model.Result.ContextType, model.Result.TimeRange, model.Result.ClipTime, model.Result.ContextType, model.Result.Budget, model.Result.ExteraPlay);
                    TempData.Keep("List");


                }
            }


            return null;
        }

        public ActionResult DeleteResult(long id, int month)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                RPC.Kernel item = model.Items.FirstOrDefault(i => i.Program == id && i.Month == month);
                item.IsDeleted = true;
                item.IsAdded = false;
                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);
                tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted != true).ToList();


                return PartialView("_ResultGrid", tempModel);
            }

            return null;
        }
        public ActionResult DeleteBackResult(long id, int month)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                RPC.Kernel item = model.Items.FirstOrDefault(i => i.Program == id && i.Month == month);
                item.IsDeleted = false;
                item.IsAdded = false;

                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);

                tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted != true).ToList();

                return PartialView("_ResultGrid", tempModel);
            }

            return null;
        }

        public ActionResult DeleteAddResult(long id, int month)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                RPC.Kernel item = model.Items.FirstOrDefault(i => i.Program == id && i.Month == month);
                item.IsDeleted = false;
                item.IsAdded = true;
                item.IsExtra = true;
                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);

                tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted != true).ToList();

                return PartialView("_ResultGrid", tempModel);
            }

            return null;
        }

        public ActionResult AddResult(long id, int month, long frequency)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                RPC.Kernel item = model.Items.FirstOrDefault(i => i.Program == id && i.Month == month);
                item.IsDeleted = false;
                item.IsAdded = false;
                item.Frequency = frequency;
                if (frequency > item.MaxFequency)
                    item.Frequency = item.MaxFequency;


                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);

                tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted != true).ToList();

                return PartialView("_ResultGrid", model);
            }

            return null;
        }

        public ActionResult AddSearch(long id, int month, long frequency)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                if (TempData["Search"] != null)
                {

                    TempData.Keep("Search");
                    Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                    Models.KernelModel search = ((Models.KernelModel)TempData["Search"]);

                    RPC.Kernel item = search.Items.FirstOrDefault(i => i.Program == id && i.Month == month);
                    if (item != null)
                    {
                        item.IsDeleted = false;
                        item.IsAdded = false;
                        item.IsUserSelect = true;
                        item.Frequency = frequency;
                        if (frequency > item.MaxFequency)
                            item.Frequency = item.MaxFequency;


                        search.Items.Remove(item);
                        model.Items.RemoveAll(i => i.Program == item.Program && i.Month == item.Month);
                        model.Items.Add(item);

                    }
                    TempData["Data"] = model;
                    TempData["Search"] = search;
                    TempData.Keep("Data");
                    TempData.Keep("Result");
                    TempData.Keep("Search");

                    Models.KernelModel tempModel = new Models.KernelModel();
                    tempModel.AddList(model.Items);
                    tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted != true).ToList();
                    return PartialView("_ResultGrid", tempModel);
                }
            }

            return null;
        }


        public ActionResult LocationChange(long program, long location, int month)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");
                Models.CustmoModel model2 = (Models.CustmoModel)TempData["Result"];
                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                int idx = model.Items.FindIndex(i => i.Program == program && i.Month == month);
                if (idx != -1)
                {
                    RPC.Kernel k = model.Items[idx];

                    if (k != null)
                    {
                        RPC.Kernel tmp = new RPB.Arrange().CreatItem(k, model2.Result.Media, location, model2.Result.ACC, k.PositionID, model2.Result.Lottary, month, model2.Result.ClipTime, model2.Result.ContextType, model2.Result.Budget, model2.Result.ExteraPlay);
                        model.Items[idx].LocationID = location;
                        model.Items[idx].PPS = tmp.PPS;
                        model.Items[idx].CPS = tmp.CPS;
                        TempData["Data"] = model;
                        TempData.Keep("Data");
                    }
                }
                return PartialView("_ResultGrid", model);
            }

            return null;

        }

        public ActionResult PositionChange(long program, long position, int month)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");
                Models.CustmoModel model2 = (Models.CustmoModel)TempData["Result"];
                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                int idx = model.Items.FindIndex(i => i.Program == program && i.Month == month);
                if (idx != -1)
                {
                    RPC.Kernel k = model.Items[idx];
                    if (k != null)
                    {
                        RPC.Kernel tmp = new RPB.Arrange().CreatItem(k, model2.Result.Media, k.LocationID, model2.Result.ACC, position, model2.Result.Lottary, month, model2.Result.ClipTime, model2.Result.ContextType, model2.Result.Budget, model2.Result.ExteraPlay);
                        model.Items[idx].Position = tmp.Position;
                        model.Items[idx].PositionID = tmp.PositionID;
                        model.Items[idx].PositionList = tmp.PositionList;
                        model.Items[idx].PPS = tmp.PPS;
                        model.Items[idx].CPS = tmp.CPS;
                        TempData["Data"] = model;
                        TempData.Keep("Data");
                        model = ((Models.KernelModel)TempData["Data"]);
                    }
                }


                return PartialView("_ResultGrid", model);

            }
            return null;

        }


        public ActionResult FrequencyChange(long program, long frequency, int month)
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");
                Models.CustmoModel model2 = (Models.CustmoModel)TempData["Result"];
                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                int idx = model.Items.FindIndex(i => i.Program == program && i.Month == month);
                if (idx != -1)
                {
                    RPC.Kernel k = model.Items[idx];
                    if (k != null)
                    {
                        //RPC.Kernel tmp = new RPB.Arrange().CreatItem(k, model2.Result.Media, k.LocationID, model2.Result.ACC, position, model2.Result.Lottary, month, model2.Result.ClipTime, model2.Result.ContextType, model2.Result.Budget, model2.Result.ExteraPlay);
                        if (frequency < model.Items[idx].MaxFequency)
                            model.Items[idx].Frequency = frequency;
                        else
                            model.Items[idx].Frequency = model.Items[idx].MaxFequency;
                        TempData["Data"] = model;
                        TempData.Keep("Data");
                        model = ((Models.KernelModel)TempData["Data"]);
                    }
                }


                return PartialView("_ResultGrid", model);

            }
            return null;

        }

        public ActionResult DeleteTable()
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);




                tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted == true).ToList();

                return PartialView("_ResultDeleteGrid", tempModel);
            }

            return null;
        }

        public ActionResult AddTable()
        {
            if ((TempData["Data"] != null))
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);

                tempModel.Items = tempModel.Items.Where(i => i.IsAdded == true).ToList();
                return PartialView("_ResultExtraGrid", tempModel);
            }

            return null;
        }

        public ActionResult ReCalculate()
        {
            if (TempData["Data"] != null)
            {
                TempData.Keep("Data");
                TempData.Keep("Result");

                Models.KernelModel model = ((Models.KernelModel)TempData["Data"]);
                Models.KernelModel tempModel = new Models.KernelModel();
                tempModel.AddList(model.Items);



                tempModel.Items = tempModel.Items.Where(i => i.IsAdded != true && i.IsDeleted != true).ToList();



                return PartialView("_ResultCalaulate", tempModel);
            }
            return null;
        }


        public ActionResult Print()
        {

            TempData.Keep("Result");
            TempData.Keep("Data");
            if (TempData["Result"] != null)
            {
                TempData.Keep("Result");
                Models.CustmoModel rr = (Models.CustmoModel)TempData["Result"];
                rr.CampaignList = new RPB.Campaign().FilterByUser(RPC.Common.CurrentUser.ID);
                rr.ContextTypeList = new RPB.ContextType().All();
                rr.ContextTypeList.Insert(0, new RPC.ContextType() { ID = 0, Title = Resource_fa.General });
                rr.CityList = new RPB.City().All();
                rr.LocationList = new RPB.Location().All();
                rr.PositionList = new RPB.Position().All();
                rr.LottaryList = new RPB.Lottary().All();
                rr.ChannelList = new RPB.Channel().All();
                rr.ACCList = new RPB.ACC().All();
                rr.ChannelList = new RPB.Channel().All();
                
            }


            SendEmail();
            return View();
        }

        public void SendEmail()
        {

            String smtp = "Mail.Rightpoint.ir";
            int smtp_Port = 25;
            String from = "App@rightpoint.ir";
            String title = "RightPoint Application";
            string pass = "mam@03421";
            List<string> bcc = new List<string>();
            bcc.Add("Info@rightpoint.ir");

            RPC.Common.SendEmail(from, null, null, bcc, "123", "test", null, title, pass, true, smtp, smtp_Port);



        }
    }
}