using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic;

namespace RPD
{
    public class Respondent
    {

        public static long Insert(TBL_Respondent input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Respondent.Add(input);

            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Respondent input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Respondent ret = context.TBL_Respondent.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_BestAdvertising = input.FLD_BestAdvertising;
                ret.FLD_BestAdvertising2 = input.FLD_BestAdvertising2;
                ret.FLD_BestClass = input.FLD_BestClass;
                ret.FLD_BestClass2 = input.FLD_BestClass2;
                ret.FLD_BestCompany = input.FLD_BestCompany;
                ret.FLD_BestCompany2 = input.FLD_BestCompany2;
                ret.FLD_Inquirer_FK = input.FLD_Inquirer_FK;
                ret.FLD_IsPublish = input.FLD_IsPublish;
                ret.FLD_Questionnaire_FK = input.FLD_Questionnaire_FK;
                ret.FLD_Responder_FK = input.FLD_Responder_FK;
                ret.FLD_WatchTV = input.FLD_WatchTV;
                ret.FLD_WorstAdvertising = input.FLD_WorstAdvertising;
                ret.FLD_WorstClass = input.FLD_WorstClass;
                ret.FLD_WorstClass2 = input.FLD_WorstClass2;
                ret.FLD_WorstCompany = input.FLD_WorstCompany;
                ret.FLD_WorstCompany2 = input.FLD_WorstCompany2;





                foreach (TBL_Answer item in input.TBL_Answer)
                {
                    ret.TBL_Answer.Add(item);
                }
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Respondent ret = context.TBL_Respondent.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Respondent.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool Publish(long inputID, bool publish)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Respondent ret = context.TBL_Respondent.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                ret.FLD_IsPublish = publish;
                context.SaveChanges();
                return true;
            }
            return false;
        }



        public static VIW_Respondent FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_Respondent ret = context.VIW_Respondent.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static IQueryable<VIW_Respondent> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.AsQueryable();
        }

        public static IQueryable<VIW_Respondent> FilterByUser(long user)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.Where(i => i.FLD_Inquirer_FK == user).AsQueryable();
        }

        public static IQueryable<VIW_Respondent> FilterByAutorize(long? questionnaireID, List<int> maritalStatus, List<int> education, List<int> ages, List<int> occupation, List<int> familyIncome, List<bool> gender, byte? timeRange)
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






            var contracts = PredicateBuilder.True<VIW_Respondent>();




            if (questionnaireID != null)
            {
                if (questionnaireID != 0)
                {
                    contracts = contracts.And(i => i.FLD_Questionnaire_FK == questionnaireID);
                }
            }







            if (maritalStatus != null)
            {
                if (maritalStatus.Count > 0)
                {
                    var temp = PredicateBuilder.True<VIW_Respondent>();
                    foreach (byte item in maritalStatus)
                        temp = temp.Or(i => i.FLD_ResponderMaritalStatus == item);
                    contracts.And(temp);
                }
            }
            if (education != null)
            {
                if (education.Count > 0)
                {
                    var temp = PredicateBuilder.True<VIW_Respondent>();
                    foreach (byte item in education)
                        temp = temp.Or(i => i.FLD_ResponderEducation == item);
                    contracts.And(temp);
                }
            }
            if (ages != null)
            {
                if (ages.Count > 0)
                {
                    var temp = PredicateBuilder.True<VIW_Respondent>();
                    foreach (byte item in ages)
                        temp = temp.Or(i => i.FLD_ResponderAges == item);
                    contracts.And(temp);
                }
            }

            if (occupation != null)
            {
                if (occupation.Count > 0)
                {
                    var temp = PredicateBuilder.True<VIW_Respondent>();
                    foreach (byte item in occupation)
                        temp = temp.Or(i => i.FLD_ResponderOccupation == item);
                    contracts.And(temp);
                }
            }

            if (familyIncome != null)
            {
                if (familyIncome.Count > 0)
                {
                    var temp = PredicateBuilder.True<VIW_Respondent>();
                    foreach (byte item in familyIncome)
                        temp = temp.Or(i => i.FLD_ResponderFamilyIncome == item);
                    contracts.And(temp);
                }
            }

            if (gender != null)
            {
                if (gender.Count > 0)
                {
                    var temp = PredicateBuilder.True<VIW_Respondent>();
                    foreach (bool item in gender)
                        temp = temp.Or(i => i.FLD_ResponderGender == item);
                    contracts.And(temp);
                }
            }

            if (timeRange != null)
                if (timeRange != 0)
                {
                    // contracts = contracts.And(i => System.Data.Entity.DbFunctions.TruncateTime(i.FLD_CreateDate) >= System.Data.Entity.DbFunctions.TruncateTime(start));

                    //  contracts = contracts.And(i => System.Data.Entity.DbFunctions.TruncateTime(i.FLD_CreateDate) <= System.Data.Entity.DbFunctions.TruncateTime(end));

                }


            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.Where(contracts).AsQueryable();
        }


        public static IQueryable<VIW_Respondent> FilterByQuestionnaire(long questionnaireID)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.Where(i => i.FLD_Questionnaire_FK == questionnaireID).AsQueryable();
        }
        public static IQueryable<VIW_Respondent> FilterByPublish(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Respondent.Where(i => i.FLD_IsPublish == true).AsQueryable();
            else
                return context.VIW_Respondent.Where(i => ((i.FLD_IsPublish == false) || (i.FLD_IsPublish == (bool?)null))).AsQueryable();
        }

        public static IQueryable<VIW_Respondent> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }

        public static IQueryable<VIW_Respondent> SelectRange(long user, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.Where(i => i.FLD_Inquirer_FK == user).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }

        public static IQueryable<VIW_Respondent> FilterByPublish(bool isPublish, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Respondent.Where(i => i.FLD_IsPublish == true).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
            else
                return context.VIW_Respondent.Where(i => ((i.FLD_IsPublish == false) || (i.FLD_IsPublish == (bool?)null))).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }

        public static IQueryable<VIW_Respondent> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();

            return context.VIW_Respondent.Where(i => ((i.FLD_QNo.Contains(keyword)) || (i.FLD_QTitle.Contains(keyword)))).AsQueryable();

        }


        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.Count();
        }

        public static long Count(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();

            if (isPublish)

                return context.VIW_Respondent.Where(i => i.FLD_IsPublish == true).Count();
            else
                return context.VIW_Respondent.Where(i => ((i.FLD_IsPublish == false) || (i.FLD_IsPublish == (bool?)null))).Count();

        }

        public static long Count(long questionnaire)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Respondent.Where(i => i.FLD_Questionnaire_FK == questionnaire).Count();

        }
    }
}
