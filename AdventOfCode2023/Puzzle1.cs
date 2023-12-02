using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle1
{
    static string[] digitWords = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    public static int Calculate()
    {
        int sum = 0;
        


        using (StreamReader sr = new StreamReader(@"./Inputs/puzzle1.txt"))
        {
            string? line = "";
            bool hasSeenFirstDigit = false;
            char firstDigit = '0';
            char lastDigit = '0';

            line = sr.ReadLine();
            while (line != null)
            {
                hasSeenFirstDigit = false;
                for(int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    
                    for(int j = 0; j < digitWords.Length; j++)
                    {
                        string word = digitWords[j];

                        int distanceToEnd = line.Length - i;

                        if (line.Substring(i, Math.Min(5, distanceToEnd)).StartsWith(word))
                        {
                            c = (char)('1' + j);
                            break;
                        }
                    }

                    if (char.IsDigit(c))
                    {
                        if (!hasSeenFirstDigit)
                        {
                            sum += (int)char.GetNumericValue(c) * 10;
                            firstDigit = c;
                            hasSeenFirstDigit = true;
                        }
                        lastDigit = c;
                    }
                }

                sum += (int)char.GetNumericValue(lastDigit);

                line = sr.ReadLine();
            }
        }

        return sum;
    }
    
}
