using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class AMD
    {
        public long ID { get; set; }
        public Nullable<byte> Month { get; set; }
        public Nullable<byte> Year { get; set; }
        public Nullable<double> Redundant { get; set; }
        public Nullable<byte> RedundantType { get; set; }
    }
}
