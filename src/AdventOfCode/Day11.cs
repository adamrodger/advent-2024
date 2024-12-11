using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 11
    /// </summary>
    public class Day11
    {
        public int Part1(string[] input)
        {
            long[] stones = input[0].Split(' ').Select(long.Parse).ToArray();

            for (int i = 0; i < 25; i++)
            {
                Debug.WriteLine(stones.Length);
                stones = Blink(stones).ToArray();
            }

            // 101929 low
            return stones.Length;
        }

        public int Part2(string[] input)
        {
            foreach (string line in input)
            {
                throw new NotImplementedException("Part 2 not implemented");
            }

            return 0;
        }

        private static IEnumerable<long> Blink(IEnumerable<long> stones)
        {
            foreach (long stone in stones)
            {
                if (stone == 0)
                {
                    yield return 1;
                    continue;
                }

                int digits = (int)Math.Log10(stone) + 1;

                if (digits % 2 == 0)
                {
                    int power = (int)Math.Pow(10, (int)(digits / 2));
                    (long left, long right) = Math.DivRem(stone, power);
                    yield return left;
                    yield return right;
                }
                else
                {
                    yield return stone * 2024;
                }
            }
        }
    }
}
