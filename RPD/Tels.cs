using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Tels
    {
        public static long Insert(TBL_Tels input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Tels.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Tels input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Tels ret = context.TBL_Tels.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Tel = input.FLD_Tel;               
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Tels ret = context.TBL_Tels.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Tels.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Tels FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Tels ret = context.TBL_Tels.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Tels> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Tels.AsQueryable();
        }
    }
}
