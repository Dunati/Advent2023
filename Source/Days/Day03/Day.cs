namespace Day03;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {
        if (part == 1) { return Part1(rawData); }

        var lines = rawData.Lines().ToArray();

        var IsNumber = (int x, int y) =>
        {
            if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length)
            {
                return 0;
            }

            char c = lines[y][x];
            if (!c.IsNumber())
            {
                return 0;
            }

            int xstart = x - 1;
            while (xstart >= 0 && lines[y][xstart].IsNumber())
            {
                xstart -= 1;
            }
            int xend = x + 1;
            while (xend < lines[y].Length && lines[y][xend].IsNumber())
            {
                xend += 1;
            }
            int number = 0;
            for (int i = xstart+1; i < xend; i++)
            {
                number *= 10;
                number += lines[y][i] - '0';
            }
            return number;
        };
        long partsum = 0;


        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '*')
                {
        List<int> ratios = new List<int>();
                    int gear = IsNumber(x, y - 1);
                    if (gear != 0)
                    {
                        ratios.Add(gear);
                    }
                    else
                    {
                        gear = IsNumber(x - 1, y - 1);

                        if (gear != 0)
                        {
                            ratios.Add(gear);
                        }
                        gear = IsNumber(x + 1, y - 1);
                        if (gear != 0)
                        {
                            ratios.Add(gear);
                        }
                    }
                    gear = IsNumber(x, y + 1);
                    if (gear != 0)
                    {
                        ratios.Add(gear);
                    }
                    else
                    {
                        gear = IsNumber(x - 1, y + 1);

                        if (gear != 0)
                        {
                            ratios.Add(gear);
                        }
                        gear = IsNumber(x + 1, y + 1);
                        if (gear != 0)
                        {
                            ratios.Add(gear);
                        }
                    }

                    gear = IsNumber(x - 1, y);
                    if(gear != 0)
                    {
                        ratios.Add(gear);
                    }

                    gear = IsNumber(x + 1, y);
                    if (gear != 0)
                    {
                        ratios.Add(gear);
                    }

                    if(ratios.Count == 2)
                    {
                        partsum += ratios[0] * ratios[1];
                    }
                    else
                    {
                        Debug.Assert(ratios.Count < 2);
                    }
                }
            }
        }

        return partsum.ToString();
    }

    private static string Part1(string rawData)
    {
        var lines = rawData.Lines().ToArray();

        var IsSymbol = (int x, int y) =>
        {
            if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length)
            {
                return false;
            }

            char c = lines[y][x];
            return c != '.' && !c.IsNumber();
        };

        var isPart = (int x, int y) =>
        {
            int partNum = lines[y][x] - '0';
            int length = 1;
            bool foundSymbol =
            IsSymbol(x - 1, y - 1) ||
            IsSymbol(x - 1, y) ||
            IsSymbol(x - 1, y + 1) ||
            IsSymbol(x, y - 1) ||
            IsSymbol(x, y + 1);

            while (x + length < lines[y].Length && lines[y][x + length].IsNumber())
            {
                int xx = x + length;
                foundSymbol |= IsSymbol(xx, y - 1) || IsSymbol(xx, y + 1);
                partNum *= 10;
                partNum += lines[y][xx] - '0';
                length++;
            }
            foundSymbol |= IsSymbol(x + length, y) || IsSymbol(x + length, y - 1) || IsSymbol(x + length, y + 1);

            if (foundSymbol)
            {
                return partNum;
            }
            return 0;
        };
        int partsum = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char c = lines[y][x];

                if (c != '.' && c.IsNumber())
                {
                    partsum += isPart(x, y);
                    x++;
                    while (x < lines[y].Length && lines[y][x].IsNumber())
                    {
                        x++;
                    }
                }
            }
        }
        return partsum.ToString();
    }
}
// 462696 too low
// 560670 correct
// p2 91622824 correct