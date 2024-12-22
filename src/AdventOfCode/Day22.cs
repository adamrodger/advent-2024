using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 22
    /// </summary>
    public class Day22
    {
        public long Part1(string[] input)
        {
            return input.Select(long.Parse)
                        .Sum(seed => NumberSequence(seed).Last());
        }

        public long Part2(string[] input)
        {
            var results = new long[1 << 20];
            var seen = new int[1 << 20];

            int[] prices = new int[2001];
            int[] diffs = new int[2001];

            foreach ((int id, long seed) in input.Select(long.Parse).Enumerate(start: 1))
            {
                prices[0] = (int)seed % 10;

                foreach ((int i, long n) in NumberSequence(seed).Enumerate(start: 1))
                {
                    prices[i] = (int)n % 10;
                    diffs[i] = prices[i] - prices[i - 1];
                }

                for (int i = 4; i < diffs.Length; i++)
                {
                    // store the 4 elements of the sequence as a 20bit number, 5 bits per value
                    int sequence = ((diffs[i - 3] + 9) << 15)
                                 | ((diffs[i - 2] + 9) << 10)
                                 | ((diffs[i - 1] + 9) << 5)
                                 | (diffs[i] + 9);

                    if (seen[sequence] != id) // make sure we only take the first instance of the sequence
                    {
                        results[sequence] += prices[i];
                        seen[sequence] = id;
                    }
                }
            }

            return results.Max();
        }

        private static IEnumerable<long> NumberSequence(long seed)
        {
            for (int i = 0; i < 2000; i++)
            {
                seed ^= (seed * 64);
                seed %= 16777216;
                seed ^= (seed / 32);
                seed %= 16777216;
                seed ^= (seed * 2048);
                seed %= 16777216;

                yield return seed;
            }
        }
    }
}
