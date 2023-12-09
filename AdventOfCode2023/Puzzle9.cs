using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle9
{
    private readonly record struct Node();

    private static int CompareCyclesByLength()
    {
        return -1;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle9.txt");

        int sum = 0;

        List<int> currentRow = new List<int>();
        List<int> lastColumn = new List<int>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            string[] data = line.Split(' ');

            for (int d = 0; d < data.Length; d++)
            {
                currentRow.Add(int.Parse(data[d]));
            }

            int depth = 0;
            while (currentRow.Any((num) => num != 0))
            {
                depth++;
                List<int> nextRow = new List<int>();
                for(int index = 0; index < currentRow.Count-1; index++)
                {
                    int directAbove = currentRow[index];
                    int nextAbove = currentRow[index + 1];
                    nextRow.Add(nextAbove - directAbove);
                }
                lastColumn.Add(currentRow[^1]);
                currentRow = nextRow;
            }

            int currentValue = 0;
            for(; depth > 0; depth--)
            {
                currentValue = lastColumn[depth-1] + currentValue;
            }
            sum += currentValue;

            currentRow.Clear();
            lastColumn.Clear();
        }




        return sum;
    }

    public static long CalculateTwo()
    {
        return -1;
    }

}