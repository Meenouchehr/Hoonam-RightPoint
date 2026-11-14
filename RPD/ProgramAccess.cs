using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class ProgramAccess
    {
        public static long Insert(TBL_ProgramAccess input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_ProgramAccess.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_ProgramAccess input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ProgramAccess ret = context.TBL_ProgramAccess.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Class_FK = input.FLD_Class_FK;
                ret.FLD_Extra = input.FLD_Extra;
                ret.FLD_ExtraType = input.FLD_ExtraType;
                ret.FLD_Group_FK = input.FLD_Group_FK;
                ret.FLD_ID_PK = input.FLD_ID_PK;
                ret.FLD_Program_FK = input.FLD_Program_FK;
                ret.FLD_Series_FK = input.FLD_Series_FK;
                ret.FLD_Location_FK = input.FLD_Location_FK;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ProgramAccess ret = context.TBL_ProgramAccess.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_ProgramAccess.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteByProgram(long program)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_ProgramAccess.RemoveRange(context.TBL_ProgramAccess.Where(i => i.FLD_Program_FK == program));
            context.SaveChanges();
        }
        public static void DeleteByProgram(long program,byte month)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_ProgramAccess.RemoveRange(context.TBL_ProgramAccess.Where(i => i.FLD_Program_FK == program && i.FLD_Month==month));
            context.SaveChanges();
        }

        public static void DeleteByGroup(long group)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_ProgramAccess.RemoveRange(context.TBL_ProgramAccess.Where(i => i.FLD_Group_FK == group));
            context.SaveChanges();
        }


        public static void DeleteByClass(long classID)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_ProgramAccess.RemoveRange(context.TBL_ProgramAccess.Where(i => i.FLD_Class_FK == classID));
            context.SaveChanges();
        }

        public static VIW_ProgramAccess FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_ProgramAccess ret = context.VIW_ProgramAccess.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<VIW_ProgramAccess> FindByClass(long classID)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_ProgramAccess.Where(i => i.FLD_Class_FK == classID).AsQueryable();
        }

        public static IQueryable<VIW_ProgramAccess> FindByGroup(long group)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_ProgramAccess.Where(i => i.FLD_Group_FK == group).AsQueryable();
        }


        public static IQueryable<VIW_ProgramAccess> FindByProgram(long program,byte month)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_ProgramAccess.Where(i => i.FLD_Program_FK== program && i.FLD_Month==month).AsQueryable();
        }

        public static IQueryable<VIW_ProgramAccess> FindByProgram(long program)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_ProgramAccess.Where(i => i.FLD_Program_FK == program).AsQueryable();
        }

        public static IQueryable<VIW_ProgramAccess> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_ProgramAccess.AsQueryable();
        }

    }
}
