using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Descriptive
    {

        public static long Insert(TBL_Descriptive input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Descriptive.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Descriptive input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Descriptive ret = context.TBL_Descriptive.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Descrip = input.FLD_Descrip;                
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Descriptive ret = context.TBL_Descriptive.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Descriptive.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Descriptive FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Descriptive ret = context.TBL_Descriptive.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Descriptive> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Descriptive.AsQueryable();
        }

    }
}
