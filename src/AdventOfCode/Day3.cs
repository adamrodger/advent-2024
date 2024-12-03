using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 3
    /// </summary>
    public class Day3
    {
        public int Part1(string[] input)
        {
            var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)", RegexOptions.Compiled | RegexOptions.Singleline);

            var sums = from line in input
                       from match in regex.Matches(line)
                       let numbers = match.Value.Numbers<int>()
                       select numbers[0] * numbers[1];

            return sums.Sum();
        }

        public int Part2(string[] input)
        {
            // only match from the start of the line
            var regex = new Regex(@"^mul\(\d{1,3},\d{1,3}\)", RegexOptions.Compiled | RegexOptions.Singleline);

            int total = 0;
            bool enabled = true;

            foreach (string line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    string span = line[i..];

                    if (!enabled)
                    {
                        if (span.StartsWith("do()"))
                        {
                            i += 3;
                            enabled = true;
                        }

                        continue;
                    }

                    if (span.StartsWith("don't()"))
                    {
                        i += 6;
                        enabled = false;
                        continue;
                    }

                    Match match = regex.Match(span);

                    if (match.Success)
                    {
                        var numbers = match.Value.Numbers<int>();
                        total += numbers[0] * numbers[1];
                    }
                }
            }

            return total;
        }
    }
}
