using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class QProgram
    {

        public static long Insert(TBL_QProgram input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_QProgram.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_QProgram input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QProgram ret = context.TBL_QProgram.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Optional = input.FLD_Optional;
                ret.FLD_Program_FK = input.FLD_Program_FK;

                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QProgram ret = context.TBL_QProgram.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_QProgram.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_QProgram FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QProgram ret = context.TBL_QProgram.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_QProgram> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_QProgram.AsQueryable();
        }

    }
}
