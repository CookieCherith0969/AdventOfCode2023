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

        for (int y = 0; y < cards.Length; y++)
        {
            currentNum = 0;
            winningNums.Clear();
            ownedNums.Clear();
            seenID = false;
            seenWinning = false;

            string card = cards[y];
            if (card.Length == 0) break;

            for (int x = 0; x < card.Length; x++)
            {
                char c = card[x];
                if (!seenID)
                {
                    if(c == idChar)
                    {
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
            ownedNums.Add(currentNum);
            int wins = winningNums.Intersect<int>(ownedNums).Count();
            if (wins > 0)
            {
                sum += (int)Math.Pow(2, wins - 1);
            }
        }


        return sum;
    }

    public static int CalculateTwo()
    {
        int sum = 0;
        string[] cards = File.ReadAllLines(@"./Inputs/puzzle4.txt");

        int currentNum = 0;
        HashSet<int> winningNums = new HashSet<int>();
        HashSet<int> ownedNums = new HashSet<int>();
        int[] cardNums = new int[cards.Length];
        for(int i = 0; i < cardNums.Length; i++)
        {
            cardNums[i] = 1;
        }
        bool seenID = false;
        bool seenWinning = false;
        int currentID = 0;

        for (int y = 0; y < cards.Length; y++)
        {
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
                    else if (c == idChar)
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
                    else if (currentNum > 0)
                    {
                        winningNums.Add(currentNum);
                        currentNum = 0;
                    }
                    if (c == winningChar)
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
            ownedNums.Add(currentNum);
            int wins = winningNums.Intersect<int>(ownedNums).Count();
            int currentCardNum = cardNums[currentID - 1];
            for (int i = 0; i < wins; i++)
            {
                cardNums[currentID + i] += currentCardNum;
            }
            sum += currentCardNum;
        }


        return sum;
    }

}