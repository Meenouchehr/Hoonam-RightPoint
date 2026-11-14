using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Province
    {
        public List<RPC.Province> All()
        {
            return RPD.Province.All().Select(Convert.ToProvince).ToList();

        }
    }
}
