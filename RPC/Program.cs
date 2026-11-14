using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Program
    {
        public Program()
        {
            WeekRepList = new List<WeekRep>();
            LimitList = new List<Limit>();
            ProgramAccessList = new List<ProgramAccess>();
        }


        public long ID { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public Nullable<long> Channel { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string PlayTime { get; set; }
        public string ChannelMedia { get; set; }
        public string ChannelName { get; set; }
        public Nullable<byte> ChannelBroadcast { get; set; }
        public Nullable<bool> Before { get; set; }
        public Nullable<bool> Middle { get; set; }
        public Nullable<bool> After { get; set; }
        public Nullable<byte> Media { get; set; }
     

        public List<RPC.WeekRep> WeekRepList { get; set; }
        public List<RPC.Limit> LimitList { get; set; }

        public List<RPC.ProgramAccess> ProgramAccessList { get; set; }

    }
}
