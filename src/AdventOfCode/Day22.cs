using System.Collections.Generic;
using System.Linq;

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
            long[] seeds = input.Select(long.Parse).ToArray();

            (int Price, int Diff)[][] prices = seeds.Select(s => PriceSequence(s).ToArray()).ToArray();

            var results = new Dictionary<(int, int, int, int), long>();
            int[] sequence = new int[4];

            for (int i = 0; i < seeds.Length; i++)
            {
                var seen = new HashSet<(int, int, int, int)>();
                (int Price, int Diff)[] diffs = prices[i];

                for (int j = 3; j < diffs.Length; j++)
                {
                    sequence[0] = diffs[j - 3].Diff;
                    sequence[1] = diffs[j - 2].Diff;
                    sequence[2] = diffs[j - 1].Diff;
                    sequence[3] = diffs[j].Diff;

                    var key = (sequence[0], sequence[1], sequence[2], sequence[3]);

                    if (!seen.Contains(key)) // make sure we only take the first instance of the sequence
                    {
                        results[key] = results.GetValueOrDefault(key) + diffs[j].Price;
                        seen.Add(key);
                    }
                }
            }

            //var foo = results[(-2, 1, -1, 3)];

            return results.Values.Max();
        }

        private static IEnumerable<long> NumberSequence(long seed)
        {
            yield return seed;

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

        private static IEnumerable<(int Price, int Diff)> PriceSequence(long seed)
        {
            // why did I not just do this with zip...?

            using IEnumerator<long> cursor = NumberSequence(seed).GetEnumerator();

            cursor.MoveNext();
            int previous = (int)(cursor.Current % 10);

            while (cursor.MoveNext())
            {
                int price = (int)(cursor.Current % 10);
                yield return (price, price - previous);
                previous = price;
            }
        }
    }
}
