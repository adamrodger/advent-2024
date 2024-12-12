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
            List<(char Id, HashSet<Point2D> Points)> regions = CalculateRegions(grid);

            return regions.Select(r => r.Points.Count * CountPerimeter(grid, r.Id, r.Points)).Sum();
        }

        public int Part2(string[] input)
        {
            var grid = input.ToGrid();
            List<(char Id, HashSet<Point2D> Points)> regions = CalculateRegions(grid);

            return regions.Select(r => r.Points.Count * CountSides(grid, r.Id, r.Points)).Sum();
        }

        /// <summary>
        /// Calculate the disjoint contiguous regions in the grid
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <returns>Regions, with ID and points within the region</returns>
        private static List<(char Id, HashSet<Point2D> Points)> CalculateRegions(char[,] grid)
        {
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

            return regions;
        }

        /// <summary>
        /// Find the region starting at the given point
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="start">Start point</param>
        /// <returns>All points within the same region as the start point</returns>
        private static HashSet<Point2D> FindRegion(char[,] grid, Point2D start)
        {
            char id = grid.At(start);

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

        /// <summary>
        /// Count the perimeter of a region
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="id">Region ID</param>
        /// <param name="region">Region points</param>
        /// <returns>Perimeter size</returns>
        private static int CountPerimeter(char[,] grid, char id, HashSet<Point2D> region)
        {
            // the same point can count multiple times as long as it was reached from a different direction
            var perimeter = region.SelectMany(Adjacent)
                                  .Where(p => !p.Point.InBounds(grid) || grid.At(p.Point) != id)
                                  .ToHashSet();

            return perimeter.Count;
        }

        /// <summary>
        /// Count the number of sides of a region
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="id">Region ID</param>
        /// <param name="region">Region points</param>
        /// <returns>Number of sides</returns>
        private static int CountSides(char[,] grid, char id, HashSet<Point2D> region)
        {
            int sides = 0;

            foreach (Point2D p in region)
            {
                // check which of the 8 surrounding positions are still in the region
                bool above = grid.AtOrDefault(p + (0, -1)) == id;
                bool below = grid.AtOrDefault(p + (0, 1)) == id;
                bool left = grid.AtOrDefault(p + (-1, 0)) == id;
                bool right = grid.AtOrDefault(p + (1, 0)) == id;
                bool topLeft = grid.AtOrDefault(p + (-1, -1)) == id;
                bool topRight = grid.AtOrDefault(p + (1, -1)) == id;
                bool bottomLeft = grid.AtOrDefault(p + (-1, 1)) == id;
                bool bottomRight = grid.AtOrDefault(p + (1, 1)) == id;

                // outer corners
                // the central point is the corner we're trying to detect
                // 
                //   NE     NW      SW     SE
                //   ...    ...     .##    ##.
                //   ##.    .##     .##    ##.
                //   ##.    .##     ...    ...
                if (!above && !right) sides++;
                if (!above && !left) sides++;
                if (!below && !left) sides++;
                if (!below && !right) sides++;

                // inner corners
                // the central point is the corner we're trying to detect
                // 
                //   NE     NW      SW     SE
                //   ##.    .##     ###    ###
                //   ###    ###     ###    ###
                //   ###    ###     .##    ##.
                if (above && right && !topRight) sides++;
                if (above && left && !topLeft) sides++;
                if (below && left && !bottomLeft) sides++;
                if (below && right && !bottomRight) sides++;
            }

            return sides;
        }

        /// <summary>
        /// The adjacent 4 positions of a point along with the direction of travel to get there
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>Next position plus the bearing from the start point</returns>
        private static IEnumerable<(Point2D Point, Bearing Bearing)> Adjacent(Point2D point)
        {
            yield return (point with { Y = point.Y - 1 }, Bearing.North);
            yield return (point with { X = point.X - 1 }, Bearing.West);
            yield return (point with { X = point.X + 1 }, Bearing.East);
            yield return (point with { Y = point.Y + 1 }, Bearing.South);
        }
    }
}
