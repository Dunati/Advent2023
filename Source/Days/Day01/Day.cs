using System.Runtime.CompilerServices;
using System.Threading;

namespace Day01;



class Day : BaseDay
{

    static int num(char x)
    {
        return x >= '0' && x <= '9' ? (int)(x - '0') : 0;
    }

    string[] numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    IEnumerable<int> Digits(string str, int part)
    {
        for (int i = 0; i < str.Length; i++)
        {
            int value = num(str[i]);
            if (value != 0)
            {
                yield return value;
            }
            else
            {
                if (part == 2)
                {
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        string num = numbers[j];
                        if ((i + num.Length <= str.Length) && str[i..(i + num.Length)] == num)
                        {
                            yield return j + 1;
                        }
                    }
                }
            }
        }
    }


    int value(string str, int part)
    {
        int[] values = Digits(str, part).ToArray();

        return values[0] * 10 + values.Last();
    }

    public override string Run(int part, string rawData)
    {
        return rawData.Lines().Select(x => value(x, part)).Sum().ToString();
    }
}