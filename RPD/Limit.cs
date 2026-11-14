using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Limit
    {
        public static long Insert(TBL_Limit input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Limit.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Limit input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Limit ret = context.TBL_Limit.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Date = input.FLD_Date;
                ret.FLD_Day = input.FLD_Day;
                ret.FLD_Program_FK = input.FLD_Program_FK;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Limit ret = context.TBL_Limit.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Limit.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteByProgram(long program)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_Limit.RemoveRange(context.TBL_Limit.Where(i => i.FLD_Program_FK == program));
            context.SaveChanges();
        }

        public static TBL_Limit FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Limit ret = context.TBL_Limit.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Limit> FindByProgram(long program)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Limit.Where(i => i.FLD_Program_FK == program).AsQueryable();
        }

        public static IQueryable<TBL_Limit> FindByTime(DateTime? start, DateTime? end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Limit.Where(i => 
                System.Data.Entity.DbFunctions.TruncateTime(i.FLD_Date) >= System.Data.Entity.DbFunctions.TruncateTime(start)
                &&
                System.Data.Entity.DbFunctions.TruncateTime(i.FLD_Date) <= System.Data.Entity.DbFunctions.TruncateTime(end)

                ).AsQueryable();
        }

        public static IQueryable<TBL_Limit> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Limit.AsQueryable();
        }
    }
}
