using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class ProgramStatusModel
    {
      
        public long ID { get; set; }
       
        public String Title { get; set; }
       
        public String Status { get; set; }
    }
    public class RespondentModel
    {

        public RespondentModel()
        {
            ChannelList = new List<RPC.Channel>();
            ProgramList = new List<ProgramStatusModel>();
            CityList = new List<RPC.City>();
        }


        [Key]
     
        public long ID { get; set; }
      
        public long? ResponderID { get; set; }
       
        public long? IngurirID { set; get; }
        
        public long Questionnaire { get; set; }
      
        public String Fullname { get; set; }
     
        public byte MaritalStatus { get; set; }
      
        public byte Education { get; set; }
       
        public byte Ages { get; set; }
      
        public byte Occupation { get; set; }
       
        public byte FamilyIncome { get; set; }

            
        public byte Gender { get; set; }

        public Nullable<int> City { get; set; }
       
        public byte Option { get; set; }

      
        public String BestCompany { get; set; }
       
        public long BestClass { get; set; }
       
        public String WorstCompany { get; set; }

      
        public String BestCompany2 { get; set; }
    
        public long BestClass2 { get; set; }
   
        public String WorstCompany2 { get; set; }

       
        public String BestAdvertising { set; get; }
       
        public long WorstClass { get; set; }
       
        public String WorstAdvertising { set; get; }
       
        public String BestAdvertising2 { set; get; }
       
        public long WorstClass2 { get; set; }
      
        public String WorstAdvertising2 { set; get; }
       
        public bool IsPublished { set; get; }

        public List<ProgramStatusModel> ProgramList { set; get; }
        public List<EnumModel> MaritalStatusList
        {
            get { return ((IEnumerable<MaritalStatus>)Enum.GetValues(typeof(MaritalStatus))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> EducationList
        {
            get { return ((IEnumerable<Education>)Enum.GetValues(typeof(Education))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> AgesList
        {
            get { return ((IEnumerable<Ages>)Enum.GetValues(typeof(Ages))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> OccupationList
        {
            get { return ((IEnumerable<Occupation>)Enum.GetValues(typeof(Occupation))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> GenderList
        {
            get { return ((IEnumerable<Gender>)Enum.GetValues(typeof(Gender))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> FamilyIncomeList
        {
            get { return ((IEnumerable<FamilyIncome>)Enum.GetValues(typeof(FamilyIncome))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> OptionList
        {
            get { return ((IEnumerable<Option>)Enum.GetValues(typeof(Option))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<RPC.Channel> ChannelList { get; set; }


        public List<RPC.City> CityList { get; set; }

        public RPC.Respondent toConvert()
        {

            RPC.Respondent obj = new RPC.Respondent();
            obj.BestAdvertising = BestAdvertising;
            obj.BestAdvertising2 = BestAdvertising2;
            obj.BestClass = BestClass;
            obj.BestClass2 = BestClass2;
            obj.BestCompany = BestCompany;
            obj.BestCompany2 = BestCompany2;
            obj.Questionnaire = Questionnaire;
            obj.ID = ID;
            obj.ResponderProfile = new RPC.Responder() { Ages = this.Ages, Education = this.Education, FamilyIncome = this.FamilyIncome, Gender = (this.Gender == 1) ? true : false, ID = (long)((this.ResponderID == null) ? 0 : this.ResponderID), Occupation = this.Occupation, Name = this.Fullname , MaritalStatus=this.MaritalStatus , City=this.City };
            obj.Responder =ResponderID;
            obj.Inquirer = IngurirID;
            obj.IsPublish = IsPublished;
            obj.WorstAdvertising = WorstAdvertising;
            obj.WorstAdvertising2 = WorstAdvertising2;
            obj.WorstClass = WorstClass;
            obj.WorstClass2 = WorstClass2;
            obj.WorstCompany = WorstCompany;
            obj.WorstCompany2 = WorstCompany2;
            obj.WatchTV = Option;

            if (obj.ID==0)
            {
                obj.CreateDate = DateTime.Now;
                obj.Inquirer = RPC.Common.CurrentUser.ID;
            }

            if (ProgramList != null)
                if (ProgramList.Count > 0)
                {
                    foreach (ProgramStatusModel prg in ProgramList)
                    {
                        obj.AnswerList.Add(new RPC.Answer() { Program = prg.ID, Option =((prg.Status==null)? (byte?)null : byte.Parse(prg.Status)) });
                    }
                }


            return obj;
        }
    }
}