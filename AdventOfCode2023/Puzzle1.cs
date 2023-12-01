using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle1
{
    public static int Calculate()
    {
        int sum = 0;
        


        using (StreamReader sr = new StreamReader(@"C:\Users\cooki\source\repos\AdventOfCode2023\AdventOfCode2023\Inputs\puzzle1.txt"))
        {
            string? line = "";
            bool hasSeenFirstDigit = false;
            char lastDigit = '0';

            line = sr.ReadLine();
            while (line != null)
            {

                foreach(char c in line)
                {
                    if (char.IsDigit(c))
                    {
                        if (!hasSeenFirstDigit)
                        {
                            sum += (int)char.GetNumericValue(c) * 10;
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
