using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic;

namespace RPD
{
    public class Arrange
    {
        public static IQueryable<VIW_AutorizeList> AutorizeList(long mediaID, long? groupID, long? channelID, long? questionnaireID, List<int> maritalStatus, List<int> education, List<int> ages, List<int> occupation, List<int> familyIncome, List<bool> gender, List<int> city,long? location, byte? timeRange, DateTime? startPoint, DateTime? endPoint, byte sm, byte em)
        {

            DateTime dt = DateTime.Now;
            DateTime end = dt;
            DateTime start = dt;
            if (timeRange != null)
                if (timeRange != 0)
                {

                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            end = dt.AddDays(-3);
                            break;
                        case DayOfWeek.Saturday:
                            end = dt.AddDays(-1);
                            break;
                        case DayOfWeek.Sunday:
                            end = dt.AddDays(-2);
                            break;
                        case DayOfWeek.Thursday:
                            end = dt.AddDays(-6);
                            break;
                        case DayOfWeek.Tuesday:
                            end = dt.AddDays(-4);
                            break;
                        case DayOfWeek.Wednesday:
                            end = dt.AddDays(-5);
                            break;
                    }

                    int first = -(((byte)timeRange) * 7);

                    start = end.AddDays(first);
                }






            var contracts = PredicateBuilder.True<VIW_AutorizeList>();


            contracts = contracts.And(i => i.FLD_Month == sm || i.FLD_Month == em);
            //contracts = contracts.Or(i => i.FLD_Month == em );

            if (groupID != null)
            {
                if (groupID != 0)
                {
                    contracts = contracts.And(i => i.FLD_Group_FK == groupID);
                }
            }

            if (location != null)
                if (location != 0)
                {
                    switch (location)
                    {
                        case 1:
                            contracts = contracts.And(i => i.FLD_Before == true && i.FLD_Location_FK == location);
                            break;
                        case 2:
                        case 4:
                            contracts = contracts.And(i => i.FLD_Middle == true && i.FLD_Location_FK == location);
                            break;
                        case 3:
                            contracts = contracts.And(i => i.FLD_After == true && i.FLD_Location_FK == location);
                            break;
                    }
                }

            if (mediaID != 0)
            {
                contracts = contracts.And(i => i.FLD_Media_FK == mediaID);
            }

            if (channelID != null)
            {
                if (channelID != 0)
                {
                    contracts = contracts.And(i => i.FLD_Channel_FK == channelID);

                }
                else
                {
                    contracts = contracts.And(i => i.FLD_Broadcast != 3);
                }
            }
            else
            {
                contracts = contracts.And(i => i.FLD_Broadcast != 3);
            }




            if (questionnaireID != null)
            {
                if (questionnaireID != 0)
                {
                    contracts = contracts.And(i => i.FLD_Questionnaire_FK == questionnaireID);
                }
            }




            if (startPoint != null)
            {

                // contracts = contracts.And(i => System.Data.Entity.DbFunctions.TruncateTime(i.FLD_StartTime) <= System.Data.Entity.DbFunctions.TruncateTime(startPoint));
                //contracts = contracts.And(i => System.Data.Entity.DbFunctions.TruncateTime(i.FLD_EndTime) >= System.Data.Entity.DbFunctions.TruncateTime(startPoint));
            }

            //if (endPoint != null)
            //{
            //    contracts = contracts.And(i => i.FLD_EndTime <= endPoint);
            //}



            if (maritalStatus != null)
            {
                if (maritalStatus.Count > 0)
                {
                    if (maritalStatus.Count == 1)
                    {
                        int item = maritalStatus[0];
                        contracts = contracts.And(i => i.FLD_MaritalStatus == item);
                    }
                    else
                    {
                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (byte item in maritalStatus)
                            temp = temp.Or(i => i.FLD_MaritalStatus == item);
                        contracts = contracts.And(temp);
                    }
                }
            }

            if (education != null)
            {
                if (education.Count > 0)
                {
                    if (education.Count == 1)
                    {
                        int item = education[0];
                        contracts = contracts.And(i => i.FLD_Education == item);
                    }
                    else
                    {
                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (byte item in education)
                            temp = temp.Or(i => i.FLD_Education == item);
                        contracts = contracts.And(temp);
                    }
                }
            }

