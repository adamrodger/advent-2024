using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 2
    /// </summary>
    public class Day2
    {
        public int Part1(string[] input) => input.Select(l => l.Numbers<int>()).Count(IsSafe);

        public int Part2(string[] input) => input.Select(l => l.Numbers<int>()).Count(ReallySafe);

        /// <summary>
        /// Check if a sequence is constantly increasing or decreasing and that
        /// the diffs between each step are no more than 3
        /// </summary>
        /// <param name="numbers">Sequence to check</param>
        /// <returns>Sequence is safe</returns>
        private static bool IsSafe(ICollection<int> numbers)
        {
            IEnumerable<(int First, int Second)> pairs = numbers.Zip(numbers.Skip(1));
            int[] diffs = pairs.Select(pair => pair.First - pair.Second).ToArray();

            int expected = Math.Sign(diffs[0]);
            bool sameSign = diffs.All(d => Math.Sign(d) == expected);
            bool safeDiffs = diffs.Select(Math.Abs).All(d => d is > 0 and <= 3);

            return sameSign && safeDiffs;
        }

        /// <summary>
        /// Check if a sequence is safe outright, or whether removing one element from
        /// the sequence would make it safe
        /// </summary>
        /// <param name="numbers">Sequence to check</param>
        /// <returns>Sequence is safe</returns>
        private static bool ReallySafe(ICollection<int> numbers)
        {
            if (IsSafe(numbers))
            {
                return true;
            }

            // check if removing one of the numbers would make the sequence safe
            for (int i = 0; i < numbers.Count; i++)
            {
                var clone = numbers.ToList();
                clone.RemoveAt(i);

                if (IsSafe(clone))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
