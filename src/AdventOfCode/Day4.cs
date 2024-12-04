using System;
using System.Collections.Generic;
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

            (Bearing, Bearing?)[] directions =
            [
                (Bearing.North, null),
                (Bearing.North, Bearing.East),
                (Bearing.East, null),
                (Bearing.South, Bearing.East),
                (Bearing.South, null),
                (Bearing.South, Bearing.West),
                (Bearing.West, null),
                (Bearing.North, Bearing.West)
            ];

            int total = 0;

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    Point2D point = (x, y);

                    total += directions.Select(d => Slice(grid, point, d.Item1, d.Item2))
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

        /// <summary>
        /// Take a slice of the grid along a given bearing
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="current">Start point</param>
        /// <param name="bearing">Bearing</param>
        /// <param name="otherBearing">Other bearing, for moving diagonally</param>
        /// <param name="size">Size of the slice</param>
        /// <returns>Slice of the grid</returns>
        private static IEnumerable<char> Slice(char[,] grid, Point2D current, Bearing bearing, Bearing? otherBearing, int size = 4)
        {
            yield return grid[current.Y, current.X];

            for (int i = 0; i < size - 1; i++)
            {
                current = current.Move(bearing);

                // TODO: Add diagonal bearings to utility methods so this can be done in a single operation
                if (otherBearing.HasValue)
                {
                    current = current.Move(otherBearing.Value);
                }

                if (!current.InBounds(grid))
                {
                    yield break;
                }

                yield return grid[current.Y, current.X];
            }
        }
    }
}
