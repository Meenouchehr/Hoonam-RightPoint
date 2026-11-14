using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Result
    {
        public long RetID { set; get; }
        public RPC.Operation RetOperation { set; get; }
        public RPC.Upshot RetUpshot { set; get; }
    }
}
