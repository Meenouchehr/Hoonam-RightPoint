using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class General
    {
        public  List<RPC.General> Gender()
        {
            List<RPC.General> list = new List<RPC.General>();
            list.Add(new RPC.General() { ID = 1, Title = "همه" });
            list.Add(new RPC.General() { ID = 2, Title = "مرد" });
            list.Add(new RPC.General() { ID = 3, Title = "زن" });

            return list;
        }

        public List<RPC.General> Width()
        {
            List<RPC.General> list = new List<RPC.General>();
            list.Add(new RPC.General() { ID = 1, Title = "کل" });
            list.Add(new RPC.General() { ID = 2, Title = "یک دوم" });
            list.Add(new RPC.General() { ID = 3, Title = "یک سوم" });
            list.Add(new RPC.General() { ID = 4, Title = "یک چهارم" });
            list.Add(new RPC.General() { ID = 5, Title = "یک پنجم" });
            return list;
        }

        public List<RPC.General> Hight()
        {
            List<RPC.General> list = new List<RPC.General>();
            list.Add(new RPC.General() { ID = 1, Title = "یک هفته" });
            list.Add(new RPC.General() { ID = 2, Title = "دو هفته" });
            list.Add(new RPC.General() { ID = 3, Title = "سه هفته" });
            list.Add(new RPC.General() { ID = 4, Title = "چهار هفته" });
            return list;
        }
    }
}
