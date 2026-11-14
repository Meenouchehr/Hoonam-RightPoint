using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class CompanyModel
    {
        public CompanyModel()
        {
            GroupList=new List<RPC.OwnerGroup>();
        }

     
        public long ID { set; get; }
       
        public Nullable<long> GroupID { set; get; }
       
        public String Title { set; get; }

        public String EnglishTitle { set; get; }
       
        public String GroupTitle { set; get; }
       
        public String GroupComment { set; get; }
       
        public String Image { set; get; }
       
        public String ImageAlt { set; get; }
       
        public String Tel { set; get; }
      
        public bool IsDelete { set; get; }

        public List<RPC.OwnerGroup> GroupList { set; get; }

        public RPC.Company toConvert()
        {
            RPC.Company obj = new RPC.Company();
            obj.Group = this.GroupID;
            obj.GroupComment = this.GroupComment;
            obj.GroupName = this.GroupTitle;
            obj.ID = this.ID;
            obj.Image = this.Image;
            obj.Phone = this.Tel;
            obj.Title = this.Title;
            obj.EnglishTitle = this.EnglishTitle;
           



            return obj;
        }

    }
}