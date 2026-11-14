using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class AMD
    {
        public static long Insert(TBL_AMD input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_AMD.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_AMD input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_AMD ret = context.TBL_AMD.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                
                ret.FLD_Month = input.FLD_Month;
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
            TBL_AMD ret = context.TBL_AMD.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_AMD.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_AMD FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_AMD ret = context.TBL_AMD.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static TBL_AMD FindByMediaMonth(byte media,byte month)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_AMD ret = context.TBL_AMD.FirstOrDefault(i => i.FLD_Month== month && i.FLD_Media_FK==media);
            return ret;
        }

        public static IQueryable<TBL_AMD> FilterByMedia(byte Media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_AMD.Where(i => i.FLD_Media_FK == Media).AsQueryable();

        }

        public static IQueryable<TBL_AMD> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_AMD.AsQueryable();
        }
    }
}
