using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Responder
    {
        public long ID { get; set; }
        public Nullable<byte> Education { get; set; }
        public Nullable<byte> Ages { get; set; }
        public Nullable<byte> Occupation { get; set; }
        public Nullable<byte> FamilyIncome { get; set; }
        public Nullable<bool> Cooperation { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<byte> MaritalStatus { get; set; }
        public Nullable<int> City { set; get; }
        public String Name { set; get; }
    }
}
