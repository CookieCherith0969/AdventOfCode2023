using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle4
{
    const char idChar = ':';
    const char winningChar = '|';


    public static int CalculateOne()
    {
        int sum = 0;
        string[] cards = File.ReadAllLines(@"./Inputs/puzzle4.txt");

        int currentNum = 0;
        HashSet<int> winningNums = new HashSet<int>();
        HashSet<int> ownedNums = new HashSet<int>();
        bool seenID = false;
        bool seenWinning = false;
        int currentID = 0;

        for (int y = 0; y < cards.Length; y++)
        {
            if (winningNums.Count > 0)
            {
                ownedNums.Add(currentNum);
                int wins = winningNums.Intersect<int>(ownedNums).Count();
                if(wins > 0)
                {
                    sum += (int)Math.Pow(2, wins - 1);
                }
            }

            currentNum = 0;
            winningNums.Clear();
            ownedNums.Clear();
            seenID = false;
            seenWinning = false;
            currentID = 0;

            string card = cards[y];
            if (card.Length == 0) break;

            for (int x = 0; x < card.Length; x++)
            {
                char c = card[x];
                if (!seenID)
                {
                    if (char.IsDigit(c))
                    {
                        currentNum *= 10;
                        currentNum += (int)char.GetNumericValue(c);
                    }
                    else if(c == idChar)
                    {
                        currentID = currentNum;
                        currentNum = 0;
                        seenID = true;
                    }
                }
                else if (!seenWinning)
                {
                    if (char.IsDigit(c))
                    {
                        currentNum *= 10;
                        currentNum += (int)char.GetNumericValue(c);
                    }
                    else if(currentNum > 0)
                    {
                        winningNums.Add(currentNum);
                        currentNum = 0;
                    }
                    if(c == winningChar)
                    {
                        seenWinning = true;
                    }
                }
                else
                {
                    if (char.IsDigit(c))
                    {
                        currentNum *= 10;
                        currentNum += (int)char.GetNumericValue(c);
                    }
                    else if (currentNum > 0)
                    {
                        ownedNums.Add(currentNum);
                        currentNum = 0;
                    }
                }
            }
        }


        return sum;
    }

    public static int CalculateTwo()
    {
        return -1;
    }

}