using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Addresses
    {
        public static long Insert(TBL_Addresses input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Addresses.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Addresses input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Addresses ret = context.TBL_Addresses.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Address = input.FLD_Address;                
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Addresses ret = context.TBL_Addresses.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Addresses.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Addresses FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Addresses ret = context.TBL_Addresses.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Addresses> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Addresses.AsQueryable();
        }
    }
}
