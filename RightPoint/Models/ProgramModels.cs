using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Generic;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{


    public class ProgramAccessModel
    {
        public ProgramAccessModel()
        {
            ClassList = new List<Models.ClassModel>();
            SeriesList = new List<RPC.ProgramSeries>();
           

        }

      
        public long Group { get; set; }
        
        public long Class { get; set; }

        public long Series { get; set; }
       
        public long Location { get; set; }
      
        public Nullable<long> Extra { get; set; }
       
        public Nullable<byte> ExtraType { get; set; }

        public string GroupName { get; set; }

        public string GroupComment { get; set; }
    
        public String Title { get; set; }

       
        public int Index { get; set; }

        public byte Month { get; set; }

        public byte SelectMonth { get; set; }

        public List<Models.ClassModel> ClassList { set; get; }
        public List<RPC.ProgramSeries> SeriesList { set; get; }
     

    }

    public class ProgramModel
    {

        public ProgramModel()
        {
            After = true;
            Before = true;
            Middle = true;
            IsEnabled = true;

            LocationList = new List<RPC.Location>();
            ChannelList = new List<RPC.Channel>();
            LimitList = new List<RPC.Limit>();
            ProgramAccessList = new List<Models.ProgramAccessModel>();

        }

        [Key]
       
        public long ID { get; set; }
      
        public String Title { get; set; }

        public String EnglishTitle { get; set; }

        public String ChannelName { get; set; }
       
        public long Channel { get; set; }
       
        public String StartTime { get; set; }
      
        public String EndTime { get; set; }
      
        public String PlayTime { get; set; }
      
        public bool IsEnabled { get; set; }
       
        public bool Before { get; set; }
      
        public bool Middle { get; set; }
       
        public bool After { get; set; }
      
        public int[] WeekRep { get; set; }
       
        public String Limit { get; set; }

        public bool IsDeleted { get; set; }
      
        public byte Media { get; set; }




        public List<EnumModel> WeekDayList
        {
            get { return ((IEnumerable<WeekDay>)Enum.GetValues(typeof(WeekDay))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<RPC.Channel> ChannelList { get; set; }
        public List<RPC.Limit> LimitList { get; set; }
        public List<RPC.Location> LocationList { set; get; }
        public List<EnumModel> MediaList
        {
            get { return ((IEnumerable<Media>)Enum.GetValues(typeof(Media))).Select(c => new EnumModel(c)).ToList(); }
        }



        public List<Models.ProgramAccessModel> ProgramAccessList { get; set; }

        public RPC.Program toConvert()
        {
            RPC.Program obj = new RPC.Program();
            obj.After = this.After;
            obj.Before = this.Before;
            obj.Channel = (this.Channel == 0) ? (long?)null : this.Channel;
            obj.Enable = IsEnabled;
            obj.EndTime = this.EndTime.ToGerigorian();
            obj.ID = this.ID;
            obj.Middle = this.Middle;
            obj.PlayTime = this.PlayTime;
            obj.StartTime = this.StartTime.ToGerigorian();
            obj.Title = this.Title;
            obj.EnglishTitle = this.EnglishTitle;
            if (WeekRep != null)
                if (WeekRep.Length > 0)
                    obj.WeekRepList = WeekRep.Select(i => new RPC.WeekRep() { Day = (byte)i }).ToList();
            if (Limit != null)
                if (Limit.Length > 0)
                {
                    Regex r = new Regex("({)(.*?)(})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection mc = r.Matches(Limit);
                    foreach (Match m1 in mc)
                    {
                        RPC.Limit i = new RPC.Limit();
                        i.Date = m1.Groups[2].Value.ToString().ToGerigorian();
                        obj.LimitList.Add(i);
                    }
                }


            if (ProgramAccessList != null)
            {
                foreach (Models.ProgramAccessModel item in ProgramAccessList)
                {
                    RPC.ProgramAccess tmp = new RPC.ProgramAccess();
                    tmp.Group = (long?)((item.Group == 0) ? (long?)null : item.Group); 
                    tmp.Class = (long?)((item.Class == 0) ? (long?)null : item.Class);
                    tmp.Series = (long?)((item.Series == 0) ? (long?)null : item.Series); 
                    tmp.Extra = item.Extra;
                    tmp.Month = item.Month;
                    tmp.ExtraType = 1;
                    tmp.Location = (long?)((item.Location == 0) ? (long?)null : item.Location); 
                    obj.ProgramAccessList.Add(tmp);

                }
            }


            return obj;
        }




    }
}