using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Question
    {
        public static long Insert(TBL_Question input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Question.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Question input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Question ret = context.TBL_Question.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Program_FK = input.FLD_Program_FK;
                ret.FLD_Questionnaire_FK = input.FLD_Questionnaire_FK;
              

                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Question ret = context.TBL_Question.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Question.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteByQuestionnaire(long questionnaire)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_Question.RemoveRange(context.TBL_Question.Where(i => i.FLD_Questionnaire_FK == questionnaire));
            context.SaveChanges();
        }
        public static TBL_Question FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Question ret = context.TBL_Question.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Question> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Question.AsQueryable();
        }

        public static IQueryable<VIW_Question> FindByQuestionnaire(long questionnaire)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Question.Where(i => i.FLD_Questionnaire_FK == questionnaire).AsQueryable();
        }
    }
}
