namespace Day05;


class Day : BaseDay
{
    class Range
    {
        public long start;
        public long count;
        public long offset;
        public long end => start + count;

        public override string ToString()
        {
            return $"[{start}-{end}]";
        }
    }

    string Draw(List<Range> ranges)
    {
        
        char[] r = new char[150];
      
        Array.Fill(r, ' ');
        for(int i = 0; i < ranges.Count; i++)
        {
            for(long j = ranges[i].start; j < ranges[i].end; j++)
            {
                r[j] = (char)(i+'A');
            }
        }
        return new string(r);
    }

    public override string Run(int part, string rawData)
    {
        var parts = rawData.Split("\n\n");

        var seeds = parts[0].Split(' ').Skip(1).ToLongs().ToArray();

        var mappings = new List<Queue<Range>>();

        for (int i = 1; i < parts.Length; i++)
        {
            var mapping = new List<Range>();
            foreach (var line in parts[i].Split('\n', StringSplitOptions.RemoveEmptyEntries).Skip(1))
            {
                var (dest, source, count, rest) = line.ToLongs(10, " ");
                mapping.Add(new Range() { start = source, count = count, offset = dest - source });
            }

            mapping.Sort((x, y) => x.start.CompareTo(y.start));

            mappings.Add(new Queue<Range>(mapping));
        }

        if (part == 1)
        {
            return Part1(seeds, mappings);
        }
        List<Range> s = new List<Range>();

        for (int i = 0; i < seeds.Length - 1; i += 2)
        {
            s.Add(new Range() { start = seeds[i], count = seeds[i + 1] });
        }

        s.Sort((x, y) => x.start.CompareTo(y.start));
        Queue<Range> ranges = new Queue<Range>(s);

        foreach (var maps in mappings)
        {
         //  Trace.WriteLine(Draw(ranges.ToList()));
         //  Trace.WriteLine(Draw(maps.ToList()));
         //  Trace.WriteLine("");

            List<Range> next = new List<Range>();
            while (ranges.Any() && maps.Any())
            {
                var range = ranges.Peek();
                var map = maps.Peek();
                if (range.end <= map.start)
                {
                    next.Add(ranges.Dequeue());
                }
                else if (range.start >= map.end)
                {
                    maps.Dequeue();
                }
                else if (range.start < map.start)
                {
                    Debug.Assert(range.end > map.start);

                    var pre = new Range() { start = range.start , count = map.start - range.start };
                    range.count -= pre.count;
                    range.start += pre.count;
                    next.Add(pre);

                }
                else
                {
                    Debug.Assert(range.start < map.end);
                    if (range.end <= map.end)
                    {
                        range.start += map.offset;
                        next.Add(ranges.Dequeue());
                    }
                    else
                    {
                        var pre = new Range() { start = range.start + map.offset, count = map.end - range.start };
                        range.count -= pre.count;
                        range.start += pre.count;
                        next.Add(pre);
                    }
                }
            }
            next.AddRange(ranges);
            ranges = new Queue<Range>(next.OrderBy(x=>x.start));

        }
        return ranges.Peek().start.ToString();
    }

    // 905069255 too high
    // 176908934 wrong
    // 6082852
    private static string Part1(long[] seeds, List<Queue<Range>> mappings)
    {
        Int64[] locations = new long[seeds.Length];

        for (int i = 0; i < seeds.Length; i++)
        {
            long id = seeds[i];
            for (int m = 0; m < mappings.Count; m++)
            {
                foreach (var map in mappings[m])
                {
                    if (id >= map.start && id < map.start + map.count)
                    {
                        id += map.offset;
                        break;
                    }
                }
            }
            locations[i] = id;
        }


        return locations.Min().ToString();
    }
}
// p1 3374647