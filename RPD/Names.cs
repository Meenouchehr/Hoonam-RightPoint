using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Names
    {
        public static long Insert(TBL_Names input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Names.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Names input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Names ret = context.TBL_Names.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_ForeName = input.FLD_ForeName;
                ret.FLD_Gender = input.FLD_Gender;
                ret.FLD_Surname = input.FLD_Surname;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Names ret = context.TBL_Names.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Names.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Names FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Names ret = context.TBL_Names.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Names> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Names.AsQueryable();
        }
    }
}
