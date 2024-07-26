using slot_server.interfaces;
using slot_server.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slot_server.controller.winning_modes
{
    public class LinesCalculator : IWinningCalculator
    {
        private readonly Paytable payTable;
        private readonly Paylines paylines;
        private readonly int totalReels;
        private readonly int totalRows;
        private List<WinningLine> winningWays = new List<WinningLine>();

        public LinesCalculator(Paytable payTable, Paylines paylines, int totalRows, int totalReels)
        {
            this.paylines = paylines;
            this.payTable = payTable;
            this.totalReels = totalReels;
            this.totalRows = totalRows;
        }

        public Winning CalculateWins(string[,] screenSymbols)
        {
            Winning winning = new Winning();
            winning.totalWin = 0;

            for (int lineIndex = 0; lineIndex < paylines.lines.Count; lineIndex++)
            {
                var line = paylines.lines[lineIndex];
                string firstLineSymbol = screenSymbols[line[0] / totalReels, 0];

                if (string.IsNullOrEmpty(firstLineSymbol)) continue;

                int symMinMatches = payTable.combinationQuantities.ContainsKey(firstLineSymbol) ? payTable.combinationQuantities[firstLineSymbol][0] : 0;
                int matchCount = 1;

                for (var i = 1; i < line.Count - 1; i++)
                {
                    int row = line[i] / totalReels;
                    int col = line[i] % totalReels;

                    string currentSymbol = screenSymbols[row, col];
                    if (currentSymbol == firstLineSymbol)
                    {
                        matchCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (matchCount >= symMinMatches && payTable.combinationCredits.ContainsKey(firstLineSymbol))
                {
                    int creditsWon = payTable.combinationCredits[firstLineSymbol][matchCount - 3];
                    winning.totalWin += creditsWon;

                    WinningLine winningLine = new WinningLine
                    {
                        winningPositions = line,
                        symbolWin = firstLineSymbol,
                        quantity = matchCount,
                        creditsWon = creditsWon,
                        lineId = (lineIndex + 1).ToString()
                    };

                    winning.winnings.Add(winningLine);
                }
            }

            return winning;
        }
    }
}
