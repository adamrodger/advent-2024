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
            SortedDictionary<string, ICollection<string>> graph = new();
            SortedSet<(string, string)> edges = [];

            foreach (string line in input)
            {
                string[] elements = line.Split('-');
                Debug.Assert(elements.Length == 2);

                graph.GetOrCreate(elements[0], () => new SortedSet<string>()).Add(elements[1]);
                graph.GetOrCreate(elements[1], () => new SortedSet<string>()).Add(elements[0]);

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

            string[] nodes = graph.Keys.Order().ToArray();

            foreach (string node in nodes)
            {
                foreach (string left in graph[node].Where(l => string.CompareOrdinal(node, l) < 0))
                {
                    foreach (string right in graph[left].Where(r => string.CompareOrdinal(left, r) < 0))
                    {
                        if (!edges.Contains((node, right)))
                        {
                            continue;
                        }

                        if (node.StartsWith('t') || left.StartsWith('t') || right.StartsWith('t'))
                        {
                            total++;
                        }
                    }
                }
            }

            // 148884 high
            // 2819 high, but works for sample... Did a Contains instead of StartsWith... reading fail
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