            if (ages != null)
            {
                if (ages.Count > 0)
                {
                    if (ages.Count == 1)
                    {
                        int item = ages[0];
                        contracts = contracts.And(i => i.FLD_Ages == item);
                    }
                    else
                    {

                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (byte item in ages)
                            temp = temp.Or(i => i.FLD_Ages == item);
                        contracts = contracts.And(temp);
                    }
                }
            }

            if (occupation != null)
            {
                if (occupation.Count > 0)

                    if (occupation.Count == 1)
                    {
                        int item = occupation[0];
                        contracts = contracts.And(i => i.FLD_Occupation == item);
                    }
                    else
                    {
                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (byte item in occupation)
                            temp = temp.Or(i => i.FLD_Occupation == item);
                        contracts = contracts.And(temp);
                    }
            }

            if (familyIncome != null)
            {

                if (familyIncome.Count > 0)
                {
                    if (familyIncome.Count == 1)
                    {
                        int item = familyIncome[0];
                        contracts = contracts.And(i => i.FLD_FamilyIncome == item);
                    }
                    else
                    {
                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (byte item in familyIncome)
                            temp = temp.Or(i => i.FLD_FamilyIncome == item);
                        contracts = contracts.And(temp);
                    }
                }
            }

            if (gender != null)
            {
                if (gender.Count > 0)
                {

                    if (gender.Count == 1)
                    {
                        bool item = gender[0];
                        contracts = contracts.And(i => i.FLD_Gender == item);
                    }
                    else
                    {
                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (bool item in gender)
                            temp = temp.Or(i => i.FLD_Gender == item);
                        contracts = contracts.And(temp);
                    }
                }
            }



            if (city != null)
            {
                if (city.Count > 0)
                {

                    if (city.Count == 1)
                    {
                        int item = city[0];
                        contracts = contracts.And(i => i.FLD_City_FK == item);
                    }
                    else
                    {
                        var temp = PredicateBuilder.False<VIW_AutorizeList>();
                        foreach (int item in city)
                            temp = temp.Or(i => i.FLD_City_FK == item);
                        contracts = contracts.And(temp);
                    }
                }
            }






            if (timeRange != null)
                if (timeRange != 0)
                {
                    // contracts = contracts.And(i => System.Data.Entity.DbFunctions.TruncateTime(i.FLD_CreatDate) >= System.Data.Entity.DbFunctions.TruncateTime(start));
                    //  contracts = contracts.And(i => System.Data.Entity.DbFunctions.TruncateTime(i.FLD_CreatDate) <= System.Data.Entity.DbFunctions.TruncateTime(end));
                }




            RightPointEntities context = new RightPointEntities();
            return context.VIW_AutorizeList.Where(contracts).AsQueryable();
        }

        public static IQueryable<VIW_AutorizeList> AutorizeList(long mediaID, long? groupID, long? locationID, byte sm, byte em, String keyword)
        {

            RightPointEntities context = new RightPointEntities();
            return context.VIW_AutorizeList.Where(i => i.FLD_Media_FK == mediaID && i.FLD_Group_FK == groupID && i.FLD_Location_FK == locationID && (i.FLD_Month == sm || i.FLD_Month == em) && i.FLD_ProgramTitle.Contains(keyword)).AsQueryable();
        }

        public static VIW_AutorizeList FilterByProgramGroup(long? programID, long? groupID, long? location, byte sm, byte em)
        {

            RightPointEntities context = new RightPointEntities();
            var contracts = PredicateBuilder.True<VIW_AutorizeList>();

            if (programID != null)
            {
                if (programID != 0)
                {
                    contracts = contracts.And(i => i.FLD_Program_FK == programID);
                }
            }


            if (groupID != null)
            {
                if (groupID != 0)
                {
                    contracts = contracts.And(i => i.FLD_Group_FK == groupID);
                }
            }

            if (location != null)
            {
                if (location != 0)
                {
                    switch (location)
                    {
                        case 1:
                            contracts = contracts.And(i => i.FLD_Before == true);
                            break;
                        case 2:
                            contracts = contracts.And(i => i.FLD_Middle == true);
                            break;
                        case 3:
                            contracts = contracts.And(i => i.FLD_After == true);
                            break;
                    }
                }
            }


            contracts = contracts.And(i => i.FLD_Month == sm || i.FLD_Month == em);


            return context.VIW_AutorizeList.FirstOrDefault(contracts);
        }

    }
}
