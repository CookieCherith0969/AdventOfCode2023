using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle5
{
    private readonly record struct MapRange(ulong DestinationStart, ulong SourceStart, ulong Length);

    public static ulong CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle5.txt");
        //Get number of seeds by splitting first line
        //Create array of current items, fill with seeds
        //Create dictionary for current map, populate with items from ranges
        //For each current item, replace it if it's in the map, else don't touch it
        //Clear the current map, repopulate and repeat until file ends
        //Find lowest value in current items
        string[] seeds = lines[0].Split(' ');
        ulong[] currentValues = new ulong[seeds.Length-1];
        for(int i = 1; i < seeds.Length; i++)
        {
            currentValues[i-1] = ulong.Parse(seeds[i]);
        }
        List<MapRange> currentMap = new List<MapRange>();


        for (int y = 3; y < lines.Length; y++)
        {
            string line = lines[y];
            if(line.Length == 0 && currentMap.Count > 0)
            {
                for(int i = 0; i < currentValues.Length; i++)
                {
                    ulong value = currentValues[i];
                    foreach(MapRange range in currentMap)
                    {
                        if(value >= range.SourceStart && value < range.SourceStart + range.Length)
                        {
                            currentValues[i] = range.DestinationStart + (value - range.SourceStart);
                            break;
                        }
                    }
                }
                currentMap.Clear();
            }
            else if (!char.IsDigit(line[0]))
            {
                continue;
            }
            else
            {
                string[] rangeSplit = line.Split(' ');
                ulong destinationStart = ulong.Parse(rangeSplit[0]);
                ulong sourceStart = ulong.Parse(rangeSplit[1]);
                ulong rangeLength = ulong.Parse(rangeSplit[2]);

                currentMap.Add(new MapRange(destinationStart, sourceStart, rangeLength));
            }
        }

        if (currentMap.Count > 0)
        {
            for (int i = 0; i < currentValues.Length; i++)
            {
                ulong value = currentValues[i];
                foreach (MapRange range in currentMap)
                {
                    if (value >= range.SourceStart && value < range.SourceStart + range.Length)
                    {
                        currentValues[i] = range.DestinationStart + (value - range.SourceStart);
                        break;
                    }
                }
            }
            currentMap.Clear();
        }

        return currentValues.Min();
    }

    public static int CalculateTwo()
    {
        return -1;
    }

}
