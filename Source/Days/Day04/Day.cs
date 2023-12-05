namespace Day04;


class Day : BaseDay
{
    public override string Run(int part, string rawData)
    {
        if (part == 1)
        {
            return Part1(rawData);
        }

        var lines = rawData.Lines().ToArray();
        int[] cards = new int[lines.Length];
        Array.Fill(cards, 1);

        for(int i = 0; i < cards.Length; i++) 
        {
            string line = lines[i];
            string[] parts = line.Split(':', '|');
            var winning = new Dictionary<int, int>(parts[1].ToInts(10, " ").Select(x => new KeyValuePair<int, int>(x, 0)));

            foreach (int number in parts[2].ToInts(10, " "))
            {
                if (winning.TryGetValue(number, out int value))
                {
                    winning[number] = value + 1;
                }
            }
            int matches = winning.Values.Where(x => x > 0).Count();
            for(int m = 1; m<=matches; m++) {
                if(m+i < cards.Length)
                {
                    cards[m+i] +=cards[i];
                }
            }
        }
        return cards.Sum().ToString();
    }
    private static string Part1(string rawData)
    {
        int total_points = 0;
        foreach (var line in rawData.Lines())
        {
            string[] parts = line.Split(':', '|');
            var winning = new Dictionary<int, int>(parts[1].ToInts(10, " ").Select(x => new KeyValuePair<int, int>(x, 0)));

            foreach (int number in parts[2].ToInts(10, " "))
            {
                if (winning.TryGetValue(number, out int value))
                {
                    winning[number] = value + 1;
                }
            }
            int matches = winning.Values.Where(x => x > 0).Count();
            if (matches > 0)
            {
                total_points += 1 << (matches - 1);
            }
        }
        return total_points.ToString();
    }
}
// p1
// 882 too low
// 215853 too high
// 20667

// p2
// 5833065