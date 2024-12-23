using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

            foreach (string node in graph.Keys)
            {
                foreach (string left in graph[node].Where(l => string.CompareOrdinal(node, l) < 0))
                {
                    foreach (string right in graph[left].Where(r => string.CompareOrdinal(left, r) < 0))
                    {
                        if (!edges.Contains((node, right)))
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

            // 148884 high
            // 2819 high, but works for sample... Did a Contains instead of StartsWith... reading fail
            return total;
        }

        public string Part2(string[] input)
        {
            SortedDictionary<string, ICollection<string>> graph = new();

            foreach (string line in input)
            {
                string[] elements = line.Split('-');
                Debug.Assert(elements.Length == 2);

                graph.GetOrCreate(elements[0], () => new SortedSet<string>()).Add(elements[1]);
                graph.GetOrCreate(elements[1], () => new SortedSet<string>()).Add(elements[0]);
            }

            var cliques = new List<IImmutableSet<string>>();
            BronKerbosch(graph, [], graph.Keys.ToImmutableSortedSet(), [], cliques);

            IImmutableSet<string> biggest = cliques.MaxBy(c => c.Count);

            return string.Join(',', biggest.Order());
        }

        /// <summary>
        /// Had to do some Googling for this one... Never heard of it before
        /// </summary>
        /// <param name="graph">Graph of node ID to the IDs of all connected nodes</param>
        /// <param name="clique">Current clique being considered</param>
        /// <param name="candidates">Candidate nodes to be added to the clique</param>
        /// <param name="visited">Nodes already visited</param>
        /// <param name="cliques">All cliques found</param>
        private static void BronKerbosch(IDictionary<string, ICollection<string>> graph,
                                         IImmutableSet<string> clique,
                                         IImmutableSet<string> candidates,
                                         IImmutableSet<string> visited,
                                         ICollection<IImmutableSet<string>> cliques)
        {
            if (candidates.Count == 0)
            {
                cliques.Add(clique);
                return;
            }

            foreach (var candidate in candidates.ToArray())
            {
                BronKerbosch(graph,
                             clique.Add(candidate),
                             candidates.Intersect(graph[candidate]),
                             visited.Intersect(graph[candidate]),
                             cliques);

                candidates = candidates.Remove(candidate);
                visited = visited.Add(candidate);
            }
        }
    }
}
