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
    private static int ColumnLoad(List<int> obstacles, List<int> rocks, out List<int> newRocks, int height)
    {
        int load = 0;
        newRocks = new List<int>();
        if(obstacles.Count == 0) 
        {
            for(int i = 0; i < rocks.Count; i++)
            {
                load += height - i;
                newRocks.Add(i);
            }
            return load;
        }

        for(int i = 0; i < rocks.Count; i++)
        {
            int rock = rocks[i];
            int nextObstacleIndex = IndexOfHighestBelow(obstacles, rock);
            int nextObstacle;
            if (nextObstacleIndex == -1)
            {
                nextObstacle = -1;
                newRocks.Add(0);
                obstacles.Insert(0, 0);
            }
            else 
            {
                nextObstacle = obstacles[nextObstacleIndex];
                newRocks.Add(nextObstacle + 1);
                obstacles.Insert(nextObstacleIndex+1, nextObstacle+1);
            }
            load += height - nextObstacle - 1;
            
        }
        return load;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle14.txt");

        List<int>[] columnObstacles = new List<int>[lines.Length];
        for(int i = 0; i < columnObstacles.Length; i++)
        {
            columnObstacles[i] = new List<int>();
        }
        List<int>[] columnRocks = new List<int>[lines.Length];
        for (int i = 0; i < columnRocks.Length; i++)
        {
            columnRocks[i] = new List<int>();
        }
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            for(int x = 0; x < line.Length; x++)
            {
                char rock = line[x];

                if(rock == obstacleChar)
                {
                    columnObstacles[x].Add(y);
                }
                else if(rock == rockChar)
                {
                    columnRocks[x].Add(y);
                }
            }
        }

        List<int>[] newRocks = new List<int>[lines.Length];
        int sum = 0;
        for(int i = 0; i < columnRocks.Length; i++)
        {
            sum += ColumnLoad(columnObstacles[i], columnRocks[i], out newRocks[i], lines.Length);
        }
        return sum;
    }

    public static long CalculateTwo()
    {
        return -1;
    }

}
