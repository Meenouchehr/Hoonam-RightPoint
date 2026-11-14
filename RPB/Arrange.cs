using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using Generic;

namespace RPB
{
    public class Arrange
    {

        private List<Decision> decisions;
        public Arrange()
        {
            decisions = new List<Decision>();
        }

        private Model CreateModel(SolverContext context, List<RPC.Kernel> items, int clipTime, long? content, long budget, int extraPlay, int rep)
        {
            Model model = context.CreateModel();
            String cast = String.Empty;
            String frequency = String.Empty;
            String frequency2 = String.Empty;
            String viewer = String.Empty;




            if (items != null)
            {




                foreach (RPC.Kernel k in items)
                {


                    if (k.CPS > 0 && k.Frequency > 0 && k.RatePoint > 0 && k.RatePoint >= k.MinRatePoint)
                    {

                        //double val = (double)((k.CPS * 100) / (100 + extraPlay));
                        //k.CPS = Math.Ceiling(val);



                        String name = String.Format("tag{0}_{1}", k.Month, k.Program);

                        Decision d = new Decision(Domain.IntegerNonnegative, name);
                        model.AddDecision(d);
                        if (String.IsNullOrEmpty(cast))
                        {
                            cast = String.Format("{0} * {1}", k.CPS, name);
                        }
                        else
                        {
                            cast += String.Format("+ {0} * {1}", k.CPS, name);
                        }



                        model.AddConstraint(String.Format("{0}_Freq", name), 0 <= d <= ((rep > 0) ? ((rep <= k.Frequency) ? rep : k.Frequency) : k.Frequency));








                        if (String.IsNullOrEmpty(viewer))
                        {
                            viewer = String.Format("{0} * {1}", (k.RatePoint == 0) ? 0.1 : k.RatePoint, name);
                        }
                        else
                        {
                            viewer += String.Format("+ {0} * {1}", (k.RatePoint == 0) ? 0.1 : k.RatePoint, name);
                        }

                    }
                }


                if (budget == 0)
                    budget = (long)999999999999999999;

                if (!String.IsNullOrEmpty(cast))
                {
                    cast += String.Format(" <= {0}", budget);
                    model.AddConstraint("cast", cast);
                }

                if (!String.IsNullOrEmpty(viewer))
                {

                    model.AddGoal("viwer", GoalKind.Maximize, viewer);
                    return model;
                }


            }
            return null;
        }

        public List<RPC.Kernel> CreateModel(List<RPC.Kernel> items, int clipTime, long? content, long budget, int extraPlay)
        {
            if (items != null)
            {

                foreach (RPC.Kernel k in items)
                {


                    switch (content)
                    {
                        case 1:
                            if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                                k.CPS *= 40;

                            break;
                        case 2:

                            if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                                k.CPS *= 40;

                            if ((clipTime >= 30 && clipTime < 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                                k.CPS *= (clipTime - 5);


                            break;
                        case 3:
                            if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                                k.CPS *= 40;
                            break;
                        case 4:
                            if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                                k.CPS *= 40;
                            break;
                        case 5:
                            if ((clipTime >= 40) && (k.media == 1) && (k.group == 2 || k.group == 3))
                                k.CPS *= (clipTime - 5);
                            break;
                        default:
                            k.CPS *= clipTime;
                            break;

                    }



                    k.CPSWD = k.CPS;
                    double ep = (extraPlay) + (k.SeriesRedundant);
                    if (k.Province != 0)
                    {
                        double kk = (ep);
                        if (k.Series == 1)
                            kk *= 1.2;
                        else if (k.Series == 2)
                            kk *= 1.5;
                        else
                            kk *= 2;

                        kk += (100 - k.ProvinceRedundant);

                        double val = (double)((k.CPS * 100) / (100 + kk));
                        k.CPS = Math.Ceiling(val);
                    }
                    else
                    {
                        double val = (double)((k.CPS * 100) / (100 + ep));
                        k.CPS = Math.Ceiling(val);
                    }

                    k.ClipTime = clipTime;

                }



                return items;
            }
            return null;
        }
        public List<RPC.Kernel> CreateList(List<RPC.Kernel> list, byte? media, long? location, long? acc, long? position, long? lottary, long? context, byte weeks, int clipTime, long? content, long budget, int extraPlay)
        {
            if (list == null)
                return null;
            if (list.Count == 0)
                return null;

            int days = weeks * 7;

            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(days);

            list = GetWeekRep(list, start, end);

            List<RPD.TBL_ContractMD> listCMD = RPD.ContractMD.ALL().ToList();

            RPC.Location _Location = null;
            RPC.ACC _ACC = null;
            RPC.AMD _SAMD = null;
            RPC.AMD _EAMD = null;
            RPC.Position _Position = null;
            RPC.Lottary _Lottary = null;
            RPD.TBL_ContractMD _ContractMD1 = null;
            RPD.TBL_ContractMD _ContractMD2 = null;

            List<RPC.Location> locations = new RPB.Location().FilterByMedia((byte)media);
            List<RPC.Position> Positions = new RPB.Position().FilterByMedia((byte)media);

            if (location != null)
                _Location = new RPB.Location().FindByID((long)location);
            if (acc != null)
                _ACC = new RPB.ACC().FindByID((long)acc);
            if (position != null)
                _Position = new RPB.Position().FindByID((long)position);
            if (lottary != null)
                _Lottary = new RPB.Lottary().FindByID((long)lottary);

            int sm = start.GetPersianMonth();
            int em = end.GetPersianMonth();

            _SAMD = new RPB.AMD().FindByMediaMonth((byte)media, (byte)sm);
            _EAMD = new RPB.AMD().FindByMediaMonth((byte)media, (byte)em);


            foreach (RPC.Kernel k in list)
            {

                _ContractMD1 = listCMD.FirstOrDefault(i => i.FLD_Month == (byte)sm && i.FLD_ContractID_FK == k.Contract);
                _ContractMD2 = listCMD.FirstOrDefault(i => i.FLD_Month == (byte)em && i.FLD_ContractID_FK == k.Contract);


                double price = k.PPS;


                double coffe = (double)((k.Extra == 0) ? 1 : 1 + ((double)((k.Extra) / 100)));
                price *= coffe;



                if (_Location != null)
                {
                    k.LocationList = locations;
                    k.LocationID = _Location.ID;
                    k.Location = RPC.Common.Language.ToLower().Trim().Contains("persian") ? _Location.Title : _Location.EnglishTitle;
                    if (_Location.RedundantType == (byte)RPC.RedundantType.Coefficient)
                        price *= (double)((_Location.Redundant == null) ? 1 : _Location.Redundant);
                    else if (_Location.RedundantType == (byte)RPC.RedundantType.Percentage)
                    {
                        double coff = (double)((_Location.Redundant == null) ? 1 : 1 + ((double)((_Location.Redundant) / 100)));
                        price *= coff;
                    }
                }
                if (_ACC != null)
                    if (_ACC.RedundantType == (byte)RPC.RedundantType.Coefficient)
                        price *= (double)((_ACC.Redundant == null) ? 1 : _ACC.Redundant);
                    else if (_ACC.RedundantType == (byte)RPC.RedundantType.Percentage)
                    {
                        double coff = (double)((_ACC.Redundant == null) ? 1 : 1 + ((double)(_ACC.Redundant) / 100));
                        price *= coff;
                    }




                if (_Position != null)
                {
                    k.PositionList = Positions;
                    k.Position = RPC.Common.Language.ToLower().Trim().Contains("persian") ? _Position.Title : _Position.EnglishTitle;
                    k.PositionID = _Position.ID;
                    if (_Position.RedundantType == (byte)RPC.RedundantType.Coefficient)
                        price *= (double)((_Position.Redundant == null) ? 1 : _Position.Redundant);
                    else if (_Position.RedundantType == (byte)RPC.RedundantType.Percentage)
                    {
                        double coff = (double)((_Position.Redundant == null) ? 1 : 1 + ((double)(_Position.Redundant) / 100));
                        price *= coff;
                    }
                }

                if (_Lottary != null)
                {
                    if (_Lottary.RedundantType == (byte)RPC.RedundantType.Coefficient)
                        price *= (double)((_Lottary.Redundant == null) ? 1 : _Lottary.Redundant);
                    else if (_Lottary.RedundantType == (byte)RPC.RedundantType.Percentage)
                    {
                        double coff = (double)((_Lottary.Redundant == null) ? 1 : 1 + ((double)(_Lottary.Redundant) / 100));
                        price *= coff;
                    }
                }


                double tempPrice = price;

                if (k.Month == sm)
                {
                    if (_SAMD != null)
                        if (_SAMD.RedundantType == (byte)RPC.RedundantType.Coefficient)
                            price *= (double)((_SAMD.Redundant == null) ? 1 : _SAMD.Redundant);
                        else if (_SAMD.RedundantType == (byte)RPC.RedundantType.Percentage)
                        {
                            double coff = (double)((_SAMD.Redundant == null) ? 1 : 1 + ((double)(_SAMD.Redundant) / 100));
                            price *= coff;
                        }


                    if (_ContractMD1 != null)
                        if (_ContractMD1.FLD_Type == (byte)RPC.RedundantType.Coefficient)
                            price *= (double)((_ContractMD1.FLD_Redundant == null) ? 1 : _ContractMD1.FLD_Redundant);
                        else if (_ContractMD1.FLD_Type == (byte)RPC.RedundantType.Percentage)
                        {
                            double coff = (double)((_ContractMD1.FLD_Redundant == null) ? 1 : 1 + ((double)(_ContractMD1.FLD_Redundant) / 100));
                            price *= coff;
                        }



                }
                else if (k.Month == em)
                {
                    if (_EAMD != null)
                        if (_EAMD.RedundantType == (byte)RPC.RedundantType.Coefficient)
                            price *= (double)((_EAMD.Redundant == null) ? 1 : _EAMD.Redundant);
                        else if (_EAMD.RedundantType == (byte)RPC.RedundantType.Percentage)
                        {
                            double coff = (double)((_EAMD.Redundant == null) ? 1 : 1 + ((double)(_EAMD.Redundant) / 100));
                            price *= coff;
                        }



                    if (_ContractMD2 != null)
                        if (_ContractMD2.FLD_Type == (byte)RPC.RedundantType.Coefficient)
                            price *= (double)((_ContractMD2.FLD_Redundant == null) ? 1 : _ContractMD2.FLD_Redundant);
                        else if (_ContractMD2.FLD_Type == (byte)RPC.RedundantType.Percentage)
                        {
                            double coff = (double)((_ContractMD2.FLD_Redundant == null) ? 1 : 1 + ((double)(_ContractMD2.FLD_Redundant) / 100));
                            price *= coff;
                        }
                }

                k.CPS = price;

                //   k.CPS = Math.Round(k.CPS * 100 / (100 + extraPlay), MidpointRounding.ToEven);


                switch (content)
                {
                    case 1:
                        if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                            k.CPS *= 40;

                        break;
                    case 2:

                        if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                            k.CPS *= 40;

                        if ((clipTime >= 30 && clipTime < 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                            k.CPS *= (clipTime - 5);


                        break;
                    case 3:
                        if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                            k.CPS *= 40;
                        break;
                    case 4:
                        if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                            k.CPS *= 40;
                        break;
                    case 5:
                        if ((clipTime >= 40) && (k.media == 1) && (k.group == 2 || k.group == 3))
                            k.CPS *= (clipTime - 5);
                        break;
                    default:
                        k.CPS *= clipTime;
                        break;

                }

                k.CPSWD = k.CPS;
                double ep = (extraPlay) + (k.SeriesRedundant);
                if (k.Province != 0)
                {
                    double kk = (ep);
                    if (k.Series == 1)
                        kk *= 1.2;
                    else if (k.Series == 2)
                        kk *= 1.5;
                    else
                        kk *= 2;

                    kk += (100 - k.ProvinceRedundant);

                    double val = (double)((k.CPS * 100) / (100 + kk));
                    k.CPS = Math.Ceiling(val);
                }
                else
                {
                    double val = (double)((k.CPS * 100) / (100 + ep));
                    k.CPS = Math.Ceiling(val);
                }

                k.ClipTime = clipTime;

            }


            return list;

        }
        public RPC.Kernel CreatItem(RPC.Kernel item, byte? media, long? location, long? acc, long? position, long? lottary, int month, int clipTime, long? content, long budget, int extraPlay)
        {
            if (item == null)
                return null;



            List<RPD.TBL_ContractMD> listCMD = RPD.ContractMD.ALL().ToList();

            RPC.Location _Location = null;
            RPC.ACC _ACC = null;
            RPC.AMD _SAMD = null;
            RPC.Position _Position = null;
            RPC.Lottary _Lottary = null;
            RPD.TBL_ContractMD _ContractMD1 = null;


            List<RPC.Location> locations = new RPB.Location().FilterByMedia((byte)media);
            List<RPC.Position> Positions = new RPB.Position().FilterByMedia((byte)media);

            if (location != null)
                _Location = new RPB.Location().FindByID((long)location);
            if (acc != null)
                _ACC = new RPB.ACC().FindByID((long)acc);
            if (position != null)
                _Position = new RPB.Position().FindByID((long)position);
            if (lottary != null)
                _Lottary = new RPB.Lottary().FindByID((long)lottary);

            int sm = month;


            _SAMD = new RPB.AMD().FindByMediaMonth((byte)media, (byte)sm);








            RPC.Kernel k = item;


            _ContractMD1 = listCMD.FirstOrDefault(i => i.FLD_Month == (byte)sm && i.FLD_ContractID_FK == k.Contract);


            double price = k.PPS;


            double coffe = (double)((k.Extra == 0) ? 1 : 1 + ((double)((k.Extra) / 100)));
            price *= coffe;



            if (_Location != null)
            {
                k.LocationList = locations;
                k.LocationID = _Location.ID;
                k.Location = _Location.Title;
                if (_Location.RedundantType == (byte)RPC.RedundantType.Coefficient)
                    price *= (double)((_Location.Redundant == null) ? 1 : _Location.Redundant);
                else if (_Location.RedundantType == (byte)RPC.RedundantType.Percentage)
                {
                    double coff = (double)((_Location.Redundant == null) ? 1 : 1 + ((double)((_Location.Redundant) / 100)));
                    price *= coff;
                }
            }
            if (_ACC != null)
                if (_ACC.RedundantType == (byte)RPC.RedundantType.Coefficient)
                    price *= (double)((_ACC.Redundant == null) ? 1 : _ACC.Redundant);
                else if (_ACC.RedundantType == (byte)RPC.RedundantType.Percentage)
                {
                    double coff = (double)((_ACC.Redundant == null) ? 1 : 1 + ((double)(_ACC.Redundant) / 100));
                    price *= coff;
                }




            if (_Position != null)
            {
                k.PositionList = Positions;
                k.Position = _Position.Title;
                k.PositionID = _Position.ID;
                if (_Position.RedundantType == (byte)RPC.RedundantType.Coefficient)
                    price *= (double)((_Position.Redundant == null) ? 1 : _Position.Redundant);
                else if (_Position.RedundantType == (byte)RPC.RedundantType.Percentage)
                {
                    double coff = (double)((_Position.Redundant == null) ? 1 : 1 + ((double)(_Position.Redundant) / 100));
                    price *= coff;
                }
            }

            if (_Lottary != null)
            {
                if (_Lottary.RedundantType == (byte)RPC.RedundantType.Coefficient)
                    price *= (double)((_Lottary.Redundant == null) ? 1 : _Lottary.Redundant);
                else if (_Lottary.RedundantType == (byte)RPC.RedundantType.Percentage)
                {
                    double coff = (double)((_Lottary.Redundant == null) ? 1 : 1 + ((double)(_Lottary.Redundant) / 100));
                    price *= coff;
                }
            }


            double tempPrice = price;

            if (k.Month == sm)
            {
                if (_SAMD != null)
                    if (_SAMD.RedundantType == (byte)RPC.RedundantType.Coefficient)
                        price *= (double)((_SAMD.Redundant == null) ? 1 : _SAMD.Redundant);
                    else if (_SAMD.RedundantType == (byte)RPC.RedundantType.Percentage)
                    {
                        double coff = (double)((_SAMD.Redundant == null) ? 1 : 1 + ((double)(_SAMD.Redundant) / 100));
                        price *= coff;
                    }


                if (_ContractMD1 != null)
                    if (_ContractMD1.FLD_Type == (byte)RPC.RedundantType.Coefficient)
                        price *= (double)((_ContractMD1.FLD_Redundant == null) ? 1 : _ContractMD1.FLD_Redundant);
                    else if (_ContractMD1.FLD_Type == (byte)RPC.RedundantType.Percentage)
                    {
                        double coff = (double)((_ContractMD1.FLD_Redundant == null) ? 1 : 1 + ((double)(_ContractMD1.FLD_Redundant) / 100));
                        price *= coff;
                    }



            }


            k.CPS = price;




            switch (content)
            {
                case 1:
                    if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                        k.CPS *= 40;

                    break;
                case 2:

                    if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                        k.CPS *= 40;

                    if ((clipTime >= 30 && clipTime < 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                        k.CPS *= (clipTime - 5);


                    break;
                case 3:
                    if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                        k.CPS *= 40;
                    break;
                case 4:
                    if ((clipTime == 60) && (k.media == 1) && (k.group == 2 || k.group == 3))
                        k.CPS *= 40;
                    break;
                case 5:
                    if ((clipTime >= 40) && (k.media == 1) && (k.group == 2 || k.group == 3))
                        k.CPS *= (clipTime - 5);
                    break;
                default:
                    k.CPS *= clipTime;
                    break;

            }

            k.CPSWD = k.CPS;


            double ep = (extraPlay) + (k.SeriesRedundant);
            if (k.Province != 0)
            {
                double kk = (ep);
                if (k.Series == 1)
                    kk *= 1.2;
                else if (k.Series == 2)
                    kk *= 1.5;
                else
                    kk *= 2;

                kk += (100 - k.ProvinceRedundant);

                double val = (double)((k.CPS * 100) / (100 + kk));
                k.CPS = Math.Ceiling(val);
            }
            else
            {
                double val = (double)((k.CPS * 100) / (100 + ep));
                k.CPS = Math.Ceiling(val);
            }



            return k;
        }


        public List<RPC.Kernel> AutorizeList(long mediaID, long campaignID, long channelID, List<int> maritalStatus, List<int> education, List<int> ages, List<int> occupation, List<int> familyIncome, List<int> gender,List<int> city ,long? location, byte range, byte weeks, bool overLap)
        {
            List<bool> _Gender = new List<bool>();
            if (gender != null)
            {
                if (gender.Count > 0)
                {
                    foreach (byte item in gender)
                    {
                        if (item == 1)
                            _Gender.Add(true);
                        else if (item == 2)
                            _Gender.Add(false);
                    }
                }
            }


            int days = weeks * 7;

            DateTime start = (DateTime)DateTime.Now.ToPersian().ToGerigorian();
            DateTime end = start.AddDays(days);
            int sm = start.GetPersianMonth();
            int em = end.GetPersianMonth();
            List<RPC.Kernel> list = null;
            RPD.VIW_Campaign campaign = RPD.Campaign.FindByID((long)campaignID);
            while (true)
            {
                try
                {
                    List<RPD.VIW_AutorizeList> tempList = RPD.Arrange.AutorizeList(mediaID, campaign.FLD_Group_FK, channelID, campaign.FLD_Questionnaire_FK, maritalStatus, education, ages, occupation, familyIncome, _Gender,city ,location, campaign.FLD_TimeRange, start, end, (byte)sm, (byte)em).ToList();

                    //int allViewer = tempList.Sum(i => (int)((i.FLD_Option == null) ? 0 : i.FLD_Option));
                    int allViewer = tempList.Count;
                    int count = tempList.GroupBy(ll => ll.FLD_Responder_FK).Count();


                    if (overLap)
                    {
                        list = tempList.GroupBy(i => new { i.FLD_Program_FK, i.FLD_ProgramTitle, i.FLD_WeekRep, i.FLD_PPS, i.FLD_ChannelTitle, i.FLD_ClassNo, i.FLD_PlayTime, i.FLD_Extra, i.FLD_Province_FK, i.FLD_ProvinceRedundant, i.FLD_ProvinceType, i.FLD_Series_FK, i.FLD_SeriesRedundant, i.FLD_SeriesType, i.FLD_After, i.FLD_Middle, i.FLD_Before, i.FLD_EnglishTitle, i.FLD_EnglishChannelTitle })
                           .Select(i => new RPC.Kernel()
                           {
                               Program = (long)((i.Key.FLD_Program_FK == null) ? 0 : i.Key.FLD_Program_FK),
                               ProgamTitle = RPC.Common.Language.ToLower().Trim().Contains("persian") ? i.Key.FLD_ProgramTitle : i.Key.FLD_EnglishTitle,
                               Viewer = i.Sum(k => (int)((k.FLD_Option == null) ? 0 : k.FLD_Option)),
                               AllViewer = count,
                               Reach = i.Where(ll => ll.FLD_Option > 0).Count(),
                               Count = count,
                               PPS = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                               CPS = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                               Frequency = (int)((i.Key.FLD_WeekRep == null) ? 0 : i.Key.FLD_WeekRep),
                               MaxFequency = (int)((i.Key.FLD_WeekRep == null) ? 0 : i.Key.FLD_WeekRep),
                               ChannelName = RPC.Common.Language.ToLower().Trim().Contains("persian") ? i.Key.FLD_ChannelTitle : i.Key.FLD_EnglishChannelTitle,
                               ClassNo = ((int)((i.Key.FLD_ClassNo == null) ? 0 : i.Key.FLD_ClassNo)).ToString(),
                               PlayTime = i.Key.FLD_PlayTime,
                               Extra = (double)((i.Key.FLD_Extra == null) ? 0 : i.Key.FLD_Extra),
                               Province = (long)((i.Key.FLD_Province_FK == null) ? 0 : i.Key.FLD_Province_FK),
                               ProvinceRedundant = (double)((i.Key.FLD_ProvinceRedundant == null) ? 0 : i.Key.FLD_ProvinceRedundant),
                               ProvinceType = (byte)((i.Key.FLD_ProvinceType == null) ? 0 : i.Key.FLD_ProvinceType),

                               Series = (long)((i.Key.FLD_Series_FK == null) ? 0 : i.Key.FLD_Series_FK),
                               SeriesRedundant = (double)((i.Key.FLD_SeriesRedundant == null) ? 0 : i.Key.FLD_SeriesRedundant),
                               SeriesType = (byte)((i.Key.FLD_SeriesType == null) ? 0 : i.Key.FLD_SeriesType),
                               After = i.Key.FLD_After,
                               Before = i.Key.FLD_Before,
                               Middle = i.Key.FLD_Middle

                           })
                          .OrderByDescending(i => i.RatePoint).ToList();


                        List<long> responders = new List<long>();

                        for (int i = 0; i < list.Count; i++)
                        {
                            List<RPD.VIW_AutorizeList> tmp = tempList.Where(l => l.FLD_Program_FK == list[i].Program).ToList();
                            for (int j = 0; j < tmp.Count; j++)
                            {
                                if (tmp[j].FLD_Option > 0)
                                {

                                    long responder = (long)((tmp[j].FLD_Responder_FK == null) ? 0 : tmp[j].FLD_Responder_FK);
                                    if (responders.FindIndex(s => s == responder) == -1)
                                    {
                                        responders.Add(responder);
                                        long prg = list[i].Program;
                                        int idxt = tempList.FindIndex(l => l.FLD_Responder_FK == responder && l.FLD_Program_FK == prg);
                                        int idx = tempList.FindIndex(l => l.FLD_Responder_FK == responder && l.FLD_Program_FK != prg && l.FLD_Option != 0);
                                        while (idx != -1)
                                        {
                                            tempList[idx].FLD_Option = 0;
                                            idx = tempList.FindIndex(l => l.FLD_Responder_FK == responder && l.FLD_Program_FK != prg && l.FLD_Option != 0);

                                        }

                                    }
                                }
                            }

                        }
                    }

                    list = tempList.GroupBy(i => new { i.FLD_Program_FK, i.FLD_ProgramTitle, i.FLD_WeekRep, i.FLD_PPS, i.FLD_ChannelTitle, i.FLD_ClassNo, i.FLD_PlayTime, i.FLD_Extra, i.FLD_Province_FK, i.FLD_ProvinceRedundant, i.FLD_ProvinceType, i.FLD_Series_FK, i.FLD_SeriesRedundant, i.FLD_SeriesType, i.FLD_Month, i.FLD_After, i.FLD_Middle, i.FLD_Before, i.FLD_EnglishTitle, i.FLD_EnglishChannelTitle })
                       .Select(i => new RPC.Kernel()
                       {
                           Program = (long)((i.Key.FLD_Program_FK == null) ? 0 : i.Key.FLD_Program_FK),
                           ProgamTitle = RPC.Common.Language.ToLower().Trim().Contains("persian") ? i.Key.FLD_ProgramTitle : i.Key.FLD_EnglishTitle,
                           ProgamEnglishTitle = i.Key.FLD_EnglishTitle,
                           Viewer = i.Where(ll => ll.FLD_Option > 0).Count(),
                           AllViewer = count,
                           Reach = i.Where(ll => ll.FLD_Option > 0).Count(),
                           Count = count,
                           PPS = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                           CPS = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                           Frequency = (int)((i.Key.FLD_WeekRep == null) ? 0 : i.Key.FLD_WeekRep),
                           MaxFequency = (int)((i.Key.FLD_WeekRep == null) ? 0 : i.Key.FLD_WeekRep),
                           ChannelName = RPC.Common.Language.ToLower().Trim().Contains("persian") ? i.Key.FLD_ChannelTitle : i.Key.FLD_EnglishChannelTitle,
                           ClassNo = ((int)((i.Key.FLD_ClassNo == null) ? 0 : i.Key.FLD_ClassNo)).ToString(),
                           PlayTime = i.Key.FLD_PlayTime,
                           Extra = (double)((i.Key.FLD_Extra == null) ? 0 : i.Key.FLD_Extra),
                           Province = (long)((i.Key.FLD_Province_FK == null) ? 0 : i.Key.FLD_Province_FK),
                           ProvinceRedundant = (double)((i.Key.FLD_ProvinceRedundant == null) ? 0 : i.Key.FLD_ProvinceRedundant),
                           ProvinceType = (byte)((i.Key.FLD_ProvinceType == null) ? 0 : i.Key.FLD_ProvinceType),

                           Series = (long)((i.Key.FLD_Series_FK == null) ? 0 : i.Key.FLD_Series_FK),
                           SeriesRedundant = (double)((i.Key.FLD_SeriesRedundant == null) ? 0 : i.Key.FLD_SeriesRedundant),
                           SeriesType = (byte)((i.Key.FLD_SeriesType == null) ? 0 : i.Key.FLD_SeriesType),
                           Month = (byte)((i.Key.FLD_Month == null) ? 0 : i.Key.FLD_Month),
                           After = i.Key.FLD_After,
                           Before = i.Key.FLD_Before,
                           Middle = i.Key.FLD_Middle
                       })
                       .OrderByDescending(i => i.RatePoint).ToList();

                    int rowCount = 0;
                    long maxRatePoint = list.Max(i => i.RatePoint);
                    switch (range)
                    {
                        case 1:
                            maxRatePoint = 0;
                            break;
                        case 2:
                            maxRatePoint /= 2;
                            break;
                        case 3:
                            maxRatePoint = (maxRatePoint * 2) / 3;
                            break;
                        case 4:
                            maxRatePoint = (maxRatePoint * 3) / 4;
                            break;
                        case 5:
                            maxRatePoint = (maxRatePoint * 4) / 5;
                            break;

                    }

                    foreach (RPC.Kernel item in list)
                    {
                        item.MinRatePoint = maxRatePoint;
                    }

                    break;
                }
                catch (Exception ex){}
            }
            return list;
        }
        public RPC.Kernel Viwers(long campaignID, List<int> maritalStatus, List<int> education, List<int> ages, List<int> occupation, List<int> familyIncome, List<int> gender)
        {

            List<bool> _Gender = null;
            if (gender != null)
            {
                _Gender = new List<bool>();
                if (gender.Count > 0)
                {
                    foreach (byte item in gender)
                    {
                        if (item == 1)
                            _Gender.Add(true);
                        else if (item == 2)
                            _Gender.Add(false);
                    }
                }
            }

            RPD.VIW_Campaign campaign = RPD.Campaign.FindByID((long)campaignID);



            int all = RPD.Respondent.All().Count();
            int limit = RPD.Respondent.FilterByAutorize(campaign.FLD_Questionnaire_FK, maritalStatus, education, ages, occupation, familyIncome, _Gender, campaign.FLD_TimeRange).Count();

            RPC.Kernel k = new RPC.Kernel();
            k.AllViewer = all;
            k.Viewer = limit;


            return k;

        }


        public RPC.Kernel FilterByProgramGroup(long? programID, long? groupID, long? location, byte sm, byte em)
        {
            RPD.VIW_AutorizeList i = RPD.Arrange.FilterByProgramGroup(programID, groupID, location, sm, em);
            return new RPC.Kernel()
            {
                Program = (long)((i.FLD_Program_FK == null) ? 0 : i.FLD_Program_FK),
                ProgamTitle = i.FLD_ProgramTitle,
                ProgamEnglishTitle = i.FLD_EnglishTitle,
                CPS = (double)((i.FLD_PPS == null) ? 0 : i.FLD_PPS),
                CPSWD = (double)((i.FLD_PPS == null) ? 0 : i.FLD_PPS),
                PPS = (double)((i.FLD_PPS == null) ? 0 : i.FLD_PPS),
                Frequency = (int)((i.FLD_WeekRep == null) ? 0 : i.FLD_WeekRep),
                MaxFequency = (int)((i.FLD_WeekRep == null) ? 0 : i.FLD_WeekRep),
                ChannelName = i.FLD_ChannelTitle,
                ClassNo = ((int)((i.FLD_ClassNo == null) ? 0 : i.FLD_ClassNo)).ToString(),
                PlayTime = i.FLD_PlayTime,
                Extra = (double)((i.FLD_Extra == null) ? 0 : i.FLD_Extra),


            };
        }
        public List<RPC.Kernel> AutorizeList(long mediaID, long campaignID, long location, String keyword, byte weeks)
        {
            int days = weeks * 7;

            DateTime start = (DateTime)DateTime.Now.ToPersian().ToGerigorian();
            DateTime end = start.AddDays(days);
            int sm = start.GetPersianMonth();
            int em = end.GetPersianMonth();

            RPD.VIW_Campaign campaign = RPD.Campaign.FindByID((long)campaignID);


            List<RPC.Kernel> list = RPD.Arrange.AutorizeList(mediaID, campaign.FLD_Group_FK, location, (byte)sm, (byte)em, keyword).GroupBy(i => new { i.FLD_Program_FK, i.FLD_ProgramTitle, i.FLD_WeekRep, i.FLD_PPS, i.FLD_ChannelTitle, i.FLD_ClassNo, i.FLD_PlayTime, i.FLD_Extra, i.FLD_EnglishTitle })
                .Select(i => new RPC.Kernel()
                {
                    Program = (long)((i.Key.FLD_Program_FK == null) ? 0 : i.Key.FLD_Program_FK),
                    ProgamTitle = i.Key.FLD_ProgramTitle,
                    ProgamEnglishTitle = i.Key.FLD_EnglishTitle,
                    Viewer = i.Sum(k => (int)((k.FLD_Option == null) ? 0 : k.FLD_Option)),
                    CPS = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                    CPSWD = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                    PPS = (double)((i.Key.FLD_PPS == null) ? 0 : i.Key.FLD_PPS),
                    Frequency = (int)((i.Key.FLD_WeekRep == null) ? 0 : i.Key.FLD_WeekRep),
                    MaxFequency = (int)((i.Key.FLD_WeekRep == null) ? 0 : i.Key.FLD_WeekRep),
                    ChannelName = i.Key.FLD_ChannelTitle,
                    ClassNo = ((int)((i.Key.FLD_ClassNo == null) ? 0 : i.Key.FLD_ClassNo)).ToString(),
                    PlayTime = i.Key.FLD_PlayTime,
                    Extra = (double)((i.Key.FLD_Extra == null) ? 0 : i.Key.FLD_Extra),


                })
                .OrderByDescending(i => i.Viewer).ToList();


            return list;
        }



        public List<RPC.Kernel> PickUp(List<RPC.Kernel> items, int clipTime, long? content, long budget, int extraPlay, int rep)
        {
            if (items != null)
            {
                // Get the context and create a new model.
                SolverContext context = SolverContext.GetContext();
                Model model = CreateModel(context, items, clipTime, content, budget, extraPlay, rep);


                if (model != null)
                {

                    // Solve the problem using the simplex solver
                    SimplexDirective simplex = new SimplexDirective();
                    //LpSolveDirective simplex = new LpSolveDirective();
                    Solution solution = context.Solve(simplex);

                    List<RPC.Kernel> list = new List<RPC.Kernel>();
                    foreach (Decision decision in solution.Decisions)
                    {
                        int count = (int)decision.ToDouble();
                        if (count > 0)
                        {
                            String[] temp = decision.Name.Split('_');
                            long programID = long.Parse(temp[1]);
                            string[] temp1 = temp[0].Split('g');
                            int month = int.Parse(temp1[1]);

                            RPC.Kernel kernel = new RPC.Kernel() { Program = programID, Month = month, Frequency = count };
                            RPC.Kernel tmp = items.FirstOrDefault(i => i.Program == programID && i.Month == month);
                            kernel.ProgamTitle = tmp.ProgamTitle;
                            kernel.ProgamEnglishTitle = tmp.ProgamEnglishTitle;
                            kernel.CPS = tmp.CPS;
                            kernel.CPSWD = tmp.CPSWD;
                            kernel.ChannelName = tmp.ChannelName;
                            kernel.ClassNo = tmp.ClassNo;
                            kernel.MaxFequency = tmp.MaxFequency;
                            kernel.PlayTime = tmp.PlayTime;
                            kernel.IsAdded = false;
                            kernel.IsDeleted = false;
                            kernel.IsExtra = false;
                            kernel.MonthName = tmp.MonthName;
                            kernel.Position = tmp.Position;
                            kernel.Location = tmp.Location;
                            kernel.PremiumFrequency = count;
                            kernel.PPS = tmp.PPS;
                            kernel.Viewer = tmp.Viewer;
                            kernel.AllViewer = tmp.AllViewer;
                            kernel.Reach = tmp.Reach;
                            kernel.Count = tmp.Count;

                            kernel.PositionID = tmp.PositionID;
                            kernel.PositionList = tmp.PositionList;
                            kernel.LocationID = tmp.LocationID;
                            kernel.LocationList = tmp.LocationList;

                            kernel.ClipTime = tmp.ClipTime;
                            kernel.Extra = tmp.Extra;
                            kernel.MinRatePoint = tmp.MinRatePoint;
                            kernel.After = tmp.After;
                            kernel.Before = tmp.Before;
                            kernel.Middle = tmp.Middle;
                            list.Add(kernel);
                        }
                    }

                    return list;
                }
            }
            return null;
        }



        public List<RPC.Kernel> GetWeekRep(List<RPC.Kernel> list, DateTime startDate, DateTime endDate)
        {
            TimeSpan kk = (endDate - startDate);
            int week = kk.Days / 7;

            int fm = startDate.GetPersianMonth();
            int lm = endDate.GetPersianMonth();

            int fd = startDate.GetPersianDay();
            int ld = endDate.GetPersianDay();


            if (fm == lm)
            {
                foreach (RPC.Kernel item in list)
                {
                    item.Month = fm;
                    item.MonthName = Generic.Date.GetPersianMonthName(fm);
                    if (!RPC.Common.Language.ToLower().Trim().Contains("persian"))
                    {
                        item.MonthName= Generic.Date.GetEnglishPersianMonthName(fm);
                    }

                    item.Frequency *= week;
                    item.MaxFequency = item.Frequency;

                }
                return list;
            }
            else
            {

                foreach (RPC.Kernel item in list)
                {
                    long total = item.Frequency * week;
                    int def = 0;
                    if (item.Month == fm)
                    {

                        item.MonthName = Generic.Date.GetPersianMonthName(fm);
                        if (!RPC.Common.Language.ToLower().Trim().Contains("persian"))
                        {
                            item.MonthName = Generic.Date.GetEnglishPersianMonthName(fm);
                        }
                        switch (fm)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                def = 31 - fd;
                                item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
                                item.MaxFequency = item.Frequency;

                                break;
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                def = 30 - fd;
                                item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
                                item.MaxFequency = item.Frequency;

                                break;
                            case 12:

                                def = 30 - fd;
                                item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
                                item.MaxFequency = item.Frequency;
                                break;
                        }
                    }
                    else if (item.Month == lm)
                    {
                        item.MonthName = Generic.Date.GetPersianMonthName(lm);
                        if (!RPC.Common.Language.ToLower().Trim().Contains("persian"))
                        {
                            item.MonthName = Generic.Date.GetEnglishPersianMonthName(lm);
                        }
                        def = ld;
                        item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
                        item.MaxFequency = item.Frequency;

                    }
                }
            }

            //if (fm == lm)
            //{
            //    foreach (RPC.Kernel item in list)
            //    {
            //        item.Month = fm;
            //        item.MonthName = Generic.Date.GetPersianMonthName(fm);
            //        item.Frequency *= week;
            //        item.MaxFequency = item.Frequency;

            //    }
            //    return list;
            //}
            //else
            //{
            //    List<RPC.Kernel> extraList = new List<RPC.Kernel>();
            //    foreach (RPC.Kernel item in list)
            //    {
            //        long total = item.Frequency * week;
            //        int def = 0;
            //        RPC.Kernel tmp = new RPC.Kernel()
            //        {
            //            Count = item.Count,
            //            Reach = item.Reach,
            //            AllViewer = item.AllViewer,
            //            CPS = item.CPS,
            //            CPSWD = item.CPSWD,
            //            Frequency = item.Frequency,
            //            Month = lm,
            //            MonthName = Generic.Date.GetPersianMonthName(lm),
            //            ProgamTitle = item.ProgamTitle,
            //            Program = item.Program,
            //            Viewer = item.Viewer,
            //            ChannelName = item.ChannelName,
            //            ClassNo = item.ClassNo,
            //            Location = item.Location,
            //            IsAdded = item.IsAdded,
            //            IsDeleted = item.IsDeleted,
            //            IsExtra = item.IsExtra,
            //            Limit = item.Limit,
            //            MaxFequency = item.MaxFequency,
            //            PlayTime = item.PlayTime,
            //            Position = item.Position,
            //            PPS = item.PPS,
            //            PremiumFrequency = item.PremiumFrequency,
            //            Province = item.Province,
            //            ProvinceRedundant = item.ProvinceRedundant,
            //            ProvinceType = item.ProvinceType,
            //            Series = item.Series,
            //            SeriesRedundant = item.SeriesRedundant,
            //            SeriesType = item.SeriesType,

            //            PositionID = item.PositionID,
            //            PositionList = item.PositionList,
            //            LocationID = item.LocationID,
            //            LocationList = item.LocationList,

            //            ClipTime = item.ClipTime,
            //            Extra = item.Extra


            //        };
            //        switch (fm)
            //        {
            //            case 1:
            //            case 2:
            //            case 3:
            //            case 4:
            //            case 5:
            //            case 6:
            //                def = ld;
            //                tmp.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
            //                tmp.MaxFequency = tmp.Frequency;
            //                if (tmp.Frequency > 0)
            //                    extraList.Add(tmp);
            //                item.Month = fm;
            //                item.MonthName = Generic.Date.GetPersianMonthName(fm);
            //                def = 31 - fd;
            //                item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
            //                item.MaxFequency = item.Frequency;

            //                break;
            //            case 7:
            //            case 8:
            //            case 9:
            //            case 10:
            //            case 11:
            //                def = ld;
            //                tmp.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
            //                tmp.MaxFequency = tmp.Frequency;
            //                if (tmp.Frequency > 0)
            //                    extraList.Add(tmp);
            //                item.Month = fm;
            //                item.MonthName = Generic.Date.GetPersianMonthName(fm);
            //                def = 30 - fd;
            //                item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
            //                item.MaxFequency = item.Frequency;

            //                break;
            //            case 12:
            //                def = ld;
            //                tmp.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
            //                tmp.MaxFequency = tmp.Frequency;
            //                if (tmp.Frequency > 0)
            //                    extraList.Add(tmp);
            //                item.Month = fm;
            //                item.MonthName = Generic.Date.GetPersianMonthName(fm);
            //                def = 30 - fd;
            //                item.Frequency = (long)Math.Ceiling((double)((total * def) / kk.Days));
            //                item.MaxFequency = item.Frequency;
            //                break;
            //        }
            //    }
            //    foreach (RPC.Kernel item in extraList)
            //    {
            //        if (item.Frequency != 0)
            //            list.Add(item);
            //    }

            list.RemoveAll(i => i.Frequency == 0);
            return list.OrderByDescending(i => i.Viewer).ToList();
            //}
        }


    }
}


