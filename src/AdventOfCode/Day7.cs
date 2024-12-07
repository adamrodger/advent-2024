using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 7
    /// </summary>
    public class Day7
    {
        public long Part1(string[] input)
        {
            long total = 0;

            foreach (string line in input)
            {
                long[] n = line.Numbers<long>();

                foreach (long sum in PossibleSums(n[2..], n[1]))
                {
                    if (sum == n[0])
                    {
                        total += n[0];
                        break;
                    }
                }
            }

            // 4_613_169_492 low
            return total;
        }

        public long Part2(string[] input)
        {
            long total = 0;

            foreach (string line in input)
            {
                long[] n = line.Numbers<long>();

                foreach (long sum in PossibleSums2(n[2..], n[1]))
                {
                    if (sum == n[0])
                    {
                        total += n[0];
                        break;
                    }
                }
            }

            // 482739998262825 low
            return total;
        }

        private static IEnumerable<long> PossibleSums(long[] slice, long total)
        {
            if (slice.Length == 1)
            {
                yield return total + slice[0];
                yield return total * slice[0];
                yield break;
            }

            IEnumerable<long> adds = PossibleSums(slice[1..], total + slice[0]);
            IEnumerable<long> multiplies = PossibleSums(slice[1..], total * slice[0]);

            foreach (long next in adds.Concat(multiplies))
            {
                yield return next;
            }
        }

        private static IEnumerable<long> PossibleSums2(long[] slice, long total)
        {
            long magnitude = (long)Math.Pow(10, Math.Ceiling(Math.Log10(slice[0])));

            if (slice.Length == 1)
            {
                yield return total + slice[0];
                yield return total * slice[0];
                yield return (total * magnitude) + slice[0];
                yield break;
            }

            IEnumerable<long> adds = PossibleSums2(slice[1..], total + slice[0]);
            IEnumerable<long> multiplies = PossibleSums2(slice[1..], total * slice[0]);
            IEnumerable<long> concats = PossibleSums2(slice[1..], (total * magnitude) + slice[0]);

            foreach (long next in adds.Concat(multiplies).Concat(concats))
            {
                yield return next;
            }
        }
    }
}
