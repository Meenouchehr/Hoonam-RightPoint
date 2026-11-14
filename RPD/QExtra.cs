using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class QExtra
    {
        public static long Insert(TBL_QExtra input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_QExtra.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_QExtra input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QExtra ret = context.TBL_QExtra.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Optional = input.FLD_Optional;
                ret.FLD_Question = input.FLD_Question;               

                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QExtra ret = context.TBL_QExtra.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_QExtra.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_QExtra FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_QExtra ret = context.TBL_QExtra.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_QExtra> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_QExtra.AsQueryable();
        }
    }
}
