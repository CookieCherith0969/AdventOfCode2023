using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle3
{
    const char emptyChar = '.';

    private static bool IsSymbolAt(string[] engine, int x, int y)
    {
        if (y < 0 || y >= engine.Length) return false;
        if (x < 0 || x >= engine[y].Length) return false;

        char c = engine[y][x];
        if (c == emptyChar) return false;
        if (char.IsDigit(c)) return false;
        return true;
    }
    private static bool IsSymbolNear(string[] engine, int x, int y)
    {
        for (int offX = -1; offX <= 1; offX++)
        {
            for (int offY = -1; offY <= 1; offY++)
            {
                if (IsSymbolAt(engine, x + offX, y + offY))
                {
                    return true;

                }
            }
        }
        return false;
    }

    public static int CalculateOne()
    {
        int sum = 0;
        string[] engine = File.ReadAllLines(@"./Inputs/puzzle3.txt");

        string? line = "";
        int id = 0;
        bool validGame = true;
        int currentNum = 0;
        bool isPartNum = false;

        for(int y = 0; y < engine.Length; y++)
        {
            string col = engine[y];
            if (col.Length == 0) break;

            if (isPartNum)
            {
                sum += currentNum;
            }
            currentNum = 0;
            isPartNum = false;
            
            for(int x = 0; x < col.Length; x++)
            {
                char c = col[x];
                if (char.IsDigit(c))
                {
                    currentNum *= 10;
                    currentNum += (int)char.GetNumericValue(c);

                    if (!isPartNum)
                    {
                        isPartNum = IsSymbolNear(engine, x, y);
                    }
                }
                else
                {
                    if (isPartNum)
                    {
                        sum += currentNum;
                    }
                    currentNum = 0;
                    isPartNum = false;
                }
            }
        }
        

        return sum;
    }

    public static int CalculateTwo()
    {
        return -1;
    }

}