using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class QPOptional
    {
        public long ID { get; set; }
        public Nullable<long> QProgram { get; set; }
        public string Option { get; set; }
        public Nullable<byte> Choice { get; set; }
    }
}
