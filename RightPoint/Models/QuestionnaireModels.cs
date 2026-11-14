using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{

    public class Program
    {
        [Key]
      
        public long ProgramID { set; get; }
     
        public bool IsChecked { set; get; }
      
        public String ProgramTitle { set; get; }
      
        public String ChannelName { set; get; }
       
        public long Channel { set; get; }
    }

    public class Channel
    {
      
        public long ChannelID { set; get; }
       
        public bool IsChecked { set; get; }
      
        public String ChannelTitle { set; get; }
       
    }

    public class QuestionnaireModel
    {

        public QuestionnaireModel()
        {
            
            ProgramList = new List<Program>();
            ChannelList = new List<Channel>();
        }

        [Key]
      
        public long ID { get; set; }
       
        public String Title { get; set; }

        public String EnglishTitle { get; set; }

        public String Code { get; set; }
      
        public String CreateDate { get; set; }
       
        public bool IsPublish { get; set; }
      
        public bool IsDeleted { get; set; }

        

        public List<Program> ProgramList { get; set; }
        public List<Channel> ChannelList { get; set; }




        public RPC.Questionnaire toConvert()
        {
            RPC.Questionnaire obj = new RPC.Questionnaire();
            obj.ID = ID;
            obj.IsPublish = IsPublish;
            obj.QNo = Code;
            obj.Title = Title;
            obj.EnglishTitle = EnglishTitle;
            if (ProgramList!=null)
                if (ProgramList.Count>0)
                {
                    foreach(Program prg in ProgramList)
                    {
                        if (prg.IsChecked)
                        obj.QuestionList.Add(new RPC.Question() { Program =prg.ProgramID });
                    }
                }

            return obj;
        }




    }
}