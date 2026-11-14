using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Questionnaire
    {
        public Questionnaire()
        {
            QuestionList = new List<Question>();
        }

        public long ID { get; set; }
        public String QNo { get; set; }

        public String Title { get; set; }

        public String EnglishTitle { get; set; }

        public Nullable<bool> IsPublish { get; set; }

        public Nullable<System.DateTime> CreatDate { get; set; }


        public List<Question> QuestionList { get; set; }
    }
}
