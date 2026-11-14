using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class UserModel
    {
        public UserModel()
        {
            CompanyList = new List<RPC.Company>();
            CampaignAccessList = new List<Models.CampaignAccessModel>();
        }

        [Key]
        
        public long ID { get; set; }
       
        public byte Access { get; set; }
      
        public String Name { get; set; }
       
        public bool IsEnabled { get; set; }
       
        public int run { get; set; }
       
        public bool IsDeleted { get; set; }

      
        public byte Gender { get; set; }
       
        public String Username { get; set; }
       
        public String Password { get; set; }
        
        public String RePassword { get; set; }
       
        public String Email { get; set; }
       
        public String Phone { get; set; }
       
        public String Cell { get; set; }

     
        public long Company { get; set; }
       
        public String CompanyTitle { get; set; }
       
        public String CreateDate { get; set; }
      
        public String DisableDate { get; set; }

        public List<RPC.Company> CompanyList { set; get; }
        public List<Models.CampaignAccessModel> CampaignAccessList { set; get; }

        public List<EnumModel> AccessList
        {
            get { return ((IEnumerable<Access>)Enum.GetValues(typeof(Access))).Select(c => new EnumModel(c)).ToList(); }
        }

        public RPC.User ToConvert()
        {

            RPC.User obj = new RPC.User();
            obj.Access = Access;
            obj.Cell = Cell;
            obj.Company = Company;
            obj.CompanyTitle = CompanyTitle;
            obj.CreateDate = Generic.Date.ToGerigorian(CreateDate);
            obj.DisableDate = Generic.Date.ToGerigorian(DisableDate);
            obj.Email = Email;
            obj.Gender = (Gender == 1) ? true : false;
            obj.ID = ID;
            obj.IsEnabled = IsEnabled;
            obj.Name = Name;
            obj.Password = Password;
            obj.Phone = Phone;
            obj.Username = Username;

            if (CampaignAccessList != null)
                foreach (CampaignAccessModel item in CampaignAccessList)
                {
                    if (item.IsSelected)
                    {
                        obj.CampaignAccessList.Add(new RPC.CampaignAccess() { Campaign = item.Campaign });
                    }
                }
            return obj;
        }
    }
}