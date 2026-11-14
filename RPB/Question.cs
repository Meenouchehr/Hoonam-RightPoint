using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Question
    {
        public List<RPC.Question> FindByQuestionnaire(long questionnaire)
        {
            return RPD.Question.FindByQuestionnaire(questionnaire).Select(Convert.ToQuestion).ToList();
        }
    }
}
