using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle12
{
    private readonly record struct Pos(int X, int Y);

    const char unsureChar = '?';
    const char brokenChar = '#';
    const char workingChar = '.';

    private static List<int> ExtractGroupSizes(string springs, bool[] brokenUnsures)
    {
        int unsureIndex = 0;
        List<int> sizes = new List<int>();
        int currentSize = 0;
        for(int i = 0; i < springs.Length; i++)
        {
            char spring = springs[i];
            if(spring == unsureChar)
            {
                if (brokenUnsures[unsureIndex])
                {
                    currentSize++;
                }
                else if(currentSize > 0)
                {
                    sizes.Add(currentSize);
                    currentSize = 0;
                }
                unsureIndex++;
            }
            else if(spring == brokenChar)
            {
                currentSize++;
            }
            else if(currentSize > 0)
            {
                sizes.Add(currentSize);
                currentSize = 0;
            }
        }
        if(currentSize > 0)
        {
            sizes.Add(currentSize);
        }
        return sizes;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle12.txt");


        int sum = 0;
        List<int> groupSizes = new List<int>();
        bool[] fullRows = new bool[lines.Length];
        bool[] fullColumns = new bool[lines[0].Length];
        for (int y = 0; y < lines.Length; y++)
        {
            groupSizes.Clear();
            string line = lines[y];
            string[] dataSplit = line.Split(' ');

            int brokenTotal = 0;
            string[] groupSplit = dataSplit[1].Split(',');
            foreach(string groupSize in groupSplit)
            {
                int size = int.Parse(groupSize);
                groupSizes.Add(int.Parse(groupSize));
                brokenTotal += size;
            }

            string springs = dataSplit[0];
            int workingTotal = springs.Length - brokenTotal;

            int unsureNum = springs.Count((c) => c == unsureChar);
            int workingNum = springs.Count((c) => c == workingChar);
            int brokenNum = springs.Count((c) => c == brokenChar);

            int missingBroken = brokenTotal - brokenNum;

            bool[] brokenUnsures = new bool[unsureNum];
            for(int i = 0; i < (1 << unsureNum); i++)
            {
                for(int j = 0; j < brokenUnsures.Length; j++)
                {
                    if((i & (1 << j)) != 0)
                    {
                        brokenUnsures[j] = true;
                    }
                    else
                    {
                        brokenUnsures[j] = false;
                    }
                }
                if (brokenUnsures.Count((b) => b) != missingBroken)
                {
                    continue;
                }
                if (ExtractGroupSizes(springs, brokenUnsures).SequenceEqual(groupSizes))
                {
                    sum++;
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