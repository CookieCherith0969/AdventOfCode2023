using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle2
{
    static string[] colourWords = { "red", "green", "blue" };
    static int[] colourNums = { 12, 13, 14 };

    public static int CalculateOne()
    {
        int sum = 0;

        using (StreamReader sr = new StreamReader(@"./Inputs/puzzle2.txt"))
        {
            string? line = "";
            int id = 0;
            bool validGame = true;

            line = sr.ReadLine();
            while (line != null && line.Length > 0)
            {
                validGame = true;
                string[] idSplit = line.Split(':');
                id = int.Parse(idSplit[0].Split(' ')[1]);
                string[] setSplit = idSplit[1].Split(';');

                foreach (string set in setSplit)
                {
                    string[] valueSplit = set.Split(',');
                    foreach(string value in valueSplit)
                    {
                        string[] dataSplit = value.Trim().Split(' ');
                        int num = int.Parse(dataSplit[0]);
                        string colour = dataSplit[1];

                        int colourIndex = Array.IndexOf(colourWords, colour);
                        if ( num > colourNums[colourIndex] )
                        {
                            validGame = false;
                        }
                    }
                }
                if ( validGame)
                {
                    sum += id;
                }

                line = sr.ReadLine();
            }
        }

        return sum;
    }

    public static int CalculateTwo()
    {
        int sum = 0;

        using (StreamReader sr = new StreamReader(@"./Inputs/puzzle2.txt"))
        {
            string? line = "";
            int id = 0;
            bool validGame = true;

            line = sr.ReadLine();
            while (line != null && line.Length > 0)
            {
                validGame = true;
                string[] idSplit = line.Split(':');
                id = int.Parse(idSplit[0].Split(' ')[1]);
                string[] setSplit = idSplit[1].Split(';');

                foreach (string set in setSplit)
                {
                    string[] valueSplit = set.Split(',');
                    foreach (string value in valueSplit)
                    {
                        string[] dataSplit = value.Trim().Split(' ');
                        int num = int.Parse(dataSplit[0]);
                        string colour = dataSplit[1];

                        int colourIndex = Array.IndexOf(colourWords, colour);
                        if (num > colourNums[colourIndex])
                        {
                            validGame = false;
                        }
                    }
                }
                if (validGame)
                {
                    sum += id;
                }

                line = sr.ReadLine();
            }
        }

        return sum;
    }

}
