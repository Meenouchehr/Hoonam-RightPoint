using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Respondent
    {

        public Respondent()
        {
            AnswerList = new List<Answer>();
        }


        public string QNo { get; set; }
        public string QTitle { get; set; }
        public long ID { get; set; }
        public Nullable<long> Questionnaire { get; set; }
        public Nullable<long> Responder { get; set; }
        public string BestCompany { get; set; }
        public Nullable<long> BestClass { get; set; }
        public string WorstAdvertising { get; set; }
        public string WorstCompany { get; set; }
        public Nullable<byte> WatchTV { get; set; }
        public Nullable<long> WorstClass { get; set; }
        public Nullable<long> Inquirer { get; set; }
        public string BestAdvertising { get; set; }
        public string ResponderName { get; set; }
        public Nullable<byte> ResponderEducation { get; set; }
        public Nullable<byte> ResponderAges { get; set; }
        public Nullable<byte> ResponderOccupation { get; set; }
        public Nullable<byte> ResponderFamilyIncome { get; set; }
        public Nullable<bool> ResponderCooperation { get; set; }
        public Nullable<bool> ResponderGender { get; set; }
        public Nullable<byte> ResponderMaritalStatus { get; set; }
        public Nullable<int> ResponderCity { get; set; }

        public string InquirerName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string BestAdvertising2 { get; set; }
        public string BestCompany2 { get; set; }
        public Nullable<long> BestClass2 { get; set; }
        public string WorstAdvertising2 { get; set; }
        public string WorstCompany2 { get; set; }
        public Nullable<long> WorstClass2 { get; set; }

        public Nullable<bool> IsPublish { get; set; }

        public RPC.Responder ResponderProfile { get; set; }

        public RPC.User InquirerProfile { get; set; }
        public List<RPC.Answer> AnswerList { get; set; }
    }
}
