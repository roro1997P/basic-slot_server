using slot_server.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slot_server.interfaces
{
    public interface IWinningCalculator
    {
        Winning CalculateWins(string[,] screen);
    }
}
