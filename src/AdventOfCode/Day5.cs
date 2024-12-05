using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 5
    /// </summary>
    public class Day5
    {
        public int Part1(string[] input)
        {
            var precedence = new Dictionary<int, HashSet<int>>();
            var pages = new List<int[]>();
            bool rules = true;

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    rules = false;
                    continue;
                }

                if (rules)
                {
                    var n = line.Numbers<int>();
                    precedence.GetOrCreate(n[0], () => new HashSet<int>()).Add(n[1]);
                }
                else
                {
                    pages.Add(line.Numbers<int>());
                }
            }

            var foo = precedence.ToDictionary(p => p.Key, p => p.Value.ToArray());

            return pages.Where(p => IsValid(p, foo))
                        .Select(p => p[p.Length / 2])
                        .Sum();
        }

        public int Part2(string[] input)
        {
            var precedence = new Dictionary<int, HashSet<int>>();
            var pages = new List<int[]>();
            bool rules = true;

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    rules = false;
                    continue;
                }

                if (rules)
                {
                    var n = line.Numbers<int>();
                    precedence.GetOrCreate(n[0], () => new HashSet<int>()).Add(n[1]);
                }
                else
                {
                    pages.Add(line.Numbers<int>());
                }
            }

            var foo = precedence.ToDictionary(p => p.Key, p => p.Value.ToArray());

            var comparer = new PageComparer(foo);
            var invalid = pages.Where(p => !IsValid(p, foo));
            int total = 0;

            foreach (int[] page in invalid)
            {
                Array.Sort(page, comparer);
                total += page[page.Length / 2];
            }

            return total;
        }

        private static bool IsValid(int[] pages, Dictionary<int, int[]> precedence)
        {
            for (int i = 1; i < pages.Length; i++)
            {
                if (!precedence.TryGetValue(pages[i], out int[] banned))
                {
                    continue;
                }

                ReadOnlySpan<int> slice = pages.AsSpan(0, i);

                if (slice.ContainsAny(banned))
                {
                    return false;
                }
            }

            return true;
        }

        private class PageComparer(IDictionary<int, int[]> precedence) : IComparer<int>
        {
            /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
            /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description><paramref name="x" /> is less than <paramref name="y" />.</description></item><item><term> Zero</term><description><paramref name="x" /> equals <paramref name="y" />.</description></item><item><term> Greater than zero</term><description><paramref name="x" /> is greater than <paramref name="y" />.</description></item></list></returns>
            public int Compare(int x, int y)
            {
                if (precedence.TryGetValue(x, out int[] banned) && banned.Contains(y))
                {
                    return -1;
                }

                if (precedence.TryGetValue(y, out banned) && banned.Contains(x))
                {
                    return 1;
                }

                return 0;
            }
        }
    }
}
