using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle8
{
    private readonly record struct Node(string ID, string Left, string Right);
    private readonly record struct Cycle(int Offset, int Length, int EndPoint);
    private readonly record struct VisitedNode(Node Node, bool GoingLeft);

    private static int CompareCyclesByLength(Cycle x, Cycle y)
    {
        if (x.Length > y.Length)
        {
            return -1;
        }
        else if (y.Length > x.Length)
        {
            return 1;
        }
        return 0;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle8.txt");
        string instructions = lines[0];
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];

            string nodeID = line.Substring(0, 3);

            string leftID = line.Substring(7, 3);
            string rightID = line.Substring(12, 3);

            nodes.Add(nodeID, new Node(nodeID, leftID, rightID));
        }

        int instructionIndex = 0;
        int steps = 0;
        Node currentNode = nodes["AAA"];
        while (true)
        {
            steps++;
            bool goLeft = instructions[instructionIndex] == 'L';
            if (goLeft)
            {
                if (currentNode.Left == "ZZZ")
                {
                    break;
                }
                currentNode = nodes[currentNode.Left];
            }
            else
            {
                if (currentNode.Right == "ZZZ")
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
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle8.txt");
        string instructions = lines[0];
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];

            string nodeID = line.Substring(0, 3);

            string leftID = line.Substring(7, 3);
            string rightID = line.Substring(12, 3);

            nodes.Add(nodeID, new Node(nodeID, leftID, rightID));
        }

        List<Node> startNodes = new List<Node>();

        foreach (string ID in nodes.Keys)
        {
            if (ID[^1] == 'A')
            {
                startNodes.Add(nodes[ID]);
            }
        }

        string testInstructions = "";
        int instructionIndex = 0;
        int steps = 0;
        List<Cycle> cycles = new List<Cycle>();
        foreach (Node startNode in startNodes)
        {
            List<VisitedNode> visitedNodes = new List<VisitedNode>();
            instructionIndex = 0;
            steps = 0;

            int endPoint = -1;

            bool goLeft = instructions[instructionIndex] == 'L';
            bool wasLeft = false;

            VisitedNode currentNode = new VisitedNode(startNode, goLeft);
            while (!visitedNodes.Contains(currentNode))
            {
                visitedNodes.Add(currentNode);

                if (currentNode.Node.ID[^1] == 'Z')
                {
                    endPoint = steps;
                }

                steps++;
                //if (cycles.Count == 0) 
                //{
                //    testInstructions = testInstructions + instructions[instructionIndex];
                //}
                
                instructionIndex++;
                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }
                wasLeft = goLeft;
                goLeft = instructions[instructionIndex] == 'L';



                if (wasLeft)
                {
                    currentNode = new VisitedNode(nodes[currentNode.Node.Left], goLeft);
                }
                else
                {
                    currentNode = new VisitedNode(nodes[currentNode.Node.Right], goLeft);
                }
            }
            int offset = visitedNodes.IndexOf(currentNode);
            int length = visitedNodes.Count - offset;

            cycles.Add(new Cycle(offset, length, endPoint));
        }
        //Console.WriteLine(testInstructions);
        cycles.Sort(CompareCyclesByLength);
        Cycle longCycle = cycles[0];

        steps = longCycle.EndPoint;
        int[] cycleSteps = new int[cycles.Count - 1];
        for (int i = 1; i < cycles.Count; i++)
        {
            cycleSteps[i - 1] = cycles[i].EndPoint;
        }

        while (true)
        {
            bool aligned = true;
            for (int i = 1; i < cycles.Count; i++)
            {
                while (cycleSteps[i - 1] < steps)
                {
                    cycleSteps[i - 1] += cycles[i].Length;
                }
                if (cycleSteps[i - 1] != steps)
                {
                    aligned = false;
                    break;
                }
            }
            if (aligned)
            {
                return steps;
            }
            steps += longCycle.Length;
        }
    }

}