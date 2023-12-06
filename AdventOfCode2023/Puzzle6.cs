using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle6
{
    private readonly record struct Race(int Time, int RecordDistance);
    private readonly record struct LongRace(long Time, long RecordDistance);
    //private static int CompareValuesByStart(ValueRange x, ValueRange y)
    //{
    //    if (x.Start > y.Start)
    //    {
    //        return 1;
    //    }
    //    else if(y.Start > x.Start)
    //    {
    //        return -1;
    //    }
    //    return 0;
    //}
   
    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle6.txt");
        int currentTime = 0;
        int currentRecord = 0;
        List<Race> races = new List<Race>();
        string timeLine = lines[0];
        string recordLine = lines[1];

        for(int i = 0; i < timeLine.Length; i++)
        {
            char timeChar = timeLine[i];
            char recordChar = recordLine[i];
            if (char.IsDigit(timeChar))
            {
                currentTime *= 10;
                currentTime += (int)char.GetNumericValue(timeChar);
            }
            if (char.IsDigit(recordChar))
            {
                currentRecord *= 10;
                currentRecord += (int)char.GetNumericValue(recordChar);
            }
            else if(currentTime > 0)
            {
                races.Add(new Race(currentTime, currentRecord));
                currentTime = 0;
                currentRecord = 0;
            }
        }
        if(currentTime > 0)
        {
            races.Add(new Race(currentTime, currentRecord));
        }

        int[] winPossibilities = new int[races.Count];

        for (int i = 0; i < races.Count; i++) 
        {
            Race race = races[i];

            int lowWin = -1;
            for(int releaseTime = 0; releaseTime < race.Time; releaseTime++)
            {
                int remainingTime = race.Time - releaseTime;
                if(releaseTime * remainingTime > race.RecordDistance)
                {
                    lowWin = releaseTime;
                    break;
                }
            }
            if(lowWin == -1)
            {
                winPossibilities[i] = 0;
                continue;
            }
            int highWin = -1;
            for (int releaseTime = race.Time-1; releaseTime >= 0; releaseTime--)
            {
                int remainingTime = race.Time - releaseTime;
                if (releaseTime * remainingTime > race.RecordDistance)
                {
                    highWin = releaseTime;
                    break;
                }
            }

            winPossibilities[i] = highWin - lowWin + 1;
        }

        int product = 1;
        foreach(int possibilities in winPossibilities)
        {
            product *= possibilities;
        }
        return product;
    }

    public static long CalculateTwo()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle6.txt");
        long currentTime = 0;
        long currentRecord = 0;
        string timeLine = lines[0];
        string recordLine = lines[1];

        for (int i = 0; i < timeLine.Length; i++)
        {
            char timeChar = timeLine[i];
            char recordChar = recordLine[i];
            if (char.IsDigit(timeChar))
            {
                currentTime *= 10;
                currentTime += (long)char.GetNumericValue(timeChar);
            }
            if (char.IsDigit(recordChar))
            {
                currentRecord *= 10;
                currentRecord += (long)char.GetNumericValue(recordChar);
            }
        }
        LongRace race = new LongRace(currentTime, currentRecord);

        long lowWin = -1;

        long halfTime = race.Time / 2;
        long pointer = halfTime / 2;
        long prevJump = halfTime;
        while(lowWin == -1) 
        {
            long remainingTime = race.Time - pointer;
            long distance = remainingTime * pointer;
            if (distance > race.RecordDistance)
            {
                long priorDistance = (remainingTime + 1) * (pointer - 1);
                if(priorDistance <= race.RecordDistance)
                {
                    lowWin = pointer;
                    break;
                }
                pointer -= prevJump / 2;
                prevJump /= 2;
                continue;
            }
            else
            {
                pointer += prevJump / 2;
                prevJump /= 2;
                continue;
            }
        }

        long winPossibilities = halfTime - lowWin;
        winPossibilities *= 2;
        if (race.Time % 2 == 0)
        {
            winPossibilities += 1;
        }
        else
        {
            winPossibilities += 2;
        }
        return winPossibilities;
    }

}
