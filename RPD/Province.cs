using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Province
    {
        public static IQueryable<TBL_Province> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Province.AsQueryable();
        }
    }
}
