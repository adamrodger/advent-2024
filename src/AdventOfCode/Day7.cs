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
        public long Part1(string[] input) => TotalPossible(input, Part.One);
        public long Part2(string[] input) => TotalPossible(input, Part.Two);

        /// <summary>
        /// Sum the target for all sums that are possible in the input
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="part">Part, used to determine the allowed operation</param>
        /// <returns>Sum of all possible targets</returns>
        private static long TotalPossible(string[] input, Part part)
        {
            return input.Select(line => line.Numbers<long>())
                        .Where(n => PossibleSums(part, n[2..], n[1]).Any(sum => sum == n[0]))
                        .Sum(n => n[0]);
        }

        /// <summary>
        /// Check the number of possible totals from the given starting position
        /// </summary>
        /// <param name="part">Part, to determine the allowed operations</param>
        /// <param name="slice">Remaining numbers</param>
        /// <param name="total">Running total</param>
        /// <returns>All possible sums, using the allowed operations for the problem part</returns>
        private static IEnumerable<long> PossibleSums(Part part, long[] slice, long total)
        {
            if (slice.Length == 0)
            {
                return [total];
            }

            long[] nextSlice = slice[1..];

            IEnumerable<long> possible = PossibleSums(part, nextSlice, total + slice[0]);
            possible = possible.Concat(PossibleSums(part, nextSlice, total * slice[0]));

            if (part == Part.Two)
            {
                long concat = long.Parse($"{total}{slice[0]}");
                possible = possible.Concat(PossibleSums(part, nextSlice, concat));
            }

            return possible;
        }
    }
}
