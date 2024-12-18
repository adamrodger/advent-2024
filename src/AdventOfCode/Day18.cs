using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 18
    /// </summary>
    public class Day18
    {
        public int Part1(string[] input, int width = 70)
        {
            var graph = BuildGraph(input, width, 1024);
            return graph.GetShortestPath((0, 0), (width, width)).Last().distance;
        }

        public string Part2(string[] input, int width = 70)
        {
            int lowestValid = 1024; // we know there's definitely a valid path here from p1
            int highestInvalid = input.Length; // we know there's no valid path here otherwise there is no answer

            while (highestInvalid - lowestValid > 1)
            {
                int mid = (highestInvalid + lowestValid) / 2;

                Graph<Point2D> graph = BuildGraph(input, width, mid);

                if (graph.GetShortestPath((0, 0), (width, width)) == null)
                {
                    highestInvalid = mid;
                }
                else
                {
                    lowestValid = mid;
                }
            }

            return input[highestInvalid - 1];
        }

        private static Graph<Point2D> BuildGraph(string[] input, int width, int wallCount)
        {
            var graph = new Graph<Point2D>(Graph<Point2D>.ManhattanDistanceHeuristic);

            var walls = input.Select(i => i.Numbers<int>())
                             .Select(n => new Point2D(n[0], n[1]))
                             .Take(wallCount)
                             .ToHashSet();

            for (int y = 0; y <= width; y++)
            {
                for (int x = 0; x <= width; x++)
                {
                    Point2D point = (x, y);

                    foreach (Point2D adjacent in point.Adjacent4()
                                                      .Where(p => p.X >= 0
                                                               && p.X <= width
                                                               && p.Y >= 0
                                                               && p.Y <= width
                                                               && !walls.Contains(p)))
                    {
                        graph.AddVertex(point, adjacent);
                    }
                }
            }

            return graph;
        }
    }
}
