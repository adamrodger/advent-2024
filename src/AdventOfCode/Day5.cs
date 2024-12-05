using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 5
    /// </summary>
    public class Day5
    {
        public int Part1(string[] input)
        {
            (ICollection<int[]> pages, ICollection<int[]> ordered) = Parse(input);

            return pages.Zip(ordered)
                        .Where(p => p.First.SequenceEqual(p.Second))
                        .Select(p => p.First[p.First.Length / 2])
                        .Sum();
        }

        public int Part2(string[] input)
        {
            (ICollection<int[]> pages, ICollection<int[]> ordered) = Parse(input);

            return pages.Zip(ordered)
                        .Where(p => !p.First.SequenceEqual(p.Second))
                        .Select(p => p.Second[p.Second.Length / 2])
                        .Sum();
        }

        /// <summary>
        /// Parse the input
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Original ordering for each page collection, and an ordered version (which may be the same)</returns>
        private static (ICollection<int[]> Pages, ICollection<int[]> SortedPages) Parse(string[] input)
        {
            var precedence = new HashSet<(int X, int Y)>();
            int linesParsed = 0;

            foreach (string line in input)
            {
                linesParsed++;

                if (string.IsNullOrEmpty(line))
                {
                    // there's a blank line between the rules and the page orderings
                    break;
                }

                int separator = line.IndexOf('|');
                int left = int.Parse(line[..separator]);
                int right = int.Parse(line[(separator + 1)..]);

                precedence.Add((left, right));
            }

            var pages = input.Skip(linesParsed)
                             .Select(p => p.Split(',').Select(int.Parse).ToArray())
                             .ToArray();

            var comparer = new PageComparer(precedence);
            var ordered = pages.Select(p => p.OrderBy(x => x, comparer).ToArray()).ToArray();

            return (pages, ordered);
        }

        /// <summary>
        /// Compares page numbers according to the precedence rules
        /// </summary>
        /// <param name="precedence">Page precedence rules</param>
        private class PageComparer(ISet<(int X, int Y)> precedence) : IComparer<int>
        {
            /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
            /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description><paramref name="x" /> is less than <paramref name="y" />.</description></item><item><term> Zero</term><description><paramref name="x" /> equals <paramref name="y" />.</description></item><item><term> Greater than zero</term><description><paramref name="x" /> is greater than <paramref name="y" />.</description></item></list></returns>
            public int Compare(int x, int y)
            {
                if (precedence.Contains((x, y)))
                {
                    return -1;
                }

                if (precedence.Contains((y, x)))
                {
                    return 1;
                }

                return 0;
            }
        }
    }
}
