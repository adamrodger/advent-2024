using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 12
    /// </summary>
    public class Day12
    {
        public int Part1(string[] input)
        {
            var grid = input.ToGrid();

            HashSet<Point2D> claimed = [];
            List<(char, HashSet<Point2D>)> regions = [];

            foreach (Point2D point in grid.Points())
            {
                if (claimed.Contains(point))
                {
                    continue;
                }

                HashSet<Point2D> region = FindRegion(grid, point);
                claimed.UnionWith(region);
                regions.Add((grid.At(point), region));
            }

            // 973268 low
            return regions.Select(r => r.Item2.Count * FindPerimeter(grid, r.Item1, r.Item2)).Sum();
        }

        public int Part2(string[] input)
        {
            var grid = input.ToGrid();

            HashSet<Point2D> claimed = [];
            List<(char, HashSet<Point2D>)> regions = [];

            foreach (Point2D point in grid.Points())
            {
                if (claimed.Contains(point))
                {
                    continue;
                }

                HashSet<Point2D> region = FindRegion(grid, point);
                claimed.UnionWith(region);
                regions.Add((grid.At(point), region));
            }

            // 885869 low
            return regions.Select(r => r.Item2.Count * FindSides(grid, r.Item1, r.Item2)).Sum();
        }

        private static HashSet<Point2D> FindRegion(char[,] grid, Point2D start)
        {
            var id = grid.At(start);

            var region = new HashSet<Point2D> { start };
            var queue = new Queue<Point2D>(region);
            var visited = new HashSet<Point2D>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (!visited.Add(current) || grid.At(current) != id)
                {
                    continue;
                }

                region.Add(current);

                foreach (Point2D next in current.Adjacent4().Where(a => a.InBounds(grid)))
                {
                    queue.Enqueue(next);
                }
            }

            return region;
        }

        private static int FindPerimeter(char[,] grid, char id, HashSet<Point2D> region)
        {
            var perimeter = region.SelectMany(Adjacent)
                                  .Where(p => !p.Point.InBounds(grid) || grid.At(p.Point) != id)
                                  .ToHashSet();

            return perimeter.Count;
        }

        private static int FindSides(char[,] grid, char id, HashSet<Point2D> region)
        {
            int sides = 0;

            foreach (Point2D p in region)
            {
                var above = p + (0, -1);
                var below = p + (0, 1);
                var left = p + (-1, 0);
                var right = p + (1, 0);
                var topLeft = p + (-1, -1);
                var topRight = p + (1, -1);
                var bottomLeft = p + (-1, 1);
                var bottomRight = p + (1, 1);

                // outer corners
                // the central point is the corner we're trying to detect
                // 
                //   NE     NW      SW     SE
                //   ...    ...     .##    ##.
                //   ##.    .##     .##    ##.
                //   ##.    .##     ...    ...

                if (grid.AtOrDefault(above) != id && grid.AtOrDefault(right) != id) sides++;
                if (grid.AtOrDefault(above) != id && grid.AtOrDefault(left) != id) sides++;
                if (grid.AtOrDefault(below) != id && grid.AtOrDefault(left) != id) sides++;
                if (grid.AtOrDefault(below) != id && grid.AtOrDefault(right) != id) sides++;

                // inner corners
                // the central point is the corner we're trying to detect
                // 
                //   NE     NW      SW     SE
                //   ##.    .##     ###    ###
                //   ###    ###     ###    ###
                //   ###    ###     .##    ##.
                if (grid.AtOrDefault(above) == id && grid.AtOrDefault(right) == id && topRight.InBounds(grid) && grid.At(topRight) != id) sides++;
                if (grid.AtOrDefault(above) == id && grid.AtOrDefault(left) == id && topLeft.InBounds(grid) && grid.At(topLeft) != id) sides++;
                if (grid.AtOrDefault(below) == id && grid.AtOrDefault(left) == id && bottomLeft.InBounds(grid) && grid.At(bottomLeft) != id) sides++;
                if (grid.AtOrDefault(below) == id && grid.AtOrDefault(right) == id && bottomRight.InBounds(grid) && grid.At(bottomRight) != id) sides++;
            }

            return sides;

            //var perimeter = region.SelectMany(Adjacent)
            //                      .Where(p => !p.Point.InBounds(grid) || grid.At(p.Point) != id)
            //                      .ToHashSet();

            //var points = perimeter.Select(p => p.Point).ToHashSet();

            //// find the top left corner
            //var minY = points.Select(p => p.Y).Min();
            //Point2D start = points.Where(p => p.Y == minY).MinBy(p => p.X);

            //// keeping the region on your right, count the number of times we need to turn (i.e. we hit a corner)
            //int turns = 0;
            //Bearing bearing = Bearing.East;
            //var current = start.Move(bearing);

            //while (current != start)
            //{
            //    var next = current.Move(bearing);

            //    if (!points.Contains(next))
            //    {
            //        bearing = bearing.Turn(TurnDirection.Right);
            //        next = next.Move(bearing);
            //        turns++;
            //    }

            //    current = next;
            //}

            //Debug.Assert(turns >= 4);
            //return turns;
        }

        private static IEnumerable<(Point2D Point, Bearing Bearing)> Adjacent(Point2D point)
        {
            yield return (new Point2D(point.X, point.Y - 1), Bearing.North);
            yield return (new Point2D(point.X - 1, point.Y), Bearing.West);
            yield return (new Point2D(point.X + 1, point.Y), Bearing.East);
            yield return (new Point2D(point.X, point.Y + 1), Bearing.South);
        }
    }
}
