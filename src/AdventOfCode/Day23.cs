using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 23
    /// </summary>
    public class Day23
    {
        public int Part1(string[] input)
        {
            SortedSet<string> nodes = [];
            SortedSet<(string, string)> edges = [];

            foreach (string line in input)
            {
                string[] elements = line.Split('-');
                Debug.Assert(elements.Length == 2);

                nodes.Add(elements[0]);
                nodes.Add(elements[1]);

                if (string.Compare(elements[0], elements[1], StringComparison.Ordinal) < 0)
                {
                    edges.Add((elements[0], elements[1]));
                }
                else
                {
                    edges.Add((elements[1], elements[0]));
                }
            }

            int total = 0;

            foreach ((int i, string node) in nodes.Enumerate())
            {
                foreach ((int j, string left) in nodes.Enumerate().Skip(i + 1))
                {
                    if (!edges.Contains((node, left)))
                    {
                        continue;
                    }

                    foreach (string right in nodes.Skip(j + 1))
                    {
                        if (!edges.Contains((node, right)))
                        {
                            continue;
                        }

                        if (!edges.Contains((left, right)))
                        {
                            continue;
                        }

                        if (node.Contains('t') || left.Contains('t') || right.Contains('t'))
                        {
                            total++;
                        }
                    }
                }
            }

            // 148884 high
            // 2819 high, but works for sample...
            return total;
        }

        public int Part2(string[] input)
        {
            foreach (string line in input)
            {
                throw new NotImplementedException("Part 2 not implemented");
            }

            return 0;
        }
    }
}
