// See https://aka.ms/new-console-template for more information
using System.Diagnostics;




Console.WriteLine("Day 1:");
Stopwatch sw = Stopwatch.StartNew();
Console.Write(Puzzle1.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle1.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 2:");
sw.Restart();
Console.Write(Puzzle2.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle2.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 3:");
sw.Restart();
Console.Write(Puzzle3.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle3.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 4:");
sw.Restart();
Console.Write(Puzzle4.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle4.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 5:");
sw.Restart();
Console.Write(Puzzle5.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle5.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 6:");
sw.Restart();
Console.Write(Puzzle6.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle6.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 7:");
sw.Restart();
Console.Write(Puzzle7.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle7.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 8:");
sw.Restart();
Console.Write(Puzzle8.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write("Disabled due to long runtime");
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 9:");
sw.Restart();
Console.Write(Puzzle9.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle9.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 10:");
sw.Restart();
Console.Write(Puzzle10.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle10.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");

Console.WriteLine("\nDay 11:");
sw.Restart();
Console.Write(Puzzle11.CalculateOne().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");
sw.Restart();
Console.Write(Puzzle11.CalculateTwo().ToString());
Console.WriteLine($" - ({sw.ElapsedMilliseconds} ms)");


Console.WriteLine();
const int TEST_NUM = 200;
const int PART_NUM = 22;

long[][] parts = new long[PART_NUM][];
for(int i = 0; i < PART_NUM; i++)
{
    parts[i] = new long[TEST_NUM];
}

for(int i = 0; i < TEST_NUM; i++)
{
    sw.Restart();
    Puzzle1.CalculateOne();
    parts[0][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle1.CalculateTwo();
    parts[1][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle2.CalculateOne();
    parts[2][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle2.CalculateTwo();
    parts[3][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle3.CalculateOne();
    parts[4][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle3.CalculateTwo();
    parts[5][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle4.CalculateOne();
    parts[6][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle4.CalculateTwo();
    parts[7][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle5.CalculateOne();
    parts[8][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle5.CalculateTwo();
    parts[9][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle6.CalculateOne();
    parts[10][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle6.CalculateTwo();
    parts[11][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle7.CalculateOne();
    parts[12][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle7.CalculateTwo();
    parts[13][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle8.CalculateOne();
    parts[14][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    //Puzzle8.CalculateTwo();
    //parts[15][i] = sw.ElapsedMilliseconds;
    parts[15][i] = -1;

    sw.Restart();
    Puzzle9.CalculateOne();
    parts[16][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle9.CalculateTwo();
    parts[17][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle10.CalculateOne();
    parts[18][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle10.CalculateTwo();
    parts[19][i] = sw.ElapsedMilliseconds;

    sw.Restart();
    Puzzle11.CalculateOne();
    parts[20][i] = sw.ElapsedMilliseconds;
    sw.Restart();
    Puzzle11.CalculateTwo();
    parts[21][i] = sw.ElapsedMilliseconds;
}

for(int i = 0; i < PART_NUM; i++)
{
    Console.WriteLine($"Day {(i+2)/2} Part {(i%2)+1}: {parts[i].Average()} ms");
}

Console.ReadLine();