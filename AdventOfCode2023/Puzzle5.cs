using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle5
{
    private readonly record struct MapRange(ulong DestinationStart, ulong SourceStart, ulong Length);
    private readonly record struct ValueRange(ulong Start, ulong Length);
    private readonly record struct OverlapRange(ulong NewStart, ulong OverlapStart, ulong Length);

    //private static int CompareValuesByStart(ValueRange x, ValueRange y)
    //{
    //    if (x.Start > y.Start)
    //    {
    //        return 1;
    //    }
    //    else if(y.Start > x.Start)
    //    {
    //        return -1;
    //    }
    //    return 0;
    //}
    private static int CompareOverlapsByStart(OverlapRange x, OverlapRange y)
    {
        if (x.OverlapStart > y.OverlapStart)
        {
            return 1;
        }
        else if(y.OverlapStart > x.OverlapStart)
        {
            return -1;
        }
        return 0;
    }
    private static List<ValueRange> UpdateValues(List<ValueRange> currentValues, List<MapRange> currentMap)
    {
        List<ValueRange> newValues = new List<ValueRange>();
        for (int i = 0; i < currentValues.Count; i++)
        {
            ValueRange value = currentValues[i];
            List<OverlapRange> overlapRanges = new List<OverlapRange>();
            foreach (MapRange range in currentMap)
            {
                if (value.Start >= range.SourceStart && value.Start < range.SourceStart + range.Length)
                {
                    if (value.Start + value.Length <= range.SourceStart + range.Length)
                    {
                        ulong startGap = value.Start - range.SourceStart;

                        overlapRanges.Add(new OverlapRange(range.DestinationStart + startGap, value.Start, value.Length));
                        break;
                    }
                    else
                    {
                        ulong sourceEnd = range.SourceStart + range.Length - 1;
                        ulong newLength = sourceEnd - value.Start;

                        ulong startGap = value.Start - range.SourceStart;

                        overlapRanges.Add(new OverlapRange(range.DestinationStart + startGap, value.Start, newLength+1));
                    }
                }
                else if (range.SourceStart >= value.Start && range.SourceStart < value.Start + value.Length)
                {
                    if (range.SourceStart + range.Length <= value.Start + value.Length)
                    {
                        overlapRanges.Add(new OverlapRange(range.DestinationStart, range.SourceStart, range.Length));
                    }
                    else
                    {
                        ulong end = value.Start + value.Length - 1;
                        ulong newLength = end - range.SourceStart;

                        overlapRanges.Add(new OverlapRange(range.DestinationStart, range.SourceStart, newLength+1));
                    }
                }
            }

            if (overlapRanges.Count == 0)
            {
                newValues.Add(new ValueRange(value.Start, value.Length));
                continue;
            }
            overlapRanges.Sort(CompareOverlapsByStart);
            OverlapRange firstOverlap = overlapRanges[0];
            ulong firstGap = firstOverlap.OverlapStart - value.Start;
            if (firstGap > 0)
            {
                newValues.Add(new ValueRange(value.Start, firstGap));
            }
            for (int j = 0; j < overlapRanges.Count - 1; j++)
            {
                OverlapRange overlap = overlapRanges[j];
                newValues.Add(new ValueRange(overlap.NewStart, overlap.Length));
                ulong currentEnd = overlap.OverlapStart + overlap.Length - 1;
                ulong nextStart = overlapRanges[j + 1].OverlapStart;
                ulong gapSize = nextStart - currentEnd - 1;
                if (gapSize > 0)
                {
                    newValues.Add(new ValueRange(currentEnd + 1, gapSize));
                }
            }
            OverlapRange lastOverlap = overlapRanges[overlapRanges.Count - 1];
            newValues.Add(new ValueRange(lastOverlap.NewStart, lastOverlap.Length));
            ulong overlapEnd = lastOverlap.OverlapStart + lastOverlap.Length - 1;
            ulong valueEnd = value.Start + value.Length - 1;
            ulong lastGap = valueEnd - overlapEnd;
            if (lastGap > 0)
            {
                newValues.Add(new ValueRange(overlapEnd + 1, lastGap));
            }
        }
        return newValues;
    }

    public static ulong CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle5.txt");
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

    public static ulong CalculateTwo()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle5.txt");
        string[] seeds = lines[0].Split(' ');
        List<ValueRange> currentValues = new List<ValueRange>();
        for (int i = 1; i < seeds.Length; i+=2)
        {
            ulong start = ulong.Parse(seeds[i]);
            ulong length = ulong.Parse(seeds[i+1]);
            currentValues.Add(new ValueRange(start, length));
        }
        //currentValues.Sort(CompareValuesByStart);
        List<MapRange> currentMap = new List<MapRange>();


        for (int y = 3; y < lines.Length; y++)
        {
            string line = lines[y];
            if (line.Length == 0 && currentMap.Count > 0)
            {
                currentValues = UpdateValues(currentValues, currentMap);
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
            currentValues = UpdateValues(currentValues, currentMap);
            currentMap.Clear();
        }

        ulong minLocation = currentValues[0].Start;
        foreach(ValueRange range in currentValues)
        {
            if(range.Start < minLocation)
            {
                minLocation = range.Start;
            }
        }
        return minLocation;
    }

}
