using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class ProgramAccess
    {

        public long ID { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<long> Group { get; set; }
        public Nullable<long> Class { get; set; }
        public Nullable<long> Series { get; set; }
        public Nullable<long> Extra { get; set; }
        public Nullable<byte> ExtraType { get; set; }
        public string GroupName { get; set; }
        public string GroupComment { get; set; }
        public Nullable<int> ClassNo { get; set; }
        public Nullable<double> PPS { get; set; }
        public Nullable<byte> Media { get; set; }
        public string ProgramTitle { get; set; }
        public Nullable<long> Location { get; set; }
        public Nullable<byte> Month { get; set; }
    }
}
