using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle11
{
    private readonly record struct Pos(int X, int Y);

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle11.txt");


        List<Pos> stars = new List<Pos>();
        bool[] fullRows = new bool[lines.Length];
        bool[] fullColumns = new bool[lines[0].Length];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            for (int x = 0; x < line.Length; x++)
            {
                char space = line[x];
                if(space == '#')
                {
                    stars.Add(new Pos(x, y));
                    if (!fullColumns[x])
                    {
                        fullColumns[x] = true;
                    }
                    if (!fullRows[y])
                    {
                        fullRows[y] = true;
                    }
                }
            }
        }
        int[] emptyRows = new int[fullRows.Count((r) => !r)];
        int emptyIndex = 0;
        for(int y = 0; y < fullRows.Count(); y++)
        {
            if (!fullRows[y])
            {
                emptyRows[emptyIndex] = y;
                emptyIndex++;
            }
        }
        int[] emptyColumns = new int[fullColumns.Count((c) => !c)];
        emptyIndex = 0;
        for (int x = 0; x < fullColumns.Count(); x++)
        {
            if (!fullColumns[x])
            {
                emptyColumns[emptyIndex] = x;
                emptyIndex++;
            }
        }

        int sum = 0;
        int extraMoves = 0;
        for (int i = 0; i < stars.Count(); i++)
        {
            Pos star = stars[i];

            for(int j = i+1; j < stars.Count(); j++)
            {
                Pos otherStar = stars[j];
                int maxX = Math.Max(otherStar.X, star.X);
                int minX = Math.Min(otherStar.X, star.X);
                int maxY = Math.Max(otherStar.Y, star.Y);
                int minY = Math.Min(otherStar.Y, star.Y);
                int yDiff = maxY - minY;
                int xDiff = maxX - minX;

                
                for(int x = minX + 1; x < maxX; x++)
                {
                    if (emptyColumns.Contains(x))
                    {
                        extraMoves++;
                    }
                }
                for (int y = minY + 1; y < maxY; y++)
                {
                    if (emptyRows.Contains(y))
                    {
                        extraMoves++;
                    }
                }

                sum += yDiff + xDiff;
            }
        }
        sum += extraMoves;
        return sum;
    }

    public static long CalculateTwo()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle11.txt");


        List<Pos> stars = new List<Pos>();
        bool[] fullRows = new bool[lines.Length];
        bool[] fullColumns = new bool[lines[0].Length];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            for (int x = 0; x < line.Length; x++)
            {
                char space = line[x];
                if (space == '#')
                {
                    stars.Add(new Pos(x, y));
                    if (!fullColumns[x])
                    {
                        fullColumns[x] = true;
                    }
                    if (!fullRows[y])
                    {
                        fullRows[y] = true;
                    }
                }
            }
        }

        long sum = 0;
        int extraMoves = 0;
        for (int i = 0; i < stars.Count(); i++)
        {
            Pos star = stars[i];

            for (int j = i + 1; j < stars.Count(); j++)
            {
                Pos otherStar = stars[j];
                int maxX = Math.Max(otherStar.X, star.X);
                int minX = Math.Min(otherStar.X, star.X);
                int maxY = Math.Max(otherStar.Y, star.Y);
                int minY = Math.Min(otherStar.Y, star.Y);
                int yDiff = maxY - minY;
                int xDiff = maxX - minX;


                for (int x = minX + 1; x < maxX; x++)
                {
                    if (!fullColumns[x])
                    {
                        extraMoves++;
                    }
                }
                for (int y = minY + 1; y < maxY; y++)
                {
                    if (!fullRows[y])
                    {
                        extraMoves++;
                    }
                }

                sum += yDiff + xDiff;
            }
        }
        sum += (long)extraMoves * 999999;
        return sum;
    }

}