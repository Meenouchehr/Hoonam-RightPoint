using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Questionnaire
    {

        public static long Insert(TBL_Questionnaire input)
        {
            RightPointEntities context = new RightPointEntities();
            input.FLD_CreatDate = DateTime.Now;
            context.TBL_Questionnaire.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Questionnaire input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Questionnaire ret = context.TBL_Questionnaire.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_IsPublish = input.FLD_IsPublish;
                ret.FLD_QNo = input.FLD_QNo;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                foreach (TBL_Question item in input.TBL_Question)
                {
                    ret.TBL_Question.Add(item);
                }
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Questionnaire ret = context.TBL_Questionnaire.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Questionnaire.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool Publish(long inputID, bool publish)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Questionnaire ret = context.TBL_Questionnaire.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                ret.FLD_IsPublish = publish;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static TBL_Questionnaire FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Questionnaire ret = context.TBL_Questionnaire.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Questionnaire> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Questionnaire.AsQueryable();
        }
        public static IQueryable<TBL_Questionnaire> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Questionnaire.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }


        public static IQueryable<TBL_Questionnaire> FilterByPublish(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.TBL_Questionnaire.Where(i => i.FLD_IsPublish == true).AsQueryable();
            else
                return context.TBL_Questionnaire.Where(i => ((i.FLD_IsPublish == false) || (i.FLD_IsPublish == (bool?)null))).AsQueryable();
        }

        public static IQueryable<TBL_Questionnaire> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();

            return context.TBL_Questionnaire.Where(i => ((i.FLD_QNo.Contains(keyword)) || (i.FLD_Title.Contains(keyword)))).AsQueryable();

        }

        public static IQueryable<TBL_Questionnaire> FilterByPublish(bool isPublish, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.TBL_Questionnaire.Where(i => i.FLD_IsPublish == true).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
            else
                return context.TBL_Questionnaire.Where(i => ((i.FLD_IsPublish == false) || (i.FLD_IsPublish == (bool?)null))).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }


     



        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Questionnaire.Count();
        }


        public static long Count(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();

            if (isPublish)

                return context.TBL_Questionnaire.Where(i => i.FLD_IsPublish == true).Count();
            else
                return context.TBL_Questionnaire.Where(i => ((i.FLD_IsPublish == false) || (i.FLD_IsPublish == (bool?)null))).Count();

        }
    }
}
