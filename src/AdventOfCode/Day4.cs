using System;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 4
    /// </summary>
    public class Day4
    {
        public int Part1(string[] input)
        {
            char[,] grid = input.ToGrid();

            Bearing[] directions =
            [
                Bearing.North,
                Bearing.NorthEast,
                Bearing.East,
                Bearing.SouthEast,
                Bearing.South,
                Bearing.SouthWest,
                Bearing.West,
                Bearing.NorthWest
            ];

            int total = 0;

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] != 'X')
                    {
                        continue;
                    }

                    Point2D point = (x, y);

                    total += directions.Select(d => grid.Slice(point, d, 4))
                                       .Count(slice => slice.SequenceEqual("XMAS"));
                }
            }

            return total;
        }

        public int Part2(string[] input)
        {
            char[,] grid = input.ToGrid();

            int total = 0;

            Point2D[] deltas = [(-1, -1), (1, -1), (0, 0), (-1, 1), (1, 1)];
            string[] possible = ["MSAMS", "MMASS", "SSAMM", "SMASM"];

            for (int y = 1; y < grid.GetLength(0); y++)
            {
                for (int x = 1; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] != 'A')
                    {
                        continue;
                    }

                    Point2D point = (x, y);

                    char[] slice = deltas.Select(d => point + d)
                                         .Where(p => p.InBounds(grid))
                                         .Select(p => grid[p.Y, p.X])
                                         .ToArray();

                    total += possible.Count(slice.SequenceEqual);
                }
            }

            return total;
        }
    }
}
