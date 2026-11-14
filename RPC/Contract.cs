using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Contract
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }

        public Nullable<int> Class { get; set; }
    }
}
