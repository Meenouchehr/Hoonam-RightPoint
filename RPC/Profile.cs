using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Profile
    {
        public long ID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cell { get; set; }
        public Nullable<System.DateTime> DisableDate { get; set; }
        public string CompanyTitle { get; set; }
        public Nullable<byte> Access { get; set; }
        public Nullable<long> Company { get; set; }
    }
}
