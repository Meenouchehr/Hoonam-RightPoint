using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Campaign
    {

        public Campaign()
        {
            AccessList = new List<CampaignAccess>();
        }
        public long ID { get; set; }
        public Nullable<long> Company { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Comment { get; set; }
        public string EnglishComment { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyImage { get; set; }
        public string CompanyTel { get; set; }
        public Nullable<long> GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupComment { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<long> ACC { get; set; }
        public Nullable<long> Lottary { get; set; }
        public Nullable<long> Questionnaire { get; set; }
        public string LottaryTitle { get; set; }
        public string ACCTitle { get; set; }
        public string QNo { get; set; }
        public string QTitle { get; set; }

        public Nullable<byte> TimeRange { get; set; }

        public String Description { get; set; }

        public List<CampaignAccess> AccessList { get; set; }

    }
}
