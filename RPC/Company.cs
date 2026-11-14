using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Company
    {
        public long ID { get; set; }
        public Nullable<long> Group { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string GroupName { get; set; }
        public string GroupComment { get; set; }
    }
}
