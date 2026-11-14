using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class Custom
    {

       

        
        public int ClipTime { get; set; }
      
        public int ExteraPlay { get; set; }

        
        public long Budget { get; set; }
     
        public int Rep { get; set; }
      
        public Nullable<long> ACC { get; set; }
       
        public Nullable<long> AMD { get; set; }
       
        public Nullable<long> Campaign { get; set; }
       
        public Nullable<byte> Media { get; set; }
      
        public Nullable<long> Position { get; set; }
        
        public Nullable<long> Lottary { get; set; }
      
        public Nullable<long> Location { get; set; }

      
        public Nullable<long> Channel { get; set; }
    
        public Nullable<long> ContextType { get; set; }


      
        public int[] MaritalStatus { get; set; }
      
        public int[] Education { get; set; }
       
        public int[] Ages { get; set; }
      
        public int[] Occupation { get; set; }
      
        public int[] FamilyIncome { get; set; }
       
        public int[] Gender { get; set; }


        public int[] City { set; get; }

     
        public byte TimeRange { get; set; }
      
        public byte DataRange { get; set; }

        
        public bool OverLap { get; set; }


    }


    public class CustmoModel
    {

        public CustmoModel()
        {
            Result = new Custom();
            ACCList = new List<RPC.ACC>();
            AMDList = new List<RPC.AMD>();
            PositionList = new List<RPC.Position>();
            LottaryList = new List<RPC.Lottary>();
            LocationList = new List<RPC.Location>();
            CampaignList = new List<RPC.CampaignAccess>();
            MediaList = new List<RPC.Media>();
            ChannelList = new List<RPC.Channel>();
            ContextTypeList = new List<RPC.ContextType>();
            CityList = new List<RPC.City>();
        }

        public Custom Result { get; set; }
        public List<RPC.ACC> ACCList { get; set; }
        public List<RPC.AMD> AMDList { get; set; }

        public List<RPC.Position> PositionList { get; set; }
        public List<RPC.Lottary> LottaryList { get; set; }

        public List<RPC.Location> LocationList { get; set; }

        public List<RPC.CampaignAccess> CampaignList { get; set; }

        public List<RPC.ContextType> ContextTypeList { get; set; }


        public List<RPC.Media> MediaList { get; set; }


        public List<RPC.Channel> ChannelList { get; set; }


        public List<RPC.City> CityList { get; set; }


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


        public List<EnumModel> TimeRangeList
        {
            get { return ((IEnumerable<TimeRange>)Enum.GetValues(typeof(TimeRange))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<EnumModel> DataRangeList
        {
            get { return ((IEnumerable<DataRange>)Enum.GetValues(typeof(DataRange))).Select(c => new EnumModel(c)).ToList(); }
        }



    }
}