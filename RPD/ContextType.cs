using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class ContextType
    {
        public static TBL_ContextType FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_ContextType ret = context.TBL_ContextType.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_ContextType> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_ContextType.AsQueryable();
        }
    }
}
