using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class QEOptional
    {
        public long ID_PK { get; set; }
        public Nullable<long> QExtra { get; set; }
        public string Option { get; set; }
        public Nullable<byte> Choice { get; set; }
    }
}
