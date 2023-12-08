using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle8
{
    private readonly record struct Node(int Left, int Right);
    private static int NodeToID(string node)
    {
        int num = 0;
        for(int i = 0; i < node.Length; i++)
        {
            num *= 26;
            num += node[i] - 'A';
        }
        return num;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle8.txt");
        string instructions = lines[0];
        Node[] nodes = new Node[17576];

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];

            int nodeID = NodeToID(line.Substring(0, 3));

            int leftID = NodeToID(line.Substring(7, 3));
            int rightID = NodeToID(line.Substring(12, 3));

            nodes[nodeID] = new Node(leftID, rightID);
        }

        int instructionIndex = 0;
        int steps = 0;
        Node currentNode = nodes[0];
        while (true)
        {
            steps++;
            bool goLeft = instructions[instructionIndex] == 'L';
            if (goLeft)
            {
                if(currentNode.Left == 17575)
                {
                    break;
                }
                currentNode = nodes[currentNode.Left];
            }
            else
            {
                if (currentNode.Right == 17575)
                {
                    break;
                }
                currentNode = nodes[currentNode.Right];
            }
            instructionIndex++;
            if (instructionIndex >= instructions.Length)
            {
                instructionIndex = 0;
            }
        }
        return steps;
    }

    public static long CalculateTwo()
    {
        return -1;
    }

}