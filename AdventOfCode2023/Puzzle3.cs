using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle3
{
    const char emptyChar = '.';
    const char gearChar = '*';

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
    private static int IndexAt(string[] engine, int x, int y)
    {
        return y * engine.Length + x;
    }

    private static int PartIndexAt(string[] engine, List<Dictionary<string, int>> partNums, int index)
    {
        for(int i = 0; i < partNums.Count; i++)
        {
            Dictionary<string, int> partNum = partNums[i];
            int partIndex = partNum["Index"];
            int partEnd = partIndex + partNum["Length"] - 1;

            if(index >= partIndex && index <= partEnd)
            {
                return i;
            }
        }
        return -1;
    }

    private static int GearRatioAt(string[] engine, List<Dictionary<string, int>> partNums, int index)
    {
        HashSet<int> nearbyPartIndexes = new HashSet<int>();
        for(int offX = -1; offX <=1; offX++)
        {
            for (int offY = -1; offY <= 1; offY++)
            {
                int offIndex = index + offX + offY*engine.Length;
                int partIndex = PartIndexAt(engine, partNums, offIndex);
                if(partIndex != -1)
                {
                    nearbyPartIndexes.Add(partIndex);
                }
            }
        }
        if(nearbyPartIndexes.Count == 2)
        {
            int product = 1;
            foreach(int i in nearbyPartIndexes)
            {
                product *= partNums[i]["Value"];
            }
            return product;
        }
        return -1;
    }

    public static int CalculateOne()
    {
        int sum = 0;
        string[] engine = File.ReadAllLines(@"./Inputs/puzzle3.txt");

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
        int sum = 0;
        string[] engine = File.ReadAllLines(@"./Inputs/puzzle3.txt");
        List<Dictionary<string, int>> partNums = new List<Dictionary<string, int>>();
        List<int> potentialGearIndexes = new List<int>();

        int currentNum = 0;
        bool isPartNum = false;
        int numLength = 0;

        for (int y = 0; y < engine.Length; y++)
        {
            string col = engine[y];
            if (col.Length == 0) break;

            if (isPartNum)
            {
                partNums.Add(new Dictionary<string, int>{
                            { "Value", currentNum },
                            { "Index", IndexAt(engine, 0, y)-numLength },
                            { "Length", numLength }
                        });
            }
            currentNum = 0;
            isPartNum = false;
            numLength = 0;

            for (int x = 0; x < col.Length; x++)
            {
                char c = col[x];
                if (char.IsDigit(c))
                {
                    currentNum *= 10;
                    currentNum += (int)char.GetNumericValue(c);
                    numLength++;

                    if (!isPartNum)
                    {
                        isPartNum = IsSymbolNear(engine, x, y);
                    }
                }
                else
                {
                    if (isPartNum)
                    {
                        partNums.Add(new Dictionary<string, int>{ 
                            { "Value", currentNum },
                            { "Index", IndexAt(engine, x, y)-numLength },
                            { "Length", numLength } 
                        });
                    }
                    currentNum = 0;
                    isPartNum = false;
                    numLength = 0;

                    if(c == gearChar)
                    {
                        potentialGearIndexes.Add(IndexAt(engine, x, y));
                    }
                }
            }
        }

        foreach (int index in potentialGearIndexes)
        {
            int gearRatio = GearRatioAt(engine, partNums, index);
            if(gearRatio != -1) 
            {
                sum += gearRatio;
            }
        }


        return sum;
    }

}