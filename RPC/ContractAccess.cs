using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class ContractAccess
    {
        public long ID { get; set; }
        public Nullable<long> Contract { get; set; }
        public Nullable<long> Class { get; set; }
        public Nullable<bool> Access { get; set; }
    }
}
