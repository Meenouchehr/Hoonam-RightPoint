using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Answer
    {
        public long ID { get; set; }
        public Nullable<long> Respondent { get; set; }
        public Nullable<long> Program { get; set; }
        public Nullable<byte> Option { get; set; }
        public string QNo { get; set; }
        public string Title { get; set; }
        public Nullable<bool> IsPublish { get; set; }
        public string ResponderName { get; set; }
        public Nullable<byte> ResponderEducation { get; set; }
        public Nullable<byte> ResponderAges { get; set; }
        public Nullable<byte> ResponderOccupation { get; set; }
        public Nullable<byte> ResponderFamilyIncome { get; set; }
        public Nullable<bool> ResponderCooperation { get; set; }
        public Nullable<bool> ResponderGender { get; set; }
        public string InquirerName { get; set; }
        public string InquirerTitle { get; set; }
        public Nullable<bool> InquirerGender { get; set; }
        public string InquirerCompanyTitle { get; set; }
        public string ProgramTitle { get; set; }
        public Nullable<bool> Before { get; set; }
        public Nullable<bool> Middle { get; set; }
        public Nullable<bool> After { get; set; }
    }
}
