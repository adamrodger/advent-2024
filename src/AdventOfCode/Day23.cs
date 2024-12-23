using System.Collections.Generic;
using System.Collections.Immutable;
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

            var cliques = new List<IImmutableSet<string>>();
            BronKerbosch(graph, [], graph.Keys.ToImmutableSortedSet(), [], cliques);

            IImmutableSet<string> biggest = cliques.MaxBy(c => c.Count);

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

        /// <summary>
        /// Sort the graph into 'cliques', which are clusters in which every node is connected
        /// to every other node.
        /// 
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

            foreach (string candidate in candidates.ToArray())
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
