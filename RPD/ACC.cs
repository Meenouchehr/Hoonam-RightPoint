using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class ACC
    {
        public static long Insert(TBL_ACC input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_ACC.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_ACC input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ACC ret = context.TBL_ACC.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                ret.FLD_Media_FK = input.FLD_Media_FK;
                ret.FLD_Redundant = input.FLD_Redundant;
                ret.FLD_Type = input.FLD_Type;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ACC ret = context.TBL_ACC.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_ACC.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_ACC FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ACC ret = context.TBL_ACC.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static IQueryable<TBL_ACC> FilterByMedia(byte Media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_ACC.Where(i => i.FLD_Media_FK == Media).AsQueryable();
          
        }
        public static IQueryable<TBL_ACC> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_ACC.AsQueryable();
        }
    }
}
