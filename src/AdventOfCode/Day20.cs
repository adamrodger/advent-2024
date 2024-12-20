using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 20
    /// </summary>
    public class Day20
    {
        public int Part1(string[] input, int minSaving = 100)
        {
            Dictionary<Point2D, int> path = CreateTrack(input);
            return PossibleCheats(path, 2, minSaving);
        }

        public int Part2(string[] input, int minSaving = 100)
        {
            Dictionary<Point2D, int> path = CreateTrack(input);
            return PossibleCheats(path, 20, minSaving);
        }

        /// <summary>
        /// Create a track from the input
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Lookup of points on the track to how many steps they are from the start</returns>
        private static Dictionary<Point2D, int> CreateTrack(string[] input)
        {
            char[,] grid = input.ToGrid();

            Point2D start = default;
            Point2D end = default;

            grid.ForEach((x, y, c) =>
            {
                if (c == 'S') start = new Point2D(x, y);
                if (c == 'E') end = new Point2D(x, y);
            });

            // walk the track, noting distance from the start
            Point2D current = start;
            int distance = 0;
            Dictionary<Point2D, int> steps = new();

            while (current != end)
            {
                steps[current] = distance++;
                current = current.Adjacent4().First(a => !steps.ContainsKey(a) && grid.AtOrDefault(a) != '#');
            }

            // last step to the end point
            steps[end] = distance;

            return steps;
        }

        /// <summary>
        /// Count how many possible cheats of the max allowed length result in the minimum expected saving
        /// </summary>
        /// <param name="path">Track path</param>
        /// <param name="maxCheatSize">Maximum allowed cheat size</param>
        /// <param name="minSaving">Minimum saving required</param>
        /// <returns>Possible cheat paths</returns>
        private static int PossibleCheats(Dictionary<Point2D, int> path, int maxCheatSize, int minSaving)
        {
            int total = 0;

            foreach ((Point2D point, int distance) in path)
            {
                foreach (Point2D destination in point.ReachablePoints(maxCheatSize))
                {
                    if (!path.TryGetValue(destination, out int destinationDistance))
                    {
                        continue;
                    }

                    // we save the steps between the dest and src, but it costs us the steps in the shortcut
                    int saving = destinationDistance - distance - destination.ManhattanDistance(point);

                    if (saving >= minSaving)
                    {
                        total++;
                    }
                }
            }

            return total;
        }
    }
}
