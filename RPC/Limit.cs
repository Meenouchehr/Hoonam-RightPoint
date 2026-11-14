using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Limit
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<byte> Day { get; set; }
        public Nullable<long> Program { get; set; }
    }
}
