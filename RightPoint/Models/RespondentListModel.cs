using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class RespondentListModel
    {
       
        public long ID { get; set; }
      
        public string QNo { get; set; }
       
        public string QTitle { get; set; }
     
        public string ResponderName { get; set; }
       
        public bool IsPublished { set; get; }
       
        public bool IsDeleted { set; get; }
    }
}