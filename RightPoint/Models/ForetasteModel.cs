using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightPoint.Models
{
    public class ForetasteModel
    {

        public byte ID { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Image { get; set; }
        public string EnglishImage { get; set; }


        public RPC.Foretaste toConvert()
        {
            RPC.Foretaste obj = new RPC.Foretaste();
         
            obj.ID = this.ID;
            obj.Image = this.Image;           
            obj.Title = this.Title;
            obj.EnglishTitle = this.EnglishTitle;
            obj.Image = this.Image;
            obj.EnglishImage = this.EnglishImage;
            
            return obj;
        }

        public void toConvert(RPC.Foretaste item)
        {
            this.ID = item.ID;
            this.Title = item.Title;
            this.EnglishTitle = item.EnglishTitle;
            this.Image = item.Image;
            this.EnglishImage = item.EnglishImage;

        }
    }
}