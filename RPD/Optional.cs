using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Optional
    {

        public static long Insert(TBL_Optional input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Optional.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Optional input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Optional ret = context.TBL_Optional.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Option = input.FLD_Option;               
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Optional ret = context.TBL_Optional.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Optional.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Optional FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Optional ret = context.TBL_Optional.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Optional> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Optional.AsQueryable();
        }

    }
}
