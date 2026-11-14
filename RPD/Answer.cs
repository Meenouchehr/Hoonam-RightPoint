using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Answer
    {
        public static long Insert(TBL_Answer input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Answer.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Answer input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Answer ret = context.TBL_Answer.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Option = input.FLD_Option;
                ret.FLD_Program_FK = input.FLD_Program_FK;
                ret.FLD_Respondent_FK = input.FLD_Respondent_FK;
               
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Answer ret = context.TBL_Answer.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Answer.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static void DeleteByRespondent(long respondent)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_Answer.RemoveRange(context.TBL_Answer.Where(i => i.FLD_Respondent_FK == respondent));
            context.SaveChanges();
        }
        public static TBL_Answer FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Answer ret = context.TBL_Answer.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Answer> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Answer.AsQueryable();
        }

        public static int FilterByScore(long questionnaire,long program)

        {
            try
            {
                RightPointEntities context = new RightPointEntities();
                return context.VIW_Answer.Where(i => i.FLD_Questionnaire_FK == questionnaire && i.FLD_Program_FK == program).Sum(i => ((byte)((i.FLD_Option == null) ? 0 : i.FLD_Option)));
            }
            catch
            {
                return 0;
            }
        }
        
        public static int Count(long questionnaire, long program)

        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Answer.Where(i => i.FLD_Questionnaire_FK == questionnaire && i.FLD_Program_FK == program).Count();
        }
        public static IQueryable<VIW_Answer> FindByRespondent(long respondent)
        {
            RightPointEntities context = new RightPointEntities();
            IQueryable<VIW_Answer> ret = context.VIW_Answer.Where(i => i.FLD_Respondent_FK == respondent);
            return ret;
        }
    }
}
