using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle15
{
    private const int modValue = 256;
    private const int multValue = 17;

    private static int[] multiples = new int[modValue];

    private static int Hasher(string toHash)
    {
        int hash = 0;
        foreach(char c in toHash)
        {
            hash += (int)c;
            hash %= modValue;
            hash = multiples[hash];
        }
        return hash;
    }

    private static void PopulateMultiples()
    {
        for(int i = 0; i < modValue; i++)
        {
            multiples[i] = (i * multValue) % modValue;
        }
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle15.txt");

        PopulateMultiples();
        string[] instructions = lines[0].Split(',');
        int sum = 0;
        foreach(string instruction in instructions) 
        {
            sum += Hasher(instruction);
        }
        return sum;
    }

    public static long CalculateTwo()
    {
        return -1;
    }

}