using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle14
{
    const char rockChar = 'O';
    const char obstacleChar = '#';
    private static int IndexOfHighestBelow(List<int> list, int max)
    {
        for(int i = list.Count - 1; i >=0; i--)
        {
            int item = list[i];
            if (item < max) 
            {
                return i;
            }
        }
        return -1;
    }
    private static int HighestBelow(List<int> obstacles, List<int> rocks, int max)
    {
        int highObstacle = -1;
        for (int i = obstacles.Count - 1; i >= 0; i--)
        {
            int obstacle = obstacles[i];
            if (obstacle < max)
            {
                highObstacle = obstacle;
                break;
            }
        }
        int highRock = -1;
        for (int i = rocks.Count - 1; i >= 0; i--)
        {
            int rock = rocks[i];
            if (rock < max)
            {
                highRock = rock;
                break;
            }
        }
        return Math.Max(highRock, highObstacle);
    }
    private static int LowestAbove(List<int> obstacles, List<int> rocks, int min, int end)
    {
        int lowObstacle = end;
        for (int i = 0; i < obstacles.Count; i++)
        {
            int obstacle = obstacles[i];
            if (obstacle > min)
            {
                lowObstacle = obstacle;
                break;
            }
        }
        int lowRock = end;
        for (int i = 0; i < rocks.Count; i++)
        {
            int rock = rocks[i];
            if (rock > min)
            {
                lowRock = rock;
                break;
            }
        }
        return Math.Min(lowRock, lowObstacle);
    }
    private static int TotalLoad(List<int>[] rowRocks, int height)
    {
        int load = 0;
        for(int y = 0; y < height; y++)
        {
            List<int> rockRow = rowRocks[y];
            foreach(int rock in rockRow) 
            {
                load += height - y;
            }
        }
        return load;
    }
    private static void SwapColumnRowFormat(List<int>[] box, out List<int>[] swappedBox)
    {
        swappedBox = new List<int>[box.Length];
        for(int i = 0; i < swappedBox.Length; i++)
        {
            swappedBox[i] = new List<int>();
        }
        for(int x = 0; x < box.Length; x++)
        {
            List<int> col = box[x];
            foreach(int y in col)
            {
                swappedBox[y].Add(x);
            }
        }
    }
    private static void RollUp(List<int>[] colObstacles, ref List<int>[] rowRocks)
    {
        List<int>[] colRocks;
        SwapColumnRowFormat(rowRocks, out colRocks);

        for (int x = 0; x < colObstacles.Length; x++)
        {
            List<int> obstacles = colObstacles[x];
            List<int> rocks = colRocks[x];
            if (obstacles.Count == 0)
            {
                for (int i = 0; i < rocks.Count; i++)
                {
                    rocks[i] = i;
                }
                continue;
            }

            for (int i = 0; i < rocks.Count; i++)
            {
                int rock = rocks[i];
                int nextBlocker = HighestBelow(obstacles, rocks, rock);
                rocks[i] = nextBlocker + 1;
            }
        }
        SwapColumnRowFormat(colRocks, out rowRocks);
    }
    private static void RollLeft(List<int>[] rowObstacles, ref List<int>[] rowRocks)
    {
        for (int x = 0; x < rowObstacles.Length; x++)
        {
            List<int> obstacles = rowObstacles[x];
            List<int> rocks = rowRocks[x];
            if (obstacles.Count == 0)
            {
                for (int i = 0; i < rocks.Count; i++)
                {
                    rocks[i] = i;
                }
                continue;
            }

            for (int i = 0; i < rocks.Count; i++)
            {
                int rock = rocks[i];
                int nextBlocker = HighestBelow(obstacles, rocks, rock);
                rocks[i] = nextBlocker + 1;
            }
        }
        
    }
    private static void RollDown(List<int>[] colObstacles, ref List<int>[] rowRocks, int height)
    {
        List<int>[] colRocks;
        SwapColumnRowFormat(rowRocks, out colRocks);

        for (int x = 0; x < colObstacles.Length; x++)
        {
            List<int> obstacles = colObstacles[x];
            List<int> rocks = colRocks[x];
            if (obstacles.Count == 0)
            {
                for (int i = 0; i < rocks.Count; i++)
                {
                    rocks[i] = height-i-1;
                }
                continue;
            }

            for (int i = rocks.Count-1; i >= 0; i--)
            {
                int rock = rocks[i];
                int nextBlocker = LowestAbove(obstacles, rocks, rock, height);
                rocks[i] = nextBlocker - 1;
            }
        }
        SwapColumnRowFormat(colRocks, out rowRocks);
    }
    private static void RollRight(List<int>[] rowObstacles, ref List<int>[] rowRocks, int width)
    {
        for (int x = 0; x < rowObstacles.Length; x++)
        {
            List<int> obstacles = rowObstacles[x];
            List<int> rocks = rowRocks[x];
            if (obstacles.Count == 0)
            {
                for (int i = 0; i < rocks.Count; i++)
                {
                    rocks[i] = width - i - 1;
                }
                continue;
            }

            for (int i = rocks.Count - 1; i >= 0; i--)
            {
                int rock = rocks[i];
                int nextBlocker = LowestAbove(obstacles, rocks, rock, width);
                rocks[i] = nextBlocker - 1;
            }
        }
    }
    private static int IndexInCache(List<List<int>[]> cache, List<int>[] colRocks)
    {
        for(int i = 0; i < cache.Count; i++)
        {
            List<int>[] cacheRocks = cache[i];

            bool matches = true;
            for(int x = 0; x < cacheRocks.Length; x++)
            {
                List<int> cacheCol = cacheRocks[x];
                List<int> rockCol = colRocks[x];
                if (!cacheCol.SequenceEqual(rockCol))
                {
                    matches = false; 
                    break;
                }
            }
            if (matches)
            {
                return i;
            }
        }
        return -1;
    }
    private static void CycleRocks(List<int>[] rowObstacles, ref List<int>[] rowRocks, int height, int width, int cycles)
    {
        List<List<int>[]> cache = new List<List<int>[]>();
        List<int>[] colObstacles;
        SwapColumnRowFormat(rowObstacles, out colObstacles);

        for(int cycleCount = 0; cycleCount < cycles; cycleCount++)
        {
            int cacheIndex = IndexInCache(cache, rowRocks);
            if (cacheIndex != -1)
            {
                int loopSize = cycleCount - cacheIndex;
                int remainingCycles = cycles - cacheIndex - loopSize;

                int remainingSteps = remainingCycles%loopSize;
                rowRocks = cache[cacheIndex + remainingSteps];
                return;
            }

            cache.Add(rowRocks);
            RollUp(colObstacles, ref rowRocks);
            RollLeft(rowObstacles, ref rowRocks);
            RollDown(colObstacles, ref rowRocks, height);
            RollRight(rowObstacles, ref rowRocks, width);
        }

    }
    public static string BoxToString(List<int>[] rowObstacles, List<int>[] rowRocks, int height, int width)
    {
        string result = string.Empty;
        StringBuilder sb = new StringBuilder();
        string emptyRow = string.Empty;
        for(int i = 0; i < width; i++)
        {
            emptyRow += '.';
        }
        emptyRow += '\n';

        for(int y = 0; y < height; y++)
        {
            sb.Clear();
            sb.Append(emptyRow);
            List<int> obstacleRow = rowObstacles[y];
            for (int i = 0; i < obstacleRow.Count; i++)
            {
                int obstacle = obstacleRow[i];
                sb.Remove(obstacle, 1);
                sb.Insert(obstacle, obstacleChar);
            }
            List<int> rockRow = rowRocks[y];
            for (int i = 0; i < rockRow.Count; i++)
            {
                int rock = rockRow[i];
                sb.Remove(rock, 1);
                sb.Insert(rock, rockChar);
            }
            result += sb.ToString();
        }
        return result;
    }
    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle14.txt");

        List<int>[] colObstacles = new List<int>[lines[0].Length];
        for(int i = 0; i < colObstacles.Length; i++)
        {
            colObstacles[i] = new List<int>();
        }
        List<int>[] colRocks = new List<int>[lines[0].Length];
        for (int i = 0; i < colRocks.Length; i++)
        {
            colRocks[i] = new List<int>();
        }
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            for(int x = 0; x < line.Length; x++)
            {
                char rock = line[x];

                if(rock == obstacleChar)
                {
                    colObstacles[x].Add(y);
                }
                else if(rock == rockChar)
                {
                    colRocks[x].Add(y);
                }
            }
        }

        RollUp(colObstacles, ref colRocks);
        return TotalLoad(colRocks, lines.Length);
    }

    public static long CalculateTwo()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle14.txt");

        List<int>[] rowObstacles = new List<int>[lines.Length];
        for (int i = 0; i < rowObstacles.Length; i++)
        {
            rowObstacles[i] = new List<int>();
        }
        List<int>[] rowRocks = new List<int>[lines.Length];
        for (int i = 0; i < rowRocks.Length; i++)
        {
            rowRocks[i] = new List<int>();
        }
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            for (int x = 0; x < line.Length; x++)
            {
                char rock = line[x];

                if (rock == obstacleChar)
                {
                    rowObstacles[y].Add(x);
                }
                else if (rock == rockChar)
                {
                    rowRocks[y].Add(x);
                }
            }
        }

        CycleRocks(rowObstacles, ref rowRocks, lines.Length, lines[0].Length, 1000000000);
        return TotalLoad(rowRocks, lines.Length);
    }

}
