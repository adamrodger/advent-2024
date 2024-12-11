using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 11
    /// </summary>
    public class Day11
    {
        public long Part1(string[] input) => Blink(input[0], 25);
        public long Part2(string[] input) => Blink(input[0], 75);

        /// <summary>
        /// Blink the given number of times
        /// </summary>
        /// <param name="line">Starting state line</param>
        /// <param name="rounds">Number of times to blink</param>
        /// <returns>Total number of stones after blinking for the given number of rounds</returns>
        private static long Blink(string line, int rounds)
        {
            long[] stones = line.Split(' ').Select(long.Parse).ToArray();
            Dictionary<long, long> counts = stones.ToDictionary(s => s, _ => 1L);

            for (int i = 0; i < rounds; i++)
            {
                var temp = new Dictionary<long, long>(counts.Count);

                foreach ((long stone, long count) in counts)
                {
                    if (stone == 0)
                    {
                        temp[1] = temp.GetOrDefault(1) + count;
                        continue;
                    }

                    int digits = (int)Math.Log10(stone) + 1;

                    if (digits % 2 == 0)
                    {
                        // ReSharper disable once PossibleLossOfFraction
                        int power = (int)Math.Pow(10, digits / 2);
                        (long left, long right) = Math.DivRem(stone, power);
                        temp[left] = temp.GetOrDefault(left) + count;
                        temp[right] = temp.GetOrDefault(right) + count;
                    }
                    else
                    {
                        temp[stone * 2024] = temp.GetOrDefault(stone * 2024) + count;
                    }
                }

                counts = temp;
            }
            
            return counts.Values.Sum();
        }
    }
}
