using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class City
    {
        public List<RPC.City> All()
        {
            return RPD.City.All().Select(Convert.ToCity).ToList();

        }
    }
}
