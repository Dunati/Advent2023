namespace Day08;


class Day : BaseDay
{
    public override string Run(int part, string rawData)
    {
        if (part == 1)
        {
            return Part1(rawData);
        }

        var lines = rawData.Lines();

        var path = lines.First();

        List<string> start = new List<string>();

        var nodes = lines.Skip(1).Select(x =>
        {
            var rl = x.Split(" =(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (rl[0][2] == 'A')
            {
                start.Push(rl[0]);
            }
            return new { left = rl[1], right = rl[2], key = rl[0] };
        }
        ).ToDictionary(x => x.key);

        var allPaths = new List<List<int>>();
        var offsets = new List<List<int>>();
        foreach (var p in start)
        {
            List<int> steps = new List<int>();
            int index = 0;
            var position = p;
            while (steps.Count < 4)
            {
                var dir = path[(index++) % path.Length];

                if (dir == 'L')
                {
                    position = nodes[position].left;
                }
                else
                {
                    position = nodes[position].right;
                }
                if (position[2] == 'Z')
                {
                    steps.Push(index);
                }
            }
            allPaths.Add(steps);
            var offset = new List<int>();
            offset.Add(steps[0]);
            for(int i = 1; i < steps.Count; i++)
            {
                offset.Add(steps[i] - steps[i-1]);
            }
            offsets.Add(offset);
        }

        var m = offsets.Select(x => (decimal)x.Last()).Aggregate((x, y) => x * y);

        return m.ToString();
    }
    // 45528895583679359994704213 too high
    private static string Part1(string rawData)
    {
        var lines = rawData.Lines();

        var path = lines.First();

        var nodes = lines.Skip(1).Select(x =>
        {
            var rl = x.Split(" =(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return new { left = rl[1], right = rl[2], key = rl[0] };
        }
        ).ToDictionary(x => x.key);


        int index = 0;
        var position = "AAA";
        while (position != "ZZZ")
        {
            var dir = path[(index++) % path.Length];

            if (dir == 'L')
            {
                position = nodes[position].left;
            }
            else
            {
                position = nodes[position].right;
            }
        }

        return index.ToString();
    }
}
