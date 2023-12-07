using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Puzzle7
{
    private readonly record struct Hand(string Cards, int Bet, int Value);

    private static int CardToValue(char card)
    {
        switch (card)
        {
            case 'T': return 10;
            case 'J': return 11;
            case 'Q': return 12;
            case 'K': return 13;
            case 'A': return 14;
            default:
                return (int)char.GetNumericValue(card);
        }
    }

    private static int CardToValueJokers(char card)
    {
        switch (card)
        {
            case 'T': return 10;
            case 'J': return 1;
            case 'Q': return 12;
            case 'K': return 13;
            case 'A': return 14;
            default:
                return (int)char.GetNumericValue(card);
        }
    }

    private static int CompareHandsByValue(Hand x, Hand y)
    {
        if (x.Value > y.Value)
        {
            return 1;
        }
        else if (y.Value > x.Value)
        {
            return -1;
        }
        else
        {
            for(int i = 0; i < x.Cards.Length; i++)
            {
                int xValue = CardToValue(x.Cards[i]);
                int yValue = CardToValue(y.Cards[i]);
                if (xValue > yValue)
                {
                    return 1;
                }
                else if(yValue > xValue)
                {
                    return -1;
                }
            }
            return 0;
        }
    }

    private static int CompareHandsByValueJokers(Hand x, Hand y)
    {
        if (x.Value > y.Value)
        {
            return 1;
        }
        else if (y.Value > x.Value)
        {
            return -1;
        }
        else
        {
            for (int i = 0; i < x.Cards.Length; i++)
            {
                int xValue = CardToValueJokers(x.Cards[i]);
                int yValue = CardToValueJokers(y.Cards[i]);
                if (xValue > yValue)
                {
                    return 1;
                }
                else if (yValue > xValue)
                {
                    return -1;
                }
            }
            return 0;
        }
    }

    private static int CardsToHandValue(string cards)
    {
        List<char> uniqueCards = new List<char>();
        foreach(char card in cards)
        {
            if (!uniqueCards.Contains(card))
            {
                uniqueCards.Add(card);
            }
        }
        int[] cardOccurences = new int[uniqueCards.Count];
        for(int i = 0; i < uniqueCards.Count; i++)
        {
            char uniqueCard = uniqueCards[i];

            int occurences = 0;
            foreach(char card in cards)
            {
                if(card == uniqueCard)
                {
                    occurences++;
                }
            }
            cardOccurences[i] = occurences;
        }
        Array.Sort(cardOccurences);

        //five of a kind
        if (cardOccurences[^1] == 5)
        {
            return 6;
        }
        //four of a kind
        if (cardOccurences[^1] == 4)
        {
            return 5;
        }
        if (cardOccurences[^1] == 3)
        {
            //full house
            if (cardOccurences[^2] == 2)
            {
                return 4;
            }
            //three of a kind
            else
            {
                return 3;
            }
        }
        if (cardOccurences[^1] == 2)
        {
            //two pair
            if (cardOccurences[^2] == 2)
            {
                return 2;
            }
            //one pair
            else
            {
                return 1;
            }
        }
        //high card
        return 0;
    }

    private static int CardsToHandValueJokers(string cards)
    {
        List<char> uniqueCards = new List<char>();
        int jokers = 0;
        foreach (char card in cards)
        {
            if (card == 'J')
            {
                jokers++;
            }
            else if (!uniqueCards.Contains(card))
            {
                uniqueCards.Add(card);
            }
        }
        if (jokers == 5)
        {
            return 6;
        }
        int[] cardOccurences = new int[uniqueCards.Count];
        for (int i = 0; i < uniqueCards.Count; i++)
        {
            char uniqueCard = uniqueCards[i];

            int occurences = 0;
            foreach (char card in cards)
            {
                if (card == uniqueCard)
                {
                    occurences++;
                }
            }
            cardOccurences[i] = occurences;
        }
        Array.Sort(cardOccurences);
        cardOccurences[^1] += jokers;

        //five of a kind
        if (cardOccurences[^1] == 5)
        {
            return 6;
        }
        //four of a kind
        if (cardOccurences[^1] == 4)
        {
            return 5;
        }
        if (cardOccurences[^1] == 3)
        {
            //full house
            if (cardOccurences[^2] == 2)
            {
                return 4;
            }
            //three of a kind
            else
            {
                return 3;
            }
        }
        if (cardOccurences[^1] == 2)
        {
            //two pair
            if (cardOccurences[^2] == 2)
            {
                return 2;
            }
            //one pair
            else
            {
                return 1;
            }
        }
        //high card
        return 0;
    }

    public static int CalculateOne()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle7.txt");

        Hand[] hands = new Hand[lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            string[] handSplit = line.Split(' ');
            hands[y] = new Hand(handSplit[0], int.Parse(handSplit[1]), CardsToHandValue(handSplit[0]));
        }

        Array.Sort(hands, CompareHandsByValue);

        int sum = 0;
        for(int i = 0; i < hands.Length; i++)
        {
            sum += hands[i].Bet * (i+1);
        }
        return sum;
    }

    public static int CalculateTwo()
    {
        string[] lines = File.ReadAllLines(@"./Inputs/puzzle7.txt");

        Hand[] hands = new Hand[lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            string[] handSplit = line.Split(' ');
            hands[y] = new Hand(handSplit[0], int.Parse(handSplit[1]), CardsToHandValueJokers(handSplit[0]));
        }

        Array.Sort(hands, CompareHandsByValueJokers);

        int sum = 0;
        for (int i = 0; i < hands.Length; i++)
        {
            sum += hands[i].Bet * (i + 1);
        }
        return sum;
    }

}