namespace Day02;


class Day : BaseDay
{
    public override string Run(int part, string rawData)
    {
        if (part == 1)
        {

            return Part1(rawData);
        }
        return Part2(rawData);
    }

    private string Part2(string rawData)
    {
        int power = 0;
        foreach (var line in rawData.Lines())
        {
            Dictionary<string, int> data = new Dictionary<string, int>() {
                { "red", 0 },
                {"green", 0 },
                {"blue", 0 }
            };
            foreach (var game in line.Split(':', ';').Skip(1))
            {
                foreach (var cubes in game.Split(','))
                {
                    var counts = cubes.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var number = int.Parse(counts[0]);
                    var color = counts[1];

                    if (data[color] < number)
                    {
                        data[color] = number;
                    }
                }
            }
            power += data["red"] * data["green"] * data["blue"];
        }
        return power.ToString();
    }

    private static string Part1(string rawData)
    {
        Dictionary<string, int> data = new Dictionary<string, int>() {
            { "red", 12 },
            {"green", 13 },
            {"blue", 14 }
        };
        int gamesum = 0;
        int gameNum = 0;
        foreach (var line in rawData.Lines())
        {
            ++gameNum;
            foreach (var game in line.Split(':', ';').Skip(1))
            {
                foreach (var cubes in game.Split(','))
                {
                    var counts = cubes.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var number = int.Parse(counts[0]);
                    var color = counts[1];

                    if (data[color] < number)
                    {
                        goto endgame;
                    }
                }
            }
            gamesum += gameNum;
        endgame:
            continue;

        }
        return gamesum.ToString();
    }
}
