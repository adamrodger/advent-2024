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
        public int Part1(string[] input)
        {
            long[] stones = input[0].Split(' ').Select(long.Parse).ToArray();

            for (int i = 0; i < 25; i++)
            {
                stones = Blink(stones).ToArray();
            }

            // 101929 low
            return stones.Length;
        }

        public long Part2(string[] input)
        {
            long[] stones = input[0].Split(' ').Select(long.Parse).ToArray();
            Dictionary<long, long> counts = stones.ToDictionary(s => s, _ => 1L);

            for (int i = 0; i < 75; i++)
            {
                var temp = new Dictionary<long, long>();

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
                        int power = (int)Math.Pow(10, (int)(digits / 2));
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


            //List<int> counts = [stones.Length];
            //List<int> hashes = [Hash(stones)];

            //for (int i = 0; i < 35; i++)
            //{
            //    stones = Blink(stones).ToArray();
            //    counts.Add(stones.Length);

            //    hashes.Add(Hash(stones));
            //}

            //foreach ((int First, int Second) in counts.Zip(hashes))
            //{
            //    Debug.WriteLine($"{First,15}{(Second),25}");
            //}

            //bool repeats = hashes.ToHashSet().Count != hashes.Count;
            //Debug.WriteLine($"Repeats? {repeats}");

            //return stones.Length;

            //static int Hash(IEnumerable<long> n)
            //{
            //    var hasher = new HashCode();

            //    foreach (long x in n.Take(100))
            //    {
            //        hasher.Add(x);
            //    }

            //    return hasher.ToHashCode();
            //}
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
