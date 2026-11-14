using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class ContractAccess
    {
        public static long Insert(TBL_ContractAccess input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_ContractAccess.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_ContractAccess input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ContractAccess ret = context.TBL_ContractAccess.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Access = input.FLD_Access;
                ret.FLD_ClassID_FK = input.FLD_ClassID_FK;
                ret.FLD_ContractID_FK = input.FLD_ContractID_FK;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ContractAccess ret = context.TBL_ContractAccess.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_ContractAccess.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_ContractAccess FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ContractAccess ret = context.TBL_ContractAccess.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_ContractAccess> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_ContractAccess.AsQueryable();
        }

    }
}
