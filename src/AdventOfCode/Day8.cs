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
            var antennae = new HashSet<(char Id, Point2D Point)>();
            var antinodes = new HashSet<Point2D>();

            char[,] grid = input.ToGrid();
            grid.ForEach((x, y, c) =>
            {
                if (c != '.') antennae.Add((c, (x, y)));
            });

            ILookup<char, (char Id, Point2D Point)> groups = antennae.ToLookup(a => a.Id);

            foreach (IGrouping<char, (char Id, Point2D Point)> group in groups)
            {
                foreach ((Point2D left, int i) in group.Select(((tuple, i) => (tuple.Point, i))))
                {
                    foreach (Point2D right in group.Skip(i + 1).Select(g => g.Point))
                    {
                        (Point2D top, Point2D bottom) = left.Y < right.Y
                                                            ? (left, right)
                                                            : (right, left);

                        Point2D vector = bottom - top;

                        var higher = top - vector;
                        var lower = bottom + vector;

                        if (higher.InBounds(grid))
                        {
                            antinodes.Add(higher);
                        }

                        if (lower.InBounds(grid))
                        {
                            antinodes.Add(lower);
                        }
                    }
                }
            }

            // 2499 high
            return antinodes.Count;
        }

        public int Part2(string[] input)
        {
            var antennae = new HashSet<(char Id, Point2D Point)>();

            char[,] grid = input.ToGrid();
            grid.ForEach((x, y, c) =>
            {
                if (c != '.') antennae.Add((c, (x, y)));
            });

            var antinodes = antennae.Select(pair => pair.Point).ToHashSet();

            ILookup<char, (char Id, Point2D Point)> groups = antennae.ToLookup(a => a.Id);

            foreach (IGrouping<char, (char Id, Point2D Point)> group in groups)
            {
                foreach ((Point2D left, int i) in group.Select(((tuple, i) => (tuple.Point, i))))
                {
                    foreach (Point2D right in group.Skip(i + 1).Select(g => g.Point))
                    {
                        (Point2D top, Point2D bottom) = left.Y < right.Y
                                                            ? (left, right)
                                                            : (right, left);

                        Point2D vector = bottom - top;



                        var higher = top - vector;

                        while (higher.InBounds(grid))
                        {
                            antinodes.Add(higher);
                            higher = higher - vector;
                        }

                        var lower = bottom + vector;

                        while (lower.InBounds(grid))
                        {
                            antinodes.Add(lower);
                            lower = lower + vector;
                        }
                    }
                }
            }

            return antinodes.Count;
        }
    }
}
