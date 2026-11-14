using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class CampaignAccess
    {

        public long ID { get; set; }
        public Nullable<long> Campaign { get; set; }
        public Nullable<long> User { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Comment { get; set; }
        public string EnglishComment { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string CompanyName { set; get; }

        public string CompanyEnglishName { set; get; }
        public string GroupName { set; get; }

        public string GroupEnglishName { set; get; }

        public string QuestionnaireTitle { set; get; }

        public string QuestionnaireEnglishTitle { set; get; }

    }
}
