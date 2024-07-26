using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slot_server.model
{
    public class Paytable
    {
        public Dictionary<string, int[]> combinationCredits = new Dictionary<string, int[]>();
        public Dictionary<string, int[]> combinationQuantities = new Dictionary<string, int[]>();
    }
}
