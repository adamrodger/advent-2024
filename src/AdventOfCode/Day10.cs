using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 10
    /// </summary>
    public class Day10
    {
        public int Part1(string[] input)
        {
            var grid = input.ToGrid();

            return grid.Points()
                       .Where(p => grid.At(p) == '0')
                       .Select(p => PossiblePeaks(grid, p))
                       .Sum();
        }

        public int Part2(string[] input)
        {
            var grid = input.ToGrid();

            return grid.Points()
                       .Where(p => grid.At(p) == '0')
                       .Select(p => PossiblePaths(grid, p))
                       .Sum();
        }

        /// <summary>
        /// Count the possible number of reachable peaks from the start point
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="start">Start point</param>
        /// <returns>Possible reachable peaks</returns>
        private static int PossiblePeaks(char[,] grid, Point2D start)
        {
            int score = 0;

            var queue = new Queue<Point2D>([start]);
            var visited = new HashSet<Point2D>();

            while (queue.Count > 0)
            {
                Point2D current = queue.Dequeue();

                if (!visited.Add(current))
                {
                    continue;
                }

                char height = grid[current.Y, current.X];

                if (height == '9')
                {
                    score++;
                    continue;
                }

                foreach (Point2D next in grid.Adjacent4Positions(current).Where(n => grid.At(n) - height == 1))
                {
                    queue.Enqueue(next);
                }
            }

            return score;
        }

        /// <summary>
        /// Count the possible paths from the start point that can reach a peak
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="start">Start point</param>
        /// <returns>Possible paths</returns>
        private static int PossiblePaths(char[,] grid, Point2D start)
        {
            int score = 0;

            var queue = new Queue<Point2D>([start]);

            while (queue.Count > 0)
            {
                Point2D current = queue.Dequeue();

                char height = grid[current.Y, current.X];

                if (height == '9')
                {
                    score++;
                    continue;
                }

                foreach (Point2D next in grid.Adjacent4Positions(current).Where(n => grid.At(n) - height == 1))
                {
                    queue.Enqueue(next);
                }
            }

            return score;
        }
    }
}
