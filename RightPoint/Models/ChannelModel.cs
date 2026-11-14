using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class ChannelModel
    {

        public ChannelModel()
        {
            ProvinceList = new List<RPC.Province>();
        }

      
        public long ID { set; get; }
      
        public String Title { set; get; }

        public String EnglishTitle { set; get; }

        public String Image { set; get; }
      
        public String ImageAlt { set; get; }
      
        public byte Media { set; get; }
       
        public byte Broadcast { set; get; }
       
        public long Province { set; get; }
       
        public bool IsDelete { set; get; }

        public List<EnumModel> MediaList
        {
            get { return ((IEnumerable<Media>)Enum.GetValues(typeof(Media))).Select(c => new EnumModel(c)).ToList(); }
        }
        public List<EnumModel> BroadcastList
        {
            get { return ((IEnumerable<Broadcast>)Enum.GetValues(typeof(Broadcast))).Select(c => new EnumModel(c)).ToList(); }
        }

        public List<RPC.Province> ProvinceList { set; get; }
               
        public RPC.Channel toConvert()
        {
            RPC.Channel obj = new RPC.Channel();
            obj.Broadcast = this.Broadcast;
            obj.ID = this.ID;
            obj.Image = this.Image;
            obj.Media = this.Media;
            obj.Title = this.Title;
            obj.EnglishTitle = this.EnglishTitle;
            obj.Province = this.Province;
            if (this.Province == 0)
                obj.Province = (long?)null;
            return obj;
        }
    }
}