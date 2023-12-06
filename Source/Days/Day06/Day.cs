namespace Day06;


class Day : BaseDay {
    public override string Run(int part, string rawData) {

        var (time, distance, rest) = rawData.Lines().Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToInts().ToArray());
        Debug.Assert(time.Length == distance.Length);

        int ways = 1;
        for(int i = 0; i < time.Length;i++)
        {
            int t = time[i];
            int d = distance[i];
            int wins = 0;
            for(int ms=1;ms<t-1; ms++)
            {
                int end = ms * (t - ms);
                if(end > d)
                {
                    wins++;
                }                
            }
            if(wins > 0)
            {
                ways *= wins;
            }
        }
        return ways.ToString();
    }
}
