using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

public class Puzzle10
{
    private readonly record struct Pipe(Pos Pos, Connections Connections);
    private readonly record struct Connections(bool Up, bool Right, bool Down, bool Left);
    private readonly record struct Pos(int X, int Y);

    private static readonly Dictionary<char, Connections> pipeConnections = new Dictionary<char, Connections>
    {
        {'.', nullConnections },
        {'S', nullConnections },
        {'|', new Connections(true, false, true, false) },
        {'-', new Connections(false, true, false, true) },
        {'L', new Connections(true, true, false, false) },
        {'J', new Connections(true, false, false, true) },
        {'7', new Connections(false, false, true, true) },
        {'F', new Connections(false, true, true, false) },

    };
    private static readonly Pos nullPos = new Pos(-1, -1);
    private static readonly Connections nullConnections = new Connections(false, false, false, false);
    private static readonly Pipe nullPipe = new Pipe(nullPos, nullConnections);

    enum Direction { Up, Right, Down, Left };

    private static bool DirectionConnectsAt(Direction direction, Pos fromPos, string[] pipeMap)
    {
        if(direction == Direction.Up)
        {
            if(fromPos.Y - 1 < 0)
            {
                return false;
            }
            char abovePipe = pipeMap[fromPos.Y - 1][fromPos.X];
            if (abovePipe == '.')
            {
                return false;
            }

            if (pipeConnections[abovePipe].Down)
            {
                return true;
            }

            return false;
        }
        else if(direction == Direction.Right)
        {
            if (fromPos.X + 1 >= pipeMap[fromPos.Y].Length)
            {
                return false;
            }
            char rightPipe = pipeMap[fromPos.Y][fromPos.X + 1];
            if (rightPipe == '.')
            {
                return false;
            }

            if (pipeConnections[rightPipe].Left)
            {
                return true;
            }

            return false;
        }
        else if(direction == Direction.Down) 
        {
            if (fromPos.Y + 1 >= pipeMap.Length)
            {
                return false;
            }
            char belowPipe = pipeMap[fromPos.Y + 1][fromPos.X];
            if (belowPipe == '.')
            {
                return false;
            }

            if (pipeConnections[belowPipe].Up)
            {
                return true;
            }

            return false;
        }
        else
        {
            if (fromPos.X - 1 < 0)
            {
                return false;
            }
            char leftPipe = pipeMap[fromPos.Y][fromPos.X - 1];
            if (leftPipe == '.')
            {
                return false;
            }

            if (pipeConnections[leftPipe].Right)
            {
                return true;
            }

            return false;
        }


    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle10.txt");


        Pos startPos = nullPos;
        bool foundStart = false;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            for(int x = 0; x < line.Length; x++)
            {
                char pipe = line[x];

                if(pipe == 'S')
                {
                    startPos = new Pos(x, y);
                    break;
                }
            }
            if (foundStart)
            {
                break;
            }
        }

        Pipe leftHead = nullPipe;
        Pipe rightHead = nullPipe;

        if (DirectionConnectsAt(Direction.Up, startPos, lines))
        {
            Pos abovePos = new Pos(startPos.X, startPos.Y - 1);
            char abovePipe = lines[abovePos.Y][abovePos.X];
            Pipe newHead = new Pipe(abovePos, pipeConnections[abovePipe]);
            if (leftHead == nullPipe)
            {
                leftHead = newHead;
            }
            else
            {
                rightHead = newHead;
            }
        }
        if (DirectionConnectsAt(Direction.Right, startPos, lines))
        {
            Pos rightPos = new Pos(startPos.X + 1, startPos.Y);
            char rightPipe = lines[rightPos.Y][rightPos.X];
            Pipe newHead = new Pipe(rightPos, pipeConnections[rightPipe]);
            if (leftHead == nullPipe)
            {
                leftHead = newHead;
            }
            else
            {
                rightHead = newHead;
            }
        }
        if (DirectionConnectsAt(Direction.Down, startPos, lines))
        {
            Pos downPos = new Pos(startPos.X, startPos.Y + 1);
            char downPipe = lines[downPos.Y][downPos.X];
            Pipe newHead = new Pipe(downPos, pipeConnections[downPipe]);
            if (leftHead == nullPipe)
            {
                leftHead = newHead;
            }
            else
            {
                rightHead = newHead;
            }
        }
        if (DirectionConnectsAt(Direction.Left, startPos, lines))
        {
            Pos leftPos = new Pos(startPos.X - 1, startPos.Y);
            char leftPipe = lines[leftPos.Y][leftPos.X];
            Pipe newHead = new Pipe(leftPos, pipeConnections[leftPipe]);
            if (leftHead == nullPipe)
            {
                leftHead = newHead;
            }
            else
            {
                rightHead = newHead;
            }
        }
        Pos prevLeft = startPos;
        Pos prevRight = startPos;

