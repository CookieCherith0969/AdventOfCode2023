using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Puzzle12
{

    public static int CalculateOne()
    {
        return -1;
        //string[] lines = File.ReadAllLines(@"./Inputs/puzzle12.txt");
        //
        //
        //int sum = 0;
        //List<sbyte> groupSizes = new List<sbyte>();
        //
        //
        //for (int y = 0; y < lines.Length; y++)
        //{
        //    groupSizes.Clear();
        //    string line = lines[y];
        //    string[] dataSplit = line.Split(' ');
        //
        //    int brokenTotal = 0;
        //    string[] groupSplit = dataSplit[1].Split(',');
        //    foreach(string groupSize in groupSplit)
        //    {
        //        int size = int.Parse(groupSize);
        //        groupSizes.Add(sbyte.Parse(groupSize));
        //        brokenTotal += size;
        //    }
        //
        //    string springs = dataSplit[0];
        //    List<sbyte> springSequence = StringToSpringSequence(springs);
        //    SpringState newState = new SpringState(springSequence, groupSizes);
        //    int arrangements = SpringState.ArrangementsOf(newState);
        //    if(debugResult) Console.WriteLine($"{newState} => {arrangements}");
        //    sum += arrangements;
        //}
        ////Console.WriteLine("Cache hits: "+SpringState.cacheHits.ToString());
        //return sum;
    }

    public static int CalculateTwo()
    {
        return -1;
        //string[] lines = File.ReadAllLines(@"./Inputs/puzzle12.txt");
        //
        //
        //int sum = 0;
        //List<sbyte> groupSizes = new List<sbyte>();
        //
        //int maxSprings = 0;
        //int maxSpringLen = 0;
        //int maxGroups = 0;
        //int maxGroupLen = 0;
        //
        //for (int y = 0; y < lines.Length; y++)
        //{
        //    groupSizes.Clear();
        //    string line = lines[y];
        //    string[] dataSplit = line.Split(' ');
        //
        //    int brokenTotal = 0;
        //    string[] groupSplit = dataSplit[1].Split(',');
        //    foreach (string groupSize in groupSplit)
        //    {
        //        int size = int.Parse(groupSize);
        //        groupSizes.Add(sbyte.Parse(groupSize));
        //        brokenTotal += size;
        //    }
        //    List<sbyte> originalSizes = groupSizes.ToList();
        //    string springs = dataSplit[0];
        //    string originalSprings = springs;
        //    for (int i = 0; i < unfoldMultipler - 1; i++)
        //    {
        //        groupSizes.AddRange(originalSizes);
        //
        //        springs += "?" + originalSprings;
        //    }
        //
        //    List<sbyte> springSequence = StringToSpringSequence(springs);
        //
        //    if (springSequence.Count > maxSprings) maxSprings = springSequence.Count;
        //    if (groupSizes.Count > maxGroups) maxGroups = groupSizes.Count;
        //    for (int i = 0; i < groupSizes.Count; i++) { if (groupSizes[i] > maxGroupLen) maxGroupLen = groupSizes[i]; }
        //    for (int i = 0; i < springSequence.Count; i++) { if (Math.Abs(springSequence[i]) > maxSpringLen) maxSpringLen = Math.Abs(springSequence[i]); }
        //
        //    // continue;
        //
        //    SpringState newState = new SpringState(springSequence, groupSizes);
        //    int arrangements = SpringState.ArrangementsOf(newState);
        //    if (debugResult) Console.WriteLine($"{newState} => {arrangements}");
        //    sum += arrangements;
        //}
        //
        //Console.WriteLine($"Springs: {maxSprings}");
        //Console.WriteLine($"Spring length: {maxSpringLen}");
        //Console.WriteLine($"Groups: {maxGroups}");
        //Console.WriteLine($"Group length: {maxGroupLen}");
        //
        //Console.WriteLine("Cache hits: " + SpringState.cacheHits.ToString());
        //return sum;
    }

}