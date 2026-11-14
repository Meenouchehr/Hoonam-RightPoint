using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Contract
    {
        public static long Insert(TBL_Contract input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Contract.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Contract input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Contract ret = context.TBL_Contract.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Commnet = input.FLD_Commnet;
                ret.FLD_Title = input.FLD_Title;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Contract ret = context.TBL_Contract.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Contract.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Contract FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Contract ret = context.TBL_Contract.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Contract> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Contract.AsQueryable();
        }

       
    }
}
