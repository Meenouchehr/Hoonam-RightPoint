using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class ContractMD
    {
        public static TBL_ContractMD FindByContractMonth(long? contract, byte? month)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ContractMD ret = context.TBL_ContractMD.FirstOrDefault(i => i.FLD_Month == month && i.FLD_ContractID_FK == contract);
            return ret;
        }

        public static IQueryable<TBL_ContractMD> ALL()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_ContractMD.AsQueryable();
            
        }
    }
}
