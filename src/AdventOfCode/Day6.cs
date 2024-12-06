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
            char[,] grid = input.ToGrid();

            Point2D current = (-1, -1);

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == '^')
                    {
                        current = (x, y);
                    }
                }
            }

            if (!current.InBounds(grid)) throw new IndexOutOfRangeException();

            Bearing bearing = Bearing.North;

            var visited = new HashSet<Point2D>();

            while (current.InBounds(grid))
            {
                visited.Add(current);
                Point2D next = current.Move(bearing);

                if (next.InBounds(grid) && grid[next.Y, next.X] == '#')
                {
                    bearing = bearing.Turn(TurnDirection.Right);
                    next = current.Move(bearing);
                }

                current = next;
            }

            return visited.Count;
        }

        public int Part2(string[] input)
        {
            char[,] grid = input.ToGrid();

            Point2D start = (-1, -1);

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == '^')
                    {
                        start = (x, y);
                    }
                }
            }

            if (!start.InBounds(grid)) throw new IndexOutOfRangeException();

            int total = 0;

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] != '.') continue;

                    grid[y, x] = '#';
                    if (FormsLoop(grid, start)) total++;
                    grid[y, x] = '.';
                }
            }

            // 1573 low
            return total;
        }

        private static bool FormsLoop(char[,] grid, Point2D start)
        {
            Point2D current = start;
            Bearing bearing = Bearing.North;

            var visited = new HashSet<(Point2D, Bearing)>();

            while (current.InBounds(grid))
            {
                if (!visited.Add((current, bearing)))
                {
                    return true;
                }

                var next = current.Move(bearing);

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
