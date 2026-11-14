using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class WeekRep
    {

        public static long Insert(TBL_WeekRep input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_WeekRep.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_WeekRep input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_WeekRep ret = context.TBL_WeekRep.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
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
            TBL_WeekRep ret = context.TBL_WeekRep.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_WeekRep.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteByProgram(long program)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_WeekRep.RemoveRange(context.TBL_WeekRep.Where(i => i.FLD_Program_FK == program));
            context.SaveChanges();
        }

        public static TBL_WeekRep FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_WeekRep ret = context.TBL_WeekRep.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_WeekRep> FindByProgram(long program)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_WeekRep.Where(i=>i.FLD_Program_FK==program).AsQueryable();
        }
        public static IQueryable<TBL_WeekRep> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_WeekRep.AsQueryable();
        }

    }
}
