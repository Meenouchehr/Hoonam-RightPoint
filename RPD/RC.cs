using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class RC
    {

        public static long Insert(TBL_RC input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_RC.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_RC input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_RC ret = context.TBL_RC.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_CAccess = input.FLD_CAccess;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_RC ret = context.TBL_RC.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_RC.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_RC FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_RC ret = context.TBL_RC.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_RC> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_RC.AsQueryable();
        }

    }
}
