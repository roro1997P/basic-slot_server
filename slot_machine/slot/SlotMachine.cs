using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slot_server.controller.slot
{
    public class SlotMachine
    {
        private readonly List<List<string>> reelSets;
        private readonly int totalReels;
        private readonly int totalRows;
        private readonly Random random;

        public SlotMachine(List<List<string>> reelSets, int totalReels, int totalRows)
        {
            this.reelSets = reelSets;
            this.totalReels = totalReels;
            this.totalRows = totalRows;
            random = new Random();
        }

        public int GetTotalReels()
        {
            return totalReels;
        }

        public int GetTotalRows()
        {
            return totalRows;
        }

        public (List<int>, string[,]) Spin()
        {
            int rows = totalRows;
            int cols = totalReels;
            List<int> stopPositions = new List<int> { 0, 0, 0, 0, 0 };
            // Fixed stopPositions (winnings guaranteed)
            //List<int> stopPositions = new List<int> { 0, 0, 0, 0, 0 };
            //List<int> stopPositions = new List<int> { 0, 11, 1, 10, 14 };

            string[,] screen = new string[rows, cols];

            for (int col = 0; col < cols; col++)
            {
                stopPositions[col] = random.Next(reelSets[col].Count); // => Comment to use fixed stopPositions
                for (int row = 0; row < rows; row++)
                {
                    int symbolIndex = (stopPositions[col] + row) % reelSets[col].Count;
                    screen[row, col] = reelSets[col][symbolIndex];
                }
            }

            return (stopPositions, screen);
        }
    }
}
