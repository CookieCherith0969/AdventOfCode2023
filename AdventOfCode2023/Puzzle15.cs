using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle15
{
    private readonly record struct Lens(string Label, int FocalLength);

    private const int modValue = 256;
    private const int multValue = 17;

    private const char removeChar = '-';
    private const char assignChar = '=';

    private static int[] multiples = new int[modValue];

    private static int Hasher(string toHash)
    {
        int hash = 0;
        foreach(char c in toHash)
        {
            hash = HashStep(hash, c);
        }
        return hash;
    }

    private static int HashStep(int hash, char toHash)
    {
        hash += (int)toHash;
        hash %= modValue;
        hash = multiples[hash];
        return hash;
    }

    private static void PopulateMultiples()
    {
        for(int i = 0; i < modValue; i++)
        {
            multiples[i] = (i * multValue) % modValue;
        }
    }

    private static int BoxFocusingPower(List<Lens> box, int boxIndex)
    {
        int total = 0;
        for(int i = 0; i < box.Count; i++)
        {
            int subtotal = 0;
            Lens currentLens = box[i];

            subtotal += boxIndex + 1;
            subtotal *= i + 1;
            subtotal *= currentLens.FocalLength;
            total += subtotal;
        }
        return total;
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
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle15.txt");
        List<Lens>[] boxes = new List<Lens>[modValue];
        for(int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new List<Lens>();
        } 

        PopulateMultiples();
        string[] instructions = lines[0].Split(',');

        foreach (string instruction in instructions)
        {
            string label = string.Empty;
            int boxID = 0;
            int focalLength = -1;
            int existingIndex = -1;
            for(int i = 0; i < instruction.Length; i++)
            {
                char c = instruction[i];
                
                if(c == removeChar || c == assignChar)
                {
                    label = instruction.Substring(0, i);
                    boxID = Hasher(label);

                    existingIndex = boxes[boxID].FindIndex((l) => l.Label == label);
                    if(existingIndex >= 0)
                    {
                        boxes[boxID].RemoveAt(existingIndex);
                    }
                }

                if (char.IsDigit(c))
                {
                    focalLength = (int)char.GetNumericValue(c);
                }
            }

            if( focalLength > -1)
            {
                if(existingIndex < 0)
                {
                    existingIndex = boxes[boxID].Count;
                }
                boxes[boxID].Insert(existingIndex, new Lens(label, focalLength));
            }
        }

        int sum = 0;
        for(int i = 0; i < boxes.Length; i++)
        {
            sum += BoxFocusingPower(boxes[i], i);
        }

        return sum;
    }

}