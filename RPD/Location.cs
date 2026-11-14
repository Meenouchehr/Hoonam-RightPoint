using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Location
    {
        public static long Insert(TBL_Location input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Location.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Location input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Location ret = context.TBL_Location.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Redundant = input.FLD_Redundant;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_Type = input.FLD_Type;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Location ret = context.TBL_Location.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Location.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Location FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Location ret = context.TBL_Location.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static TBL_Location FindByMedia(byte media,byte loc)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Location ret = context.TBL_Location.FirstOrDefault(i => i.FLD_Loc == loc && i.FLD_Media_FK==media);
            return ret;
        }

        public static IQueryable<TBL_Location> FilterByMedia(byte media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Location.Where(i => i.FLD_Media_FK == media).AsQueryable();
        }

        public static IQueryable<TBL_Location> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Location.AsQueryable();
        }
    }
}
