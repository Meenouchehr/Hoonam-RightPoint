using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class ProgramSeries
    {
        public static IQueryable<TBL_ProgramSeries> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_ProgramSeries.AsQueryable();
        }
    }
}