        int distance = 1;

        while(leftHead.Pos != rightHead.Pos && leftHead.Pos != prevRight)
        {
            bool leftSet = false;
            if (leftHead.Connections.Up && DirectionConnectsAt(Direction.Up, leftHead.Pos, lines))
            {
                Pos abovePos = new Pos(leftHead.Pos.X, leftHead.Pos.Y - 1);
                if(abovePos != prevLeft)
                {
                    char abovePipe = lines[abovePos.Y][abovePos.X];
                    Pipe newHead = new Pipe(abovePos, pipeConnections[abovePipe]);
                    prevLeft = leftHead.Pos;
                    leftSet = true;
                    leftHead = newHead;
                }
            }
            if (!leftSet && leftHead.Connections.Right && DirectionConnectsAt(Direction.Right, leftHead.Pos, lines))
            {
                Pos rightPos = new Pos(leftHead.Pos.X + 1, leftHead.Pos.Y);
                if (rightPos != prevLeft)
                {
                    char rightPipe = lines[rightPos.Y][rightPos.X];
                    Pipe newHead = new Pipe(rightPos, pipeConnections[rightPipe]);
                    prevLeft = leftHead.Pos;
                    leftSet = true;
                    leftHead = newHead;
                }
            }
            if (!leftSet && leftHead.Connections.Down && DirectionConnectsAt(Direction.Down, leftHead.Pos, lines))
            {
                Pos belowPos = new Pos(leftHead.Pos.X, leftHead.Pos.Y + 1);
                if (belowPos != prevLeft)
                {
                    char belowPipe = lines[belowPos.Y][belowPos.X];
                    Pipe newHead = new Pipe(belowPos, pipeConnections[belowPipe]);
                    prevLeft = leftHead.Pos;
                    leftSet = true;
                    leftHead = newHead;
                }
            }
            if (!leftSet && leftHead.Connections.Left && DirectionConnectsAt(Direction.Left, leftHead.Pos, lines))
            {
                Pos leftPos = new Pos(leftHead.Pos.X - 1, leftHead.Pos.Y);
                if (leftPos != prevLeft)
                {
                    char leftPipe = lines[leftPos.Y][leftPos.X];
                    Pipe newHead = new Pipe(leftPos, pipeConnections[leftPipe]);
                    prevLeft = leftHead.Pos;
                    leftHead = newHead;
                }
            }

            bool rightSet = false;
            if (rightHead.Connections.Up && DirectionConnectsAt(Direction.Up, rightHead.Pos, lines))
            {
                Pos abovePos = new Pos(rightHead.Pos.X, rightHead.Pos.Y - 1);
                if (abovePos != prevRight)
                {
                    char abovePipe = lines[abovePos.Y][abovePos.X];
                    Pipe newHead = new Pipe(abovePos, pipeConnections[abovePipe]);
                    prevRight = rightHead.Pos;
                    rightSet = true;
                    rightHead = newHead;
                }
            }
            if (!rightSet && rightHead.Connections.Right && DirectionConnectsAt(Direction.Right, rightHead.Pos, lines))
            {
                Pos rightPos = new Pos(rightHead.Pos.X + 1, rightHead.Pos.Y);
                if (rightPos != prevRight)
                {
                    char rightPipe = lines[rightPos.Y][rightPos.X];
                    Pipe newHead = new Pipe(rightPos, pipeConnections[rightPipe]);
                    prevRight = rightHead.Pos;
                    rightSet = true;
                    rightHead = newHead;
                }
            }
            if (!rightSet && rightHead.Connections.Down && DirectionConnectsAt(Direction.Down, rightHead.Pos, lines))
            {
                Pos belowPos = new Pos(rightHead.Pos.X, rightHead.Pos.Y + 1);
                if (belowPos != prevRight)
                {
                    char belowPipe = lines[belowPos.Y][belowPos.X];
                    Pipe newHead = new Pipe(belowPos, pipeConnections[belowPipe]);
                    prevRight = rightHead.Pos;
                    rightSet = true;
                    rightHead = newHead;
                }
            }
            if (!rightSet && rightHead.Connections.Left && DirectionConnectsAt(Direction.Left, rightHead.Pos, lines))
            {
                Pos leftPos = new Pos(rightHead.Pos.X - 1, rightHead.Pos.Y);
                if (leftPos != prevRight)
                {
                    char leftPipe = lines[leftPos.Y][leftPos.X];
                    Pipe newHead = new Pipe(leftPos, pipeConnections[leftPipe]);
                    prevRight = rightHead.Pos;
                    rightHead = newHead;
                }
            }
            distance++;
        }

        return distance;
    }

    public static long CalculateTwo()
    {
        return -1;
    }

}