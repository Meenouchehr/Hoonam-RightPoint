using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class ProgramSeries
    {

        public long ID { get; set; }
        public string Title { get; set; }
        public Nullable<double> Redundant { get; set; }
        public Nullable<byte> Type { get; set; }
    
    }
}
