using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class ProgramAccess
    {

        public List<RPC.ProgramAccess> FindByProgram(long id)
        {
            return RPD.ProgramAccess.FindByProgram(id).Select(Convert.ToProgramAccess).ToList();

        }
    }
}
