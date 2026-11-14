using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightPoint.Models
{

    public class Language
    {
        public int ID { set; get; }

        [LocalizedDisplayName("Language")]
        public String Title { set; get; }
    }

    public class LanguageModel
    {

        public LanguageModel()
        {
            LanList = new List<Language>();
            LanList.Add(new Language() { ID = 1, Title = "English" });
            LanList.Add(new Language() { ID = 2, Title = "Persian" });
        }

        public Language Lan { set; get; }

        public List<Language> LanList { private set; get; }
    }
}