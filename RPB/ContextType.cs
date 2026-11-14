using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class ContextType
    {
        public List<RPC.ContextType> All()
        {
            return RPD.ContextType.All().Select(Convert.ToContextType).ToList();

        }
    }
}
