using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class ProgramSeries
    {
        public List<RPC.ProgramSeries> All()
        {
            return RPD.ProgramSeries.All().Select(Convert.ToProgramSeries).ToList();

        }

    }
}
