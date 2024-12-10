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
            var grid = input.ToGrid<int>();
            int total = 0;

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == 0)
                    {
                        total += PossiblePeaks(grid, (x, y));
                    }
                }
            }

            return total;
        }

        public int Part2(string[] input)
        {
            var grid = input.ToGrid<int>();
            int total = 0;

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == 0)
                    {
                        total += PossiblePeaks2(grid, (x, y));
                    }
                }
            }

            return total;
        }

        private static int PossiblePeaks(int[,] grid, Point2D start)
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

                int height = grid[current.Y, current.X];

                if (height == 9)
                {
                    score++;
                    continue;
                }

                foreach (Point2D next in grid.Adjacent4Positions(current).Where(n => grid[n.Y, n.X] - height == 1 && !visited.Contains(n)))
                {
                    queue.Enqueue(next);
                }
            }

            return score;
        }



        private static int PossiblePeaks2(int[,] grid, Point2D start)
        {
            int score = 0;

            var queue = new Queue<Point2D>([start]);

            while (queue.Count > 0)
            {
                Point2D current = queue.Dequeue();

                int height = grid[current.Y, current.X];

                if (height == 9)
                {
                    score++;
                    continue;
                }

                foreach (Point2D next in grid.Adjacent4Positions(current).Where(n => grid[n.Y, n.X] - height == 1))
                {
                    queue.Enqueue(next);
                }
            }

            return score;
        }
    }
}
