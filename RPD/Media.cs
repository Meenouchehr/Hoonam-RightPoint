using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Media
    {

         public static TBL_Media FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Media ret = context.TBL_Media.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Media> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Media.AsQueryable();
        }
    }
}
