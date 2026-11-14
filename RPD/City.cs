using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class City
    {

        public static IQueryable<TBL_City> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_City.AsQueryable();
        }
    }
}
