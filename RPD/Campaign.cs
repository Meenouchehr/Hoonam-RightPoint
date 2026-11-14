using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Campaign
    {
        public static long Insert(TBL_Campaign input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Campaign.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Campaign input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Campaign ret = context.TBL_Campaign.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                
                ret.FLD_Comment = input.FLD_Comment;
                ret.FLD_EnglishComment = input.FLD_EnglishComment;
                ret.FLD_Company_FK = input.FLD_Company_FK;
                ret.FLD_EndDate = input.FLD_EndDate;
                ret.FLD_IsActive = input.FLD_IsActive;
                ret.FLD_StartDate = input.FLD_StartDate;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                ret.FLD_Description = input.FLD_Description;
                ret.FLD_TimeRange = input.FLD_TimeRange;
                ret.FLD_ACC_FK = input.FLD_ACC_FK;
                ret.FLD_Lottary_FK = input.FLD_Lottary_FK;
                ret.FLD_Questionnaire_FK = input.FLD_Questionnaire_FK;
                
                foreach(TBL_CampaignAccess item in input.TBL_CampaignAccess)
                {
                    ret.TBL_CampaignAccess.Add(item);
                }
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Campaign ret = context.TBL_Campaign.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Campaign.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static VIW_Campaign FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_Campaign ret = context.VIW_Campaign.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }


        public static bool Publish(long inputID, bool publish)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Campaign ret = context.TBL_Campaign.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                ret.FLD_IsActive = publish;
                context.SaveChanges();
                return true;
            }
            return false;
        }


        public static IQueryable<VIW_Campaign> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Campaign.AsQueryable();
        }

        

        public static IQueryable<VIW_Campaign> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Campaign.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }



        public static IQueryable<VIW_Campaign> FilterByPublish(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Campaign.Where(i => i.FLD_IsActive == true).AsQueryable();
            else
                return context.VIW_Campaign.Where(i => ((i.FLD_IsActive == false) || (i.FLD_IsActive == (bool?)null))).AsQueryable();
        }

        public static IQueryable<VIW_Campaign> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();

            return context.VIW_Campaign.Where(i => ((i.FLD_QNo.Contains(keyword)) || (i.FLD_Title.Contains(keyword)))).AsQueryable();

        }

        public static IQueryable<VIW_Campaign> FilterByPublish(bool isPublish, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Campaign.Where(i => i.FLD_IsActive == true).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
            else
                return context.VIW_Campaign.Where(i => ((i.FLD_IsActive == false) || (i.FLD_IsActive == (bool?)null))).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }


  




        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Campaign.Count();
        }


        public static IQueryable<VIW_Campaign> FindByCompany(long companyID)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Campaign.Where(i => i.FLD_Company_FK == companyID).AsQueryable();
        }

    }
}
