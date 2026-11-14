using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class QEOptional
    {

        public static long Insert(TBL_QEOptional input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_QEOptional.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_QEOptional input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QEOptional ret = context.TBL_QEOptional.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Choice = input.FLD_Choice;
                ret.FLD_Option = input.FLD_Option;
                ret.FLD_QExtra_FK = input.FLD_QExtra_FK;
               
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QEOptional ret = context.TBL_QEOptional.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_QEOptional.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_QEOptional FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QEOptional ret = context.TBL_QEOptional.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_QEOptional> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_QEOptional.AsQueryable();
        }

    }
}
