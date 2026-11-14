using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Authorize
    {

        public long ID { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<byte> Option { get; set; }
        public string ProgramTitle { get; set; }
        public Nullable<long> Class { get; set; }
        public Nullable<long> Channel { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string PlayTime { get; set; }
        public Nullable<bool> Before { get; set; }
        public Nullable<bool> Middle { get; set; }
        public Nullable<bool> After { get; set; }
        public Nullable<int> ClassNo { get; set; }
        public Nullable<double> PPS { get; set; }
        public Nullable<byte> Media { get; set; }
        public string ChannelTitle { get; set; }
        public Nullable<byte> Broadcast { get; set; }
        public string Name { get; set; }
        public Nullable<byte> Education { get; set; }
        public Nullable<byte> Ages { get; set; }
        public Nullable<byte> Occupation { get; set; }
        public Nullable<byte> FamilyIncome { get; set; }
        public Nullable<byte> MaritalStatus { get; set; }
        public Nullable<bool> Cooperation { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<long> Contract { get; set; }       
        public string ContractTitle { get; set; }
       

    }
}
