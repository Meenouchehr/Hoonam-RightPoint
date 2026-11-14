using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Media
    {
        public List<RPC.Media> All()
        {
            return RPD.Media.All().Select(Convert.ToMedia).ToList();

        }
    }
}
