namespace Day08;


class Day : BaseDay {
    public override string Run(int part, string rawData) {
        var lines = rawData.Lines();

        var path = lines.First();

        var nodes = lines.Skip(1).Select(x => {
            var rl = x.Split(" =(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return  new { left = rl[1], right = rl[2], key = rl[0] };
        }
        ).ToDictionary(x=>x.key);


        int index = 0;
        var position = "AAA";
        while(position != "ZZZ")
        {
            var dir = path[(index++)%path.Length];

            if(dir == 'L')
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
