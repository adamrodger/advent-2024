using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 8
    /// </summary>
    public class Day8
    {
        public int Part1(string[] input)
        {
            (char[,] grid, ILookup<char, Point2D> groups) = Parse(input);

            var antinodes = new HashSet<Point2D>();

            foreach ((Point2D top, Point2D bottom, Point2D vector) in Pairwise(groups))
            {
                Point2D higher = top - vector;
                Point2D lower = bottom + vector;

                if (higher.InBounds(grid))
                {
                    antinodes.Add(higher);
                }

                if (lower.InBounds(grid))
                {
                    antinodes.Add(lower);
                }
            }

            return antinodes.Count;
        }

        public int Part2(string[] input)
        {
            (char[,] grid, ILookup<char, Point2D> groups) = Parse(input);

            var antinodes = new HashSet<Point2D>();

            foreach ((Point2D top, Point2D bottom, Point2D vector) in Pairwise(groups))
            {
                Point2D higher = top + vector; // first iteration will just hit the other node position

                while (higher.InBounds(grid))
                {
                    antinodes.Add(higher);
                    higher += vector;
                }

                Point2D lower = bottom - vector;

                while (lower.InBounds(grid))
                {
                    antinodes.Add(lower);
                    lower -= vector;
                }
            }

            return antinodes.Count;
        }

        private static (char[,] Grid, ILookup<char, Point2D> Antennae) Parse(string[] input)
        {
            var antennae = new HashSet<(char Id, Point2D Point)>();

            char[,] grid = input.ToGrid();
            grid.ForEach((x, y, c) =>
            {
                if (c != '.') antennae.Add((c, (x, y)));
            });

            // groups will always be in ascending order of Y
            ILookup<char, Point2D> groups = antennae.ToLookup(a => a.Id, a => a.Point);

            return (grid, groups);
        }

        private static IEnumerable<(Point2D Top, Point2D Bottom, Point2D Vector)> Pairwise(ILookup<char, Point2D> antennae)
        {
            foreach (IGrouping<char, Point2D> group in antennae)
            {
                foreach ((Point2D top, int i) in group.Select(((point, i) => (point, i))))
                {
                    foreach (Point2D bottom in group.Skip(i + 1))
                    {
                        yield return (top, bottom, bottom - top);
                    }
                }
            }
        }
    }
}
