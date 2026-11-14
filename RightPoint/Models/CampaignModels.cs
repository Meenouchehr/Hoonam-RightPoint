using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{

    public class CampaignAccessModel
    {
       
        public long ID { get; set; }
       
        public Nullable<long> Campaign { get; set; }
      
        public Nullable<long> User { get; set; }

       
        public string Name { set; get; }
      
        public bool IsSelected { set; get; }
    }
    public class CampaignModel
    {
        public CampaignModel()
        {
            AccessList = new List<CampaignAccessModel>();
            ACCList = new List<RPC.ACC>();
            LottaryList = new List<RPC.Lottary>();
            QuestionaireList = new List<RPC.Questionnaire>();
            CompanyList = new List<RPC.Company>();
        }

      
        public long ID { set; get; }
       
        public String Title { set; get; }

        public String EnglishTitle { set; get; }

        public String Comment { set; get; }

        public String EnglishComment { set; get; }


        public bool IsActive { set; get; }
      
        public byte TimeRange { get; set; }
      
        public String StartDate { set; get; }
      
        public String EndDate { set; get; }
       
        public int RunCount { set; get; }
       
        public bool IsDelete { set; get; }

       
        public int PassPercent { set; get; }

       
        public Nullable<long> Lottary { get; set; }
       
        public Nullable<long> ACC { get; set; }
       
        public Nullable<long> Questionaire { get; set; }
      
        public Nullable<long> Company { get; set; }
       
        public String Active
        {
            get
            {

                if (IsActive)
                    return "فعال";
                else
                    return "غیرفعال";

            }
        }
      
        public String Description { set; get; }

        public List<RPC.ACC> ACCList { get; set; }
        public List<RPC.Lottary> LottaryList { get; set; }
        public List<RPC.Questionnaire> QuestionaireList { get; set; }
        public List<CampaignAccessModel> AccessList { get; set; }
        public List<EnumModel> TimeRangeList
        {
            get { return ((IEnumerable<TimeRange>)Enum.GetValues(typeof(TimeRange))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<RPC.Company> CompanyList { get; set; }

      

        public RPC.Campaign toConvert()
        {
            RPC.Campaign obj = new RPC.Campaign();
            obj.ACC = this.ACC;
            obj.Comment = this.Comment;
            obj.EnglishComment = this.EnglishComment;
            obj.Company = this.Company;
            obj.EndDate = Generic.Date.ToGerigorian(this.EndDate);
            obj.ID = this.ID;
            obj.IsActive = this.IsActive;
            obj.Lottary = (this.Lottary == 0) ? (long?)null : this.Lottary;
            obj.Questionnaire = this.Questionaire;
            if (obj.Questionnaire == 0) obj.Questionnaire = null;
            obj.StartDate = Generic.Date.ToGerigorian(this.StartDate); ;
            obj.Title = this.Title;
            obj.EnglishTitle = this.EnglishTitle;
            obj.TimeRange = this.TimeRange;
            obj.Description = this.Description;



            if (AccessList != null)
                foreach (CampaignAccessModel item in AccessList)
                {
                    if (item.IsSelected)
                    {
                        obj.AccessList.Add(new RPC.CampaignAccess() { User = item.User });
                    }
                }
            return obj;
        }
    }
}