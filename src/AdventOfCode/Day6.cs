using System;
using System.Collections.Generic;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 6
    /// </summary>
    public class Day6
    {
        public int Part1(string[] input)
        {
            (char[,] grid, Point2D current) = Parse(input);

            HashSet<Point2D> visited = Visit(grid, current);

            return visited.Count;
        }

        public int Part2(string[] input)
        {
            (char[,] grid, Point2D start) = Parse(input);

            IEnumerable<Point2D> candidates = Visit(grid, start);

            int total = 0;

            foreach (Point2D candidate in candidates)
            {
                grid[candidate.Y, candidate.X] = '#';

                if (FormsLoop(grid, start))
                {
                    total++;
                }

                grid[candidate.Y, candidate.X] = '.';
            }

            return total;
        }

        /// <summary>
        /// Parse the input
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Parsed grid and starting point</returns>
        /// <exception cref="ArgumentException">Unable to find a starting point</exception>
        private static (char[,] grid, Point2D start) Parse(string[] input)
        {
            char[,] grid = input.ToGrid();

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == '^')
                    {
                        return (grid, (x, y));
                    }
                }
            }

            throw new ArgumentException("No start point found");
        }

        /// <summary>
        /// Visit every point on the grid that can be reached from the given start point
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="current">Start point</param>
        /// <returns>All visited points on the grid</returns>
        private static HashSet<Point2D> Visit(char[,] grid, Point2D current)
        {
            Bearing bearing = Bearing.North;

            var visited = new HashSet<Point2D>();

            while (current.InBounds(grid))
            {
                visited.Add(current);

                Point2D next = current.Move(bearing);

                while (next.InBounds(grid) && grid[next.Y, next.X] == '#')
                {
                    bearing = bearing.Turn(TurnDirection.Right);
                    next = current.Move(bearing);
                }

                current = next;
            }

            return visited;
        }

        /// <summary>
        /// Check if the given start point would form an infinite loop on the given grid
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="start">Start point</param>
        /// <returns>Whether a loop is formed or not</returns>
        private static bool FormsLoop(char[,] grid, Point2D start)
        {
            Point2D current = start;
            Bearing bearing = Bearing.North;

            var visited = new HashSet<(Point2D, Bearing)>();

            while (current.InBounds(grid))
            {
                if (!visited.Add((current, bearing)))
                {
                    // we've been here before, and we always will....
                    return true;
                }

                Point2D next = current.Move(bearing);

                while (next.InBounds(grid) && grid[next.Y, next.X] == '#')
                {
                    bearing = bearing.Turn(TurnDirection.Right);
                    next = current.Move(bearing);
                }

                current = next;
            }

            return false;
        }
    }
}
