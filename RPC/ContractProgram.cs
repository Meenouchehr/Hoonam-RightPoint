using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class ContractProgram
    {

        public long ID { get; set; }
        public string ContractTitle { get; set; }
        public string Commnet { get; set; }
        public Nullable<int> ClassNo { get; set; }
        public Nullable<double> PPS { get; set; }
        public Nullable<byte> Media { get; set; }
        public string ChannelTitle { get; set; }
        public string ProgramTitle { get; set; }
        public Nullable<long> Class { get; set; }
        public Nullable<long> Channel { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string PlayTime { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<bool> Before { get; set; }
        public Nullable<bool> Middle { get; set; }
        public Nullable<bool> After { get; set; }
        public Nullable<bool> Enable { get; set; }
    }
}
