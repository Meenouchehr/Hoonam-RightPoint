using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Answer
    {
        public List<RPC.Answer> FindByRespondent(long respondent)
        {
            return RPD.Answer.FindByRespondent(respondent).Select(Convert.ToAnswer).ToList();
        }


        public  int FilterByScore(long questionnaire, long program)

        {
            return RPD.Answer.FilterByScore(questionnaire, program);
        }

        public  int Count(long questionnaire, long program)

        {
            return RPD.Answer.Count(questionnaire,program);
        }
    }

}