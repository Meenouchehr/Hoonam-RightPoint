using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class QProgram
    {
        public long ID { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<bool> Optional { get; set; }
        public Nullable<long> Category { get; set; }
        public Nullable<long> Questionnaire { get; set; }
        public Nullable<long> QuestionTab { get; set; }
        public Nullable<long> QNo { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<byte> CategoryTab { get; set; }
        public string Title { get; set; }
        public Nullable<long> Class_FK { get; set; }
        public Nullable<long> Channel_FK { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string Channel { get; set; }
        public Nullable<byte> Media { get; set; }
        public Nullable<byte> Broadcast { get; set; }
        public Nullable<int> ClassNo { get; set; }
        public Nullable<double> PPS { get; set; }
        public string Location { get; set; }
        public Nullable<double> LocationRedundant { get; set; }
        public Nullable<byte> LocationType { get; set; }
    }
}
