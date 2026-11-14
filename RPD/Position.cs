using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Position
    {
        public static long Insert(TBL_Position input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Position.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Position input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Position ret = context.TBL_Position.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_ID_PK = input.FLD_ID_PK;
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
            TBL_Position ret = context.TBL_Position.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Position.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Position FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Position ret = context.TBL_Position.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static IQueryable<TBL_Position> FilterByMedia(byte media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Position.Where(i => i.FLD_Media_FK == media).AsQueryable();
        }
        public static IQueryable<TBL_Position> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Position.AsQueryable();
        }
    }
}
