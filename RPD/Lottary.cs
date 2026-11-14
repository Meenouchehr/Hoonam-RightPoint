using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Lottary
    {
        public static long Insert(TBL_Lottary input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Lottary.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Lottary input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Lottary ret = context.TBL_Lottary.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
              
                ret.FLD_Category_FK = input.FLD_Category_FK;             
                ret.FLD_Redundant = input.FLD_Redundant;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                ret.FLD_Type = input.FLD_Type;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Lottary ret = context.TBL_Lottary.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Lottary.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static VIW_Lottary FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_Lottary ret = context.VIW_Lottary.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static IQueryable<VIW_Lottary> FilterByMedia(byte media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Lottary.Where(i=>i.FLD_Media_FK==media).AsQueryable();
        }

        public static IQueryable<VIW_Lottary> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Lottary.AsQueryable();
        }
    }
}
