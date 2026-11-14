using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Titles
    {
        public static long Insert(TBL_Titles input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Titles.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Titles input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Titles ret = context.TBL_Titles.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Title = input.FLD_Title;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Titles ret = context.TBL_Titles.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Titles.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Titles FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Titles ret = context.TBL_Titles.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Titles> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Titles.AsQueryable();
        }
    }
}
