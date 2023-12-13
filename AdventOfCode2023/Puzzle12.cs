using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Puzzle12
{
    const bool debugTrim = false;
    const bool debugTrivial = false;
    const bool debugGenerate = false;
    const bool debugResult = false;

    const bool useCaching = true;

    const char unsureChar = '?';
    const char brokenChar = '#';
    const char workingChar = '.';
    const int unfoldMultipler = 2;

    private readonly struct SpringState
    {
        public readonly List<sbyte> Springs;
        public readonly List<sbyte> GroupSizes;
        

        private static Dictionary<SpringState, int> seenStates = new Dictionary<SpringState, int>();
        public static int cacheHits = 0;

        public SpringState(List<sbyte> springs, List<sbyte> groupSizes)
        {
            Springs = springs;
            GroupSizes = groupSizes;
        }

        
        public int TrivialPossibilities()
        {
            int groupIndex = 0;
            bool isSatisfied = false;
            bool hasUncertainty = false;
            bool canSatisfy = true;

            if (Springs.Count((i) => i > 0) != GroupSizes.Count)
            {
                canSatisfy = false;
            }
            if (GroupSizes.Count == 0)
            {
                if (!canSatisfy)
                {
                    return 0;
                }
                return 1;
            }

            foreach (int springSet in Springs)
            {
                if (canSatisfy && !isSatisfied)
                {
                    if(springSet == GroupSizes[groupIndex])
                    {
                        groupIndex++;
                        if (groupIndex == GroupSizes.Count)
                        {
                            isSatisfied = true;
                        }
                    }
                    else
                    {
                        canSatisfy = false;
                    }
                    
                }
                if (!hasUncertainty && springSet < 0)
                {
                    hasUncertainty = true;
                }
            }

            if (isSatisfied)
            {
                return 1;
            }
            if (!hasUncertainty)
            {
                return 0;
            }
            return -1;
        }
        public SpringState[] GenerateNextPossibilities()
        {
            int firstUncertainIndex = -1;
            for(int i = 0; i < Springs.Count; i++)
            {
                if (Springs[i] < 0)
                {
                    firstUncertainIndex = i;
                    break;
                }
            }
            if(firstUncertainIndex < 0)
            {
                return Array.Empty<SpringState>();
            }
            sbyte uncertainValue = Springs[firstUncertainIndex];
            List<sbyte> assumeWorking = Springs.ToList();
            List<sbyte> assumeBroken = Springs.ToList();
            bool emptyLeft = firstUncertainIndex == 0 || Springs[firstUncertainIndex-1] == 0;
            if(uncertainValue < -1)
            {
                if (emptyLeft)
                {
                    assumeWorking[firstUncertainIndex] = (sbyte)(uncertainValue + 1);
                    assumeBroken[firstUncertainIndex] = (sbyte)(uncertainValue + 1);
                    assumeBroken.Insert(firstUncertainIndex, 1);
                }
                else
                {
                    assumeWorking[firstUncertainIndex] = (sbyte)(uncertainValue + 1);
                    assumeWorking.Insert(firstUncertainIndex, 0);
                    assumeBroken[firstUncertainIndex] = (sbyte)(uncertainValue + 1);
                    int prevValue = assumeBroken[firstUncertainIndex - 1];
                    assumeBroken[firstUncertainIndex - 1] = (sbyte)(prevValue + 1);
                }
            }
            else
            {
                bool emptyRight = firstUncertainIndex == Springs.Count -1 || Springs[firstUncertainIndex + 1] == 0;

                if (emptyLeft && emptyRight)
                {
                    if (firstUncertainIndex != assumeWorking.Count - 1)
                    {
                        assumeWorking.RemoveAt(firstUncertainIndex + 1);
                    }
                    assumeWorking.RemoveAt(firstUncertainIndex);
                    assumeBroken[firstUncertainIndex] = 1;
                }
                else if (emptyLeft)
                {
                    assumeWorking.RemoveAt(firstUncertainIndex);
                    int prevValue = assumeBroken[firstUncertainIndex + 1];
                    assumeBroken[firstUncertainIndex + 1] = (sbyte)(prevValue + 1);
                    assumeBroken.RemoveAt(firstUncertainIndex);
                }
                else if (emptyRight)
                {
                    assumeWorking.RemoveAt(firstUncertainIndex);
                    int prevValue = assumeBroken[firstUncertainIndex - 1];
                    assumeBroken[firstUncertainIndex - 1] = (sbyte)(prevValue + 1);
                    assumeBroken.RemoveAt(firstUncertainIndex);
                }
                else
                {
                    assumeWorking[firstUncertainIndex] = 0;
                    int leftValue = assumeBroken[firstUncertainIndex - 1];
                    int rightValue = assumeBroken[firstUncertainIndex + 1];
                    assumeBroken[firstUncertainIndex-1] = (sbyte)(leftValue + 1 + rightValue);
                    assumeBroken.RemoveAt(firstUncertainIndex+1);
                    assumeBroken.RemoveAt(firstUncertainIndex);
                }
            }
            SpringState[] possibilities = new SpringState[2];
            possibilities[0] = new SpringState(assumeWorking, GroupSizes.ToList());
            possibilities[1] = new SpringState(assumeBroken, GroupSizes.ToList());
            return possibilities;
        }

        private static SpringState TrimState(SpringState state)
        {
            int initCount = state.GroupSizes.Count;

            SpringState newState = new SpringState(state.Springs.ToList(), state.GroupSizes.ToList());
            if(newState.GroupSizes.Count == 0)
            {
                return newState;
            }
            for(int i = 0; i < newState.Springs.Count; i++)
            {
                int springSet = newState.Springs[i];

                if(springSet < 0)
                {
                    break;
                }
                if(springSet == newState.GroupSizes[0])
                {
                    if(i != newState.Springs.Count - 1 && newState.Springs[i+1] == 0)
                    {
                        newState.GroupSizes.RemoveAt(0);

                        newState.Springs.RemoveAt(i + 1);

                        newState.Springs.RemoveAt(i);
                        i--;
                        if (newState.GroupSizes.Count == 0)
                        {
                            if (debugTrim && newState.GroupSizes.Count != initCount) Console.WriteLine($"TrimState({state}) => {newState}");
                            return newState;
                        }
                    }
                    
                }
                else if(springSet > newState.GroupSizes[0])
                {
                    newState.Springs.Clear();
                    if (debugTrim) Console.WriteLine($"TrimState({state}) => {newState}");
                    return newState;
                }
                else if(springSet != newState.GroupSizes[0] && springSet != 0)
                {
                    break;
                }
            }
            for(int i = newState.Springs.Count - 1; i >= 0; i--)
            {
                int springSet = newState.Springs[i];

                if (springSet < 0)
                {
                    break;
                }
                if (springSet == newState.GroupSizes[^1])
                {
                    
                    if (i != 0 && newState.Springs[i - 1] == 0)
                    {
                        newState.GroupSizes.RemoveAt(newState.GroupSizes.Count - 1);

                        newState.Springs.RemoveAt(i - 1);
                        i--;

                        newState.Springs.RemoveAt(i);
                        if (newState.GroupSizes.Count == 0)
                        {
                            if (debugTrim && newState.GroupSizes.Count != initCount) Console.WriteLine($"TrimState({state}) => {newState}");
                            return newState;
                        }
                    }
                }
                else if (springSet > newState.GroupSizes[^1])
                {
                    newState.Springs.Clear();
                    if (debugTrim) Console.WriteLine($"TrimState({state}) => {newState}");
                    return newState;
                }
                else if (springSet != newState.GroupSizes[^1] && springSet != 0)
                {
                    break;
                }
            }

            if (debugTrim && newState.GroupSizes.Count != initCount) Console.WriteLine($"TrimState({state}) => {newState}");
            return newState;
        }

        public static int ArrangementsOf(SpringState state)
        {
            state = TrimState(state);
            int trivialPossibilities = state.TrivialPossibilities();
            if (debugTrivial) Console.WriteLine($"TrivialPossibilities({state}) => {trivialPossibilities} {(trivialPossibilities == 1 ? "-------------------------" : "")}");
            
            if (trivialPossibilities != -1)
            {
                return trivialPossibilities;
            }
            if (useCaching && seenStates.TryGetValue(state, out int arrangements))
            {
                cacheHits++;
                return arrangements;
            }
            
            SpringState[] possibilities = state.GenerateNextPossibilities();
            if (possibilities.Length == 0)
            {
                if(debugGenerate) Console.WriteLine($"GenerateNextPossibilities({state}) => []");
                return 0;
            }
            if(debugGenerate) Console.WriteLine($"GenerateNextPossibilities({state}) =>\n\t{possibilities[0]}\n\t{possibilities[1]}");
            int newArrangements = ArrangementsOf(possibilities[0]) + ArrangementsOf(possibilities[1]);
            if(useCaching) seenStates.TryAdd(state, newArrangements);
            return newArrangements;
        }

        public override bool Equals(object? obj)
        {
            return obj is SpringState state &&
                    state.Springs.SequenceEqual(Springs) &&
                    state.GroupSizes.SequenceEqual(GroupSizes);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (int springSet in Springs)
                {
                    hash = hash * 31 + springSet.GetHashCode();
                }
                foreach (int groupSize in GroupSizes)
                {
                    hash = hash * 31 + groupSize.GetHashCode();
                }
                return hash;
            }
        }

        public static bool operator ==(SpringState left, SpringState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SpringState left, SpringState right)
        {
            return !(left == right);
        }

        public override string? ToString()
        {
            string result = string.Empty;
            foreach(int springSet in Springs)
            {
                if(springSet == 0) 
                {
                    result += ".";
                }
                else if(springSet > 0)
                {
                    for(int i = 0; i < springSet; i++)
                    {
                        result += "#";
                    }
                }
                else
                {
                    for(int i = 0; i < -springSet; i++)
                    {
                        result += "?";
                    }
                }
                result += "|";
            }
            result += " ";
            foreach(int groupSize in GroupSizes)
            {
                result += groupSize.ToString() + ",";
            }
            return result;
        }
    }

    private static List<sbyte> StringToSpringSequence(string s)
    {
        List<sbyte> sequence = new List<sbyte>();
        char prevC = workingChar;
        sbyte currentCount = 0;
        for(int i = 0; i < s.Length ; i++)
        {
            char c = s[i];
            
            if (c != prevC)
            {
                if(prevC != workingChar)
                {
                    if(prevC == brokenChar)
                    {
                        sequence.Add(currentCount);
                    }
                    else
                    {
                        sequence.Add((sbyte)-currentCount);
                    }
                    currentCount = 0;
                }
                if(c == workingChar)
                {
                    sequence.Add(0);
                }
            }
            if (c != workingChar)
            {
                currentCount++;
            }

            prevC = c;
        }
        if(currentCount > 0)
        {
            if (s[^1] == brokenChar)
            {
                sequence.Add(currentCount);
            }
            else
            {
                sequence.Add((sbyte)-currentCount);
            }
        }
        if (sequence[^1] == 0) 
        { 
            sequence.RemoveAt(sequence.Count - 1);
        }

        return sequence;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle12.txt");


        int sum = 0;
        List<sbyte> groupSizes = new List<sbyte>();

        
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
                groupSizes.Add(sbyte.Parse(groupSize));
                brokenTotal += size;
            }

            string springs = dataSplit[0];
            List<sbyte> springSequence = StringToSpringSequence(springs);
            SpringState newState = new SpringState(springSequence, groupSizes);
            int arrangements = SpringState.ArrangementsOf(newState);
            if(debugResult) Console.WriteLine($"{newState} => {arrangements}");
            sum += arrangements;
        }
        //Console.WriteLine("Cache hits: "+SpringState.cacheHits.ToString());
        return sum;
    }

    public static int CalculateTwo()
    {
        return -1;
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle12.txt");


        int sum = 0;
        List<sbyte> groupSizes = new List<sbyte>();

        int maxSprings = 0;
        int maxSpringLen = 0;
        int maxGroups = 0;
        int maxGroupLen = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            groupSizes.Clear();
            string line = lines[y];
            string[] dataSplit = line.Split(' ');

            int brokenTotal = 0;
            string[] groupSplit = dataSplit[1].Split(',');
            foreach (string groupSize in groupSplit)
            {
                int size = int.Parse(groupSize);
                groupSizes.Add(sbyte.Parse(groupSize));
                brokenTotal += size;
            }
            List<sbyte> originalSizes = groupSizes.ToList();
            string springs = dataSplit[0];
            string originalSprings = springs;
            for (int i = 0; i < unfoldMultipler - 1; i++)
            {
                groupSizes.AddRange(originalSizes);

                springs += "?" + originalSprings;
            }

            List<sbyte> springSequence = StringToSpringSequence(springs);

            if (springSequence.Count > maxSprings) maxSprings = springSequence.Count;
            if (groupSizes.Count > maxGroups) maxGroups = groupSizes.Count;
            for (int i = 0; i < groupSizes.Count; i++) { if (groupSizes[i] > maxGroupLen) maxGroupLen = groupSizes[i]; }
            for (int i = 0; i < springSequence.Count; i++) { if (Math.Abs(springSequence[i]) > maxSpringLen) maxSpringLen = Math.Abs(springSequence[i]); }

            // continue;

            SpringState newState = new SpringState(springSequence, groupSizes);
            int arrangements = SpringState.ArrangementsOf(newState);
            if (debugResult) Console.WriteLine($"{newState} => {arrangements}");
            sum += arrangements;
        }

        Console.WriteLine($"Springs: {maxSprings}");
        Console.WriteLine($"Spring length: {maxSpringLen}");
        Console.WriteLine($"Groups: {maxGroups}");
        Console.WriteLine($"Group length: {maxGroupLen}");

        Console.WriteLine("Cache hits: " + SpringState.cacheHits.ToString());
        return sum;
    }

}