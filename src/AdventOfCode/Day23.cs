using System.Collections.Generic;
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
            SortedDictionary<string, ICollection<string>> graph = BuildGraph(input);

            int total = 0;

            /* Look for triangles of interconnected nodes, i.e.

                              node
                             /    \
                            /      \
                          left -- right
             */
            foreach (string node in graph.Keys)
            {
                foreach (string left in graph[node].Where(l => string.CompareOrdinal(node, l) < 0))
                {
                    foreach (string right in graph[left].Where(r => string.CompareOrdinal(left, r) < 0))
                    {
                        if (!graph[node].Contains(right))
                        {
                            continue;
                        }

                        // found a triangle
                        if (node.StartsWith('t') || left.StartsWith('t') || right.StartsWith('t'))
                        {
                            total++;
                        }
                    }
                }
            }

            return total;
        }

        public string Part2(string[] input)
        {
            SortedDictionary<string, ICollection<string>> graph = new();

            foreach (string line in input)
            {
                string[] elements = line.Split('-');

                graph.GetOrCreate(elements[0], () => new List<string>()).Add(elements[1]);
                graph.GetOrCreate(elements[1], () => new List<string>()).Add(elements[0]);
            }

            List<string> biggest = [];
            HashSet<string> visited = [];

            foreach (string node in graph.Keys)
            {
                if (!visited.Add(node))
                {
                    // already part of another clique
                    continue;
                }

                List<string> clique = [node];

                foreach (string candidate in graph.Keys)
                {
                    if (visited.Contains(candidate))
                    {
                        // already part of another clique
                        continue;
                    }

                    // if it's connected to every current member of the clique then it's allowed in
                    if (clique.All(member => graph[member].Contains(candidate)))
                    {
                        clique.Add(candidate);
                        visited.Add(candidate);
                    }
                }

                if (clique.Count > biggest.Count)
                {
                    biggest = clique;
                }
            }

            return string.Join(',', biggest.Order());
        }

        /// <summary>
        /// Build the graph of every node to all the reachable nodes from that point
        /// </summary>
        /// <param name="input">Input edge descriptions</param>
        /// <returns>Graph</returns>
        private static SortedDictionary<string, ICollection<string>> BuildGraph(string[] input)
        {
            SortedDictionary<string, ICollection<string>> graph = new();

            foreach (string line in input)
            {
                string[] elements = line.Split('-');

                graph.GetOrCreate(elements[0], () => new List<string>()).Add(elements[1]);
                graph.GetOrCreate(elements[1], () => new List<string>()).Add(elements[0]);
            }

            return graph;
        }
    }
}
