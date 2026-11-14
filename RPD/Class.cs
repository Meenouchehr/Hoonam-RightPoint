using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Class
    {
        public static long Insert(TBL_Class input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Class.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Class input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Class ret = context.TBL_Class.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_ClassNo = input.FLD_ClassNo;
                ret.FLD_PPS = input.FLD_PPS;               
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Class ret = context.TBL_Class.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Class.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Class FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Class ret = context.TBL_Class.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Class> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Class.AsQueryable();
        }

        public static IQueryable<TBL_Class> FilterByMedia(byte media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Class.Where(i=>i.FLD_Media_FK==media).AsQueryable();
        }

    }
}
