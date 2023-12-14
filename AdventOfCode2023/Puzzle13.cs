using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle13
{
    private static bool ColumnEquals(List<string> box, int colOne, int colTwo)
    {
        for(int i = 0; i < box.Count; i++)
        {
            if (box[i][colOne] != box[i][colTwo])
            {
                return false;
            }
        }
        return true;
    }
    private static int RowDiscrepancies(List<string> box, int rowOne, int rowTwo)
    {
        int discrepancies = 0;
        for(int i = 0; i < box[rowOne].Length; i++) 
        { 
            char charOne = box[rowOne][i];
            char charTwo = box[rowTwo][i];
            if (charOne != charTwo)
            {
                discrepancies++;
            }
        }
        return discrepancies;
    }
    private static int ColumnDiscrepancies(List<string> box, int colOne, int colTwo)
    {
        int discrepancies = 0;
        for (int i = 0; i < box.Count; i++)
        {
            char charOne = box[i][colOne];
            char charTwo = box[i][colTwo];
            if (charOne != charTwo)
            {
                discrepancies++;
            }
        }
        return discrepancies;
    }
    private static int ReflectionValueOf(List<string> box)
    {
        int height = box.Count;
        int width = box[0].Length;

        for(int y = 0; y < height-1; y++)
        {
            int min = 0;
            int max = y * 2 + 1;

            int endDistance = height - max - 1;
            if(endDistance < 0)
            {
                min -= endDistance;
            }

            bool reflects = true;
            for(int i = min; i <= y; i++)
            {
                int reflectIndex = max - i;
                if (!box[i].Equals(box[reflectIndex]))
                {
                    reflects = false;
                    break;
                }
            }
            if (reflects)
            {
                return (y+1)*100;
            }
        }
        for (int x = 0; x < width-1; x++)
        {
            int min = 0;
            int max = x * 2 + 1;

            int endDistance = width - max - 1;
            if (endDistance < 0)
            {
                min -= endDistance;
            }

            bool reflects = true;
            for (int i = min; i <= x; i++)
            {
                int reflectIndex = max - i;
                if (!ColumnEquals(box, i, reflectIndex))
                {
                    reflects = false;
                    break;
                }
            }
            if (reflects)
            {
                return x + 1;
            }
        }
        return -1000;
    }
    private static int SmudgedReflectionValueOf(List<string> box)
    {
        int height = box.Count;
        int width = box[0].Length;

        for (int y = 0; y < height - 1; y++)
        {
            int min = 0;
            int max = y * 2 + 1;

            int endDistance = height - max - 1;
            if (endDistance < 0)
            {
                min -= endDistance;
            }

            int discrepancies = 0;
            for (int i = min; i <= y; i++)
            {
                int reflectIndex = max - i;
                discrepancies += RowDiscrepancies(box, i, reflectIndex);
                if(discrepancies > 1)
                {
                    break;
                }
            }
            if (discrepancies == 1)
            {
                return (y+1)*100;
            }
        }
        for (int x = 0; x < width - 1; x++)
        {
            int min = 0;
            int max = x * 2 + 1;

            int endDistance = width - max - 1;
            if (endDistance < 0)
            {
                min -= endDistance;
            }

            int discrepancies = 0;
            for (int i = min; i <= x; i++)
            {
                int reflectIndex = max - i;
                discrepancies += ColumnDiscrepancies(box, i, reflectIndex);
                if (discrepancies > 1)
                {
                    break;
                }
            }
            if (discrepancies == 1)
            {
                return x + 1;
            }
        }
        return -1000;
    }

        public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle13.txt");

        int sum = 0;
        List<string> currentBox = new List<string>();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            if(line.Length == 0)
            {
                sum += ReflectionValueOf(currentBox);
                currentBox.Clear();
            }
            else
            {
                currentBox.Add(line);
            }
        }
        if(currentBox.Count > 0)
        {
            sum += ReflectionValueOf(currentBox);
        }
        return sum;
    }

    public static long CalculateTwo()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle13.txt");

        int sum = 0;
        List<string> currentBox = new List<string>();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            if (line.Length == 0)
            {
                sum += SmudgedReflectionValueOf(currentBox);
                currentBox.Clear();
            }
            else
            {
                currentBox.Add(line);
            }
        }
        if (currentBox.Count > 0)
        {
            sum += SmudgedReflectionValueOf(currentBox);
        }
        return sum;
    }

}
