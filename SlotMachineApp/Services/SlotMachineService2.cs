using System;
using Newtonsoft.Json;
using System.Collections.Generic;

public class SlotMachine2
{
    private static readonly string[] symbols = { "cherry", "bomb", "emas", "uang" };

    private static readonly Dictionary<string, string> symbolWinTable = new Dictionary<string, string>
    {
        { "cherry", "Jackpot" },
        { "bomb", "Lose" },
        { "emas", "Mega Win" },
        { "uang", "Super Win" }
    };

    private enum WinStatus
    {
        Lose,
        Jackpot,
        MegaWin,
        SuperWin,
        RandomWin
    }

    private static readonly List<string> winTypes = new List<string>
    {
        "Lose",    
        "Jackpot",
        "Wild",
        "Bonus",
        "Free Spins",
        "Mega Win",
        "Super Win",
        "Sticky Wild"
    };

    private static readonly Random rand = new Random();

    public static (string, string, string) Spin()
    {
        string symbol1 = symbols[rand.Next(symbols.Length)];
        string symbol2 = symbols[rand.Next(symbols.Length)];
        string symbol3 = symbols[rand.Next(symbols.Length)];

        return (symbol1, symbol2, symbol3);
    }

    public static string DetermineWin(string symbol1, string symbol2, string symbol3)
    {
        if (symbol1 == symbol2 && symbol2 == symbol3)
        {
            if (symbolWinTable.ContainsKey(symbol1))
            {
                return symbolWinTable[symbol1];
            }
        }

        int randomIndex = rand.Next(winTypes.Count);
        return winTypes[randomIndex];
    }
}
