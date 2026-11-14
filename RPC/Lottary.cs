using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Lottary    
    {
        public long ID { get; set; }
        public Nullable<long> Category { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<double> Redundant { get; set; }
        public Nullable<byte> RedundantType { get; set; }
    }
}
