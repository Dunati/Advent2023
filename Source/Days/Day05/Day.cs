namespace Day05;


class Day : BaseDay {

    struct Mapping
    {
        public Int64 start;
        public Int64 end;
        public Int64 offset;
    }
    public override string Run(int part, string rawData) {
        var parts = rawData.Split("\n\n");

        var seeds = parts[0].Split(' ').Skip(1).ToLongs().ToArray();

        var mappings = new List<List<Mapping>>();

        for(int i = 1; i < parts.Length; i++)
        {
            var mapping = new List<Mapping>();
            foreach(var line in parts[i].Split('\n', StringSplitOptions.RemoveEmptyEntries).Skip(1))
            {
                var (dest, source, count,rest) = line.ToLongs(10, " ");
                mapping.Add(new Mapping() { start = source, end=source+count, offset=dest-source });
            }
            mapping.Sort((x, y) =>
            {
                var diff = x.start - y.start;
                if(diff > 0)
                {
                    return 1;
                }else if(diff < 0)
                {
                    return -1;
                }
                return 0; 
            });
            mappings.Add(mapping);
        }


        Int64[] locations = new long[seeds.Length];

        for(int i = 0;i<seeds.Length; i++)
        {
            long id = seeds[i];
            for(int m=0; m<mappings.Count; m++)
            {
                foreach(var map in mappings[m])
                {
                    if(id>=map.start && id<map.end)
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