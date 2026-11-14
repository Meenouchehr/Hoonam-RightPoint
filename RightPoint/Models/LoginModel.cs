using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightPoint.Models
{

    public class Language
    {
        
        [LocalizedDisplayName("Language")]
        public String Title { set; get; }
    }
    public class LoginModel
    {

        public LoginModel()
        {
            Lan = new Language();
            LanList = new List<Language>();
            LanList.Add(new Language() { Title = "English" });
            LanList.Add(new Language() { Title = "Persian" });
        }


       
        public String Username { get; set; }
      
        public String Password { get; set; }


        public Language Lan { set; get; }
        public List<Language> LanList { private set; get; }
    }
}