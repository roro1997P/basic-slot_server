using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using slot_server.interfaces;
using slot_server.model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace slot_server.controller.winning_modes
{
    public class WaysCalculator : IWinningCalculator
    {
        private readonly Paytable payTable;
        private readonly int totalReels;
        private readonly int totalRows;
        private string[,] screenSymbols;
        private List<WinningLine> winningWays = new List<WinningLine>();

        public WaysCalculator(Paytable payTable, int totalRows, int totalReels)
        {
            this.payTable = payTable;
            this.totalReels = totalReels;
            this.totalRows = totalRows;
        }

        public Winning CalculateWins(string[,] screen)
        {
            int totalWin = 0;
            screenSymbols = screen;
            winningWays.Clear();

            FindWinningLines(ref totalWin);

            Winning winning = new Winning
            {
                totalWin = totalWin,
                winnings = winningWays
            };

            return winning;
        }

        private void FindWinningLines(ref int totalWin, int nextCol = 0, string symbol = "", int matchCount = 0, List<int> winPositions = null)
        {
            bool matchFound = false;
            if (winPositions == null)
            {
                winPositions = new List<int>();
            }

            if (nextCol >= totalReels)
            {
                // Max symbols win: Add winning line
                AddWinning(ref totalWin, symbol, matchCount, winPositions);
                return;
            }


            for (int row = 0; row < totalRows; row++)
            {
                string currentSym = screenSymbols[row, nextCol];
                int symbolIndex = row * totalReels + nextCol;
                int symMinMatches = payTable.combinationQuantities.ContainsKey(symbol) ? payTable.combinationQuantities[symbol][0] : 0;

                
                if (string.IsNullOrEmpty(symbol))
                {
                    // First reel symbols: Move to next reel
                    var newWinPositions = new List<int> { symbolIndex };
                    FindWinningLines(ref totalWin, nextCol + 1, currentSym, 1, newWinPositions);
                }
                else if (symbol == currentSym)
                {
                    // Match found, but no winnings yet: Move to next reel
                    var newWinPositions = new List<int>(winPositions) { symbolIndex };
                    matchFound = true;
                    FindWinningLines(ref totalWin, nextCol + 1, currentSym, matchCount + 1, newWinPositions);
                }
                else if (matchCount >= symMinMatches && symbol != currentSym && row == totalRows - 1 && !matchFound)
                {
                    // Match not found, but winnings not added yet: Add winning
                    AddWinning(ref totalWin, symbol, matchCount, winPositions);
                }
            }
        }

        private int AddWinning(ref int totalWin, string symbol, int matchCount, List<int> winPositions)
        {
            if (payTable.combinationCredits.ContainsKey(symbol))
            {
                int creditsWon = payTable.combinationCredits[symbol][matchCount - 3];
                totalWin += creditsWon;
                WinningLine winning = new WinningLine
                {
                    creditsWon = creditsWon,
                    winningPositions = winPositions,
                    quantity = matchCount,
                    symbolWin = symbol
                };
                winningWays.Add(winning);
            }

            return totalWin;
        }
    }
}
