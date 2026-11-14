using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class QPOptional
    {
        public static long Insert(TBL_QPOptional input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_QPOptional.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_QPOptional input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QPOptional ret = context.TBL_QPOptional.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Choice = input.FLD_Choice;
                ret.FLD_Option = input.FLD_Option;
                ret.FLD_QProgram_FK = input.FLD_QProgram_FK;

                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QPOptional ret = context.TBL_QPOptional.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_QPOptional.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_QPOptional FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QPOptional ret = context.TBL_QPOptional.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_QPOptional> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_QPOptional.AsQueryable();
        }
    }
}
