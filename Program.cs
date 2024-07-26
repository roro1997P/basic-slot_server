using slot_server.controller.slot;
using slot_server.controller.winning_modes;
using slot_server.model;
using System;
using System.Collections.Generic;


# region Ways
var reelSets = new List<List<string>>
    {
        new List<string> { "sym2", "sym7", "sym7", "sym1", "sym1", "sym5", "sym1", "sym4", "sym5", "sym3", "sym2", "sym3", "sym8", "sym4", "sym5", "sym2", "sym8", "sym5", "sym7", "sym2" },
        new List<string> { "sym1", "sym6", "sym7", "sym6", "sym5", "sym5", "sym8", "sym5", "sym5", "sym4", "sym7", "sym2", "sym5", "sym7", "sym1", "sym5", "sym6", "sym8", "sym7", "sym6", "sym3", "sym3", "sym6", "sym7", "sym3" },
        new List<string> { "sym5", "sym2", "sym7", "sym8", "sym3", "sym2", "sym6", "sym2", "sym2", "sym5", "sym3", "sym5", "sym1", "sym6", "sym3", "sym2", "sym4", "sym1", "sym6", "sym8", "sym6", "sym3", "sym4", "sym4", "sym8", "sym1", "sym7", "sym6", "sym1", "sym6" },
        new List<string> { "sym2", "sym6", "sym3", "sym6", "sym8", "sym8", "sym3", "sym6", "sym8", "sym1", "sym5", "sym1", "sym6", "sym3", "sym6", "sym7", "sym2", "sym5", "sym3", "sym6", "sym8", "sym4", "sym1", "sym5", "sym7" },
        new List<string> { "sym7", "sym8", "sym2", "sym3", "sym4", "sym1", "sym3", "sym2", "sym2", "sym4", "sym4", "sym2", "sym6", "sym4", "sym1", "sym6", "sym1", "sym6", "sym4", "sym8" }
    };

Paytable paytable = new Paytable();
paytable.combinationCredits = new Dictionary<string, int[]>
    {
        { "sym1", new int[] { 1, 2, 3 } },
        { "sym2", new int[] { 1, 2, 3 } },
        { "sym3", new int[] { 1, 2, 5 } },
        { "sym4", new int[] { 2, 5, 10 } },
        { "sym5", new int[] { 5, 10, 15 } },
        { "sym6", new int[] { 5, 10, 15 } },
        { "sym7", new int[] { 5, 10, 20 } },
        { "sym8", new int[] { 10, 20, 50 } }
    };

paytable.combinationQuantities = new Dictionary<string, int[]>
    {
        { "sym1", new int[] { 3, 4, 5 } },
        { "sym2", new int[] { 3, 4, 5 } },
        { "sym3", new int[] { 3, 4, 5 } },
        { "sym4", new int[] { 3, 4, 5 } },
        { "sym5", new int[] { 3, 4, 5 } },
        { "sym6", new int[] { 3, 4, 5 } },
        { "sym7", new int[] { 3, 4, 5 } },
        { "sym8", new int[] { 3, 4, 5 } }
    };

var slotMachine = new SlotMachine(reelSets, 5, 3);
var waysCalculator = new WaysCalculator(paytable, slotMachine.GetTotalRows(), slotMachine.GetTotalReels());

void SpinWays()
{
    Console.WriteLine("WAYS: ");

    var (stopPositions, screen) = slotMachine.Spin();
    Console.WriteLine($"Stop Positions: {string.Join(", ", stopPositions)}");
    Console.WriteLine("Screen:");
    for (int row = 0; row < screen.GetLength(0); row++)
    {
        for (int col = 0; col < screen.GetLength(1); col++)
        {
            Console.Write($"{screen[row, col]} ");
        }
        Console.WriteLine();
    }
    Winning winningWays = waysCalculator.CalculateWins(screen);

    Console.WriteLine("Total wins: " + winningWays.totalWin);
    foreach (var item in winningWays.winnings)
    {
        Console.WriteLine($"- Ways win {string.Join("-", item.winningPositions)}, {item.symbolWin}, x{item.quantity}, {item.creditsWon}");
    }
}
#endregion

#region Lines
var reelSetsForLines = new List<List<string>>
    {
        new List<string> { "hv2", "lv3", "lv3", "hv1", "hv1", "lv1", "hv1", "hv4", "lv1", "hv3", "hv2", "hv3", "lv4", "hv4", "lv1", "hv2", "lv4", "lv1", "lv3", "hv2" },
        new List<string> { "hv1", "lv2", "lv3", "lv2", "lv1", "lv1", "lv4", "lv1", "lv1", "hv4", "lv3", "hv2", "lv1", "lv3", "hv1", "lv1", "lv2", "lv4", "lv3", "lv2" },
        new List<string> { "lv1", "hv2", "lv3", "lv4", "hv3", "hv2", "lv2", "hv2", "hv2", "lv1", "hv3", "lv1", "hv1", "lv2", "hv3", "hv2", "hv4", "hv1", "lv2", "lv4" },
        new List<string> { "hv2", "lv2", "hv3", "lv2", "lv4", "lv4", "hv3", "lv2", "lv4", "hv1", "lv1", "hv1", "lv2", "hv3", "lv2", "lv3", "hv2", "lv1", "hv3", "lv2" },
        new List<string> { "lv3", "lv4", "hv2", "hv3", "hv4", "hv1", "hv3", "hv2", "hv2", "hv4", "hv4", "hv2", "lv2", "hv4", "hv1", "lv2", "hv1", "lv2", "hv4", "lv4" }
    };

Paylines paylines = new Paylines
{
    lines = new List<List<int>>
        {
            // Pay line 1
            new List<int> { 5, 6, 7, 8, 9 },
            // Pay line 2
            new List<int> { 0, 1, 2, 3, 4 },
            // Pay line 3
            new List<int> { 10, 11, 12, 13, 14 },
            // Pay line 4
            new List<int> { 0, 1, 7, 13, 14 },
            // Pay line 5
            new List<int> { 10, 11, 7, 3, 4 },
            // Pay line 6
            new List<int> { 0, 6, 12, 8, 4 },
            // Pay line 7
            new List<int> { 10, 6, 2, 8, 14 }
        }
};

Paytable paytableLines = new Paytable();
paytableLines.combinationCredits = new Dictionary<string, int[]>
    {
        { "hv1", new int[] { 10, 20, 50 } },
        { "hv2", new int[] { 5, 10, 20 } },
        { "hv3", new int[] { 5, 10, 15 } },
        { "hv4", new int[] { 5, 10, 15 } },
        { "lv1", new int[] { 2, 5, 10 } },
        { "lv2", new int[] { 1, 2, 5 } },
        { "lv3", new int[] { 1, 2, 3 } },
        { "lv4", new int[] { 1, 2, 3 } }
    };

paytableLines.combinationQuantities = new Dictionary<string, int[]>
    {
        { "hv1", new int[] { 3, 4, 5 } },
        { "hv2", new int[] { 3, 4, 5 } },
        { "hv3", new int[] { 3, 4, 5 } },
        { "hv4", new int[] { 3, 4, 5 } },
        { "lv1", new int[] { 3, 4, 5 } },
        { "lv2", new int[] { 3, 4, 5 } },
        { "lv3", new int[] { 3, 4, 5 } },
        { "lv4", new int[] { 3, 4, 5 } }
    };

var slotMachineLines = new SlotMachine(reelSetsForLines, 5, 3);
var linesCalculator = new LinesCalculator(paytableLines, paylines, slotMachineLines.GetTotalRows(), slotMachineLines.GetTotalReels());

void SpinLines()
{
    slotMachineLines.Spin();
    Console.WriteLine("LINES: ");
    var (stopPositionsLines, screenLines) = slotMachineLines.Spin();
    Console.WriteLine($"Stop Positions: {string.Join(", ", stopPositionsLines)}");
    Console.WriteLine("Screen:");
    for (int row = 0; row < screenLines.GetLength(0); row++)
    {
        for (int col = 0; col < screenLines.GetLength(1); col++)
        {
            Console.Write($"{screenLines[row, col]} ");
        }
        Console.WriteLine();
    }

    Winning lineWinnings = linesCalculator.CalculateWins(screenLines);

    Console.WriteLine("Total wins: " + lineWinnings.totalWin);
    foreach (var item in lineWinnings.winnings)
    {
        Console.WriteLine($"- Payline {item.lineId}, {item.symbolWin}, x{item.quantity}, {item.creditsWon}");
    }
}
#endregion


void ExecuteSpin()
{
    Console.WriteLine();
    SpinWays();
    Console.WriteLine();
    SpinLines();
    Console.WriteLine("/////////////////////////////////////////////");
}

Console.WriteLine("Press space to simulate spin");
while (true)
{
    if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
    {
        ExecuteSpin();
    }
}