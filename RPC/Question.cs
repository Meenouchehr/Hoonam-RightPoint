using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Question
    {
        public long ID { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<long> Questionnaire { get; set; }
        
    }
}
