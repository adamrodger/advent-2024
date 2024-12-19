using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 19
    /// </summary>
    public class Day19
    {
        public int Part1(string[] input)
        {
            string[] allowed = input[0].Split(", ");
            var cache = new Dictionary<string, bool>();
            return input.Skip(2).Count(i => Possible(i, allowed, cache));
        }

        public long Part2(string[] input)
        {
            string[] allowed = input[0].Split(", ");
            var cache = new Dictionary<string, long>();
            return input.Skip(2).Sum(i => Possible2(i, allowed, cache));
        }

        private static bool Possible(string check, string[] allowed, Dictionary<string, bool> cache)
        {
            if (check.Length == 0)
            {
                return true;
            }

            if (cache.TryGetValue(check, out bool result)) return result;

            result = allowed.Where(a => a.Length <= check.Length)
                            .Where(check.StartsWith)
                            .Any(a => Possible(check[a.Length..], allowed, cache));

            cache[check] = result;
            return result;
        }

        private static long Possible2(string check, string[] allowed, Dictionary<string, long> cache)
        {
            if (check.Length == 0)
            {
                return 1;
            }

            if (cache.TryGetValue(check, out long result)) return result;

            result = allowed.Where(a => a.Length <= check.Length)
                            .Where(check.StartsWith)
                            .Sum(a => Possible2(check[a.Length..], allowed, cache));

            cache[check] = result;
            return result;
        }
    }
}
