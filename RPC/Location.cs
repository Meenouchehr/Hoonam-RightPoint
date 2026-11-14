using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Location
    {
        public long ID { get; set; }
        public String Title { get; set; }
        public String EnglishTitle { get; set; }
        public Nullable<double> Redundant { get; set; }
        public Nullable<byte> RedundantType { get; set; }
        public Nullable<bool> Before { get; set; }
        public Nullable<bool> Middle { get; set; }
        public Nullable<bool> After { get; set; }

    }
}
