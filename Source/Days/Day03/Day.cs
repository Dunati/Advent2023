namespace Day03;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
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

            while (x+length < lines[y].Length && lines[y][x + length].IsNumber())
            {
                int xx = x + length;
                foundSymbol |= IsSymbol(xx, y-1) || IsSymbol(xx, y+1);
                partNum *= 10;
                partNum += lines[y][xx] - '0';
                length++;
            }
            foundSymbol |= IsSymbol(x+length, y) || IsSymbol(x + length, y-1)|| IsSymbol(x + length, y+1);

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