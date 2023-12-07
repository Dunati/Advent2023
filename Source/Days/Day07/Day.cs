namespace Day07;


class Day : BaseDay
{
    struct Hand
    {
        public string cards;
        public int bid;
        public Hands type;

        public override string ToString()
        {
            return $"{cards} {type} {bid}";
        }
    }
    enum Hands
    {
        HighCard,
        OnePair,
        TwoPair,
        Three,
        FullHouse,
        Four,
        Five
    };
    static Dictionary<char, int> order = new Dictionary<char, int>()
    {
        { 'A', 14 },
        { 'K', 13 },
        {'Q', 12 },
        {'J', 11 },
        {'T', 10 },
        {'9', 9 },
        {'8', 8 },
        {'7', 7 },
        {'6', 6 },
        {'5', 5 },
        {'4', 4 },
        {'3', 3 },
        {'2', 2 },
    };
    static int OrderHand(Hand a, Hand b)
    {
        if (a.type > b.type)
        {
            return 1;
        }
        if (a.type < b.type)
        {
            return -1;
        }
        for (int i = 0; i < 5; i++)
        {
            if (a.cards[i] != b.cards[i])
            {
                return order[a.cards[i]].CompareTo(order[b.cards[i]]);
            }
        }
        return 0;
    }
    public override string Run(int part, string rawData)
    {
        if (part == 1)
            return Part1(rawData);

        order['J'] = 1;

        List<Hand> hands = new List<Hand>();
        foreach (var line in rawData.Lines())
        {

            DefaultDictionary<char, int> hand = new DefaultDictionary<char, int>();
            for (int i = 0; i < 5; i++)
            {
                hand[line[i]]++;
            }
            Hands type = 0;
            int jokers = hand['J'];
            switch (hand.Count)
            {
            case 1: type = Hands.Five; break;
            case 2:
                if (jokers > 0)
                {
                    type = Hands.Five;
                }
                else if (hand.First().Value == 1 || hand.First().Value == 4)
                {
                    type = Hands.Four;
                }
                else
                {
                    type = Hands.FullHouse;
                }

                break;
            case 3:
                if (hand.Values.Max() == 3)
                {
                    if(jokers == 3)
                    {
                        type = Hands.Four;
                    }
                    else if (jokers == 2)
                    {
                        type = Hands.Five;
                    }
                    else if (jokers == 1)
                    {
                        type = Hands.Four;
                    }
                    else
                    {
                        Debug.Assert(jokers == 0);
                        type = Hands.Three;

                    }
                }
                else
                {
                    if (jokers == 2)
                    {
                        type = Hands.Four;
                    }
                    else if (jokers == 1)
                    {
                        type = Hands.FullHouse;
                    }
                    else
                    {
                        Debug.Assert(jokers == 0);
                        type = Hands.TwoPair;
                    }
                }

                break;
            case 4:
                if (jokers > 0)
                {
                    type = Hands.Three;
                }
                else
                {
                    type = Hands.OnePair;
                }
                break;
            default:
                Debug.Assert(hand.Count == 5);
                if (jokers > 0)
                {
                    type = Hands.OnePair;
                }
                else
                {

                    type = Hands.HighCard;
                }
                break;
            }

            hands.Add(new Hand() { cards = line[0..5], type = type, bid = int.Parse(line[6..]) });
        }

        hands.Sort(OrderHand);

        Trace.WriteLine("");
        Trace.WriteLine("");
        decimal score = 0;
        decimal rank = 1;
        foreach (Hand hand in hands)
        {
            Trace.WriteLine(hand);
            score += rank * hand.bid;
            rank++;
        }

        // 251005995 too low
        // 251195607
        return score.ToString();
    }

    private static string Part1(string rawData)
    {
        List<Hand> hands = new List<Hand>();
        foreach (var line in rawData.Lines())
        {

            DefaultDictionary<char, int> hand = new DefaultDictionary<char, int>();
            for (int i = 0; i < 5; i++)
            {
                hand[line[i]]++;
            }
            Hands type = 0;
            switch (hand.Count)
            {
            case 1: type = Hands.Five; break;
            case 2:
                if (hand.First().Value == 1 || hand.First().Value == 4)
                {
                    type = Hands.Four;
                }
                else
                {
                    type = Hands.FullHouse;
                }

                break;
            case 3:
                if (hand.Values.Max() == 3)
                {
                    type = Hands.Three;
                }
                else
                {
                    type = Hands.TwoPair;
                }
                break;
            case 4: type = Hands.OnePair; break;
            default:
                Debug.Assert(hand.Count == 5);
                type = Hands.HighCard; break;
            }

            hands.Add(new Hand() { cards = line[0..5], type = type, bid = int.Parse(line[6..]) });
        }

        hands.Sort(OrderHand);

        int score = 0;
        int rank = 1;
        foreach (Hand hand in hands)
        {
            score += rank * hand.bid;
            rank++;
        }

        return score.ToString();
    }
}