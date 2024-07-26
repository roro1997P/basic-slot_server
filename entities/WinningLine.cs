using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slot_server.model
{
    public class WinningLine
    {
        public List<int> winningPositions = new List<int>();
        public string symbolWin = string.Empty;
        public int quantity = 0;
        public int creditsWon = 0;
        public string lineId = string.Empty;
    }
}
