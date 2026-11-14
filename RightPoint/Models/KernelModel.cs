using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RightPoint.Models
{
    public class KernelModel
    {
        public KernelModel()
        {
            Items = new List<RPC.Kernel>();
        }
        public void AddList(List<RPC.Kernel> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Items.Add(list[i]);
                }
            }
        }

        public List<RPC.Kernel> Items { set; get; }

       
        public long Budget { get; set; }

      
        public byte TimeRange { get; set; }
      
        public byte DataRange { get; set; }

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