using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 16
    /// </summary>
    public class Day16
    {
        public int Part1(string[] input)
        {
            return GetShortestCost(input.ToGrid(), (1, input.Length - 2), (input[0].Length - 2, 1));
        }

        public int Part2(string[] input)
        {
            // 512 low
            return GetBestSeats(input.ToGrid(), (1, input.Length - 2), (input[0].Length - 2, 1));
        }

        private static IEnumerable<(Point2D Point, Bearing Direction)> Adjacent4(Point2D point)
        {
            yield return (new Point2D(point.X, point.Y - 1), Bearing.North);
            yield return (new Point2D(point.X - 1, point.Y), Bearing.West);
            yield return (new Point2D(point.X + 1, point.Y), Bearing.East);
            yield return (new Point2D(point.X, point.Y + 1), Bearing.South);
        }

        private static int GetShortestCost(char[,] maze, Point2D start, Point2D finish)
        {
            var open = new PriorityQueue<(Point2D Point, Bearing Direction, int Cost), int>();
            open.Enqueue((start, Bearing.East, 0), 0);

            var closed = new Dictionary<(Point2D Point, Bearing Direction), int>();

            while (open.Count > 0)
            {
                (Point2D Point, Bearing Direction, int Cost) current = open.Dequeue();

                if (current.Point == finish)
                {
                    return current.Cost;
                }

                closed[(current.Point, current.Direction)] = current.Cost;

                // walk outwards along edges to see if we can find a closer node
                foreach ((Point2D Point, Bearing Direction) next in Adjacent4(current.Point))
                {
                    char c = maze[next.Point.Y, next.Point.X];

                    if (c == '#')
                    {
                        continue;
                    }

                    int cost = current.Cost + (current.Direction == next.Direction ? 1 : 1001);

                    // found the first or a closer route to the adjacent node (from the current node)
                    if (closed.GetValueOrDefault(next, int.MaxValue) > cost)
                    {
                        open.Enqueue((next.Point, next.Direction, cost), cost);
                    }
                }
            }

            throw new ArgumentException("No path from start to finish");
        }

        private static int GetBestSeats(char[,] maze, Point2D start, Point2D finish)
        {
            var open = new PriorityQueue<(Point2D Point, Bearing Direction, int Cost, ImmutableStack<Point2D> Path), int>();
            open.Enqueue((start, Bearing.East, 0, [start]), 0);

            var closed = new Dictionary<(Point2D Point, Bearing Direction), int>();

            int shortest = int.MaxValue;
            HashSet<Point2D> seats = [];

            while (open.Count > 0)
            {
                (Point2D Point, Bearing Direction, int Cost, ImmutableStack<Point2D> Path) current = open.Dequeue();

                if (current.Cost > shortest)
                {
                    // stopped hitting the shortest paths so we can give up now
                    break;
                }

                if (current.Point == finish)
                {
                    if (current.Cost < shortest)
                    {
                        shortest = current.Cost;
                        seats.Clear();
                    }

                    seats.UnionWith(current.Path);
                }

                closed[(current.Point, current.Direction)] = current.Cost;

                // walk outwards along edges to see if we can find a closer node
                foreach ((Point2D Point, Bearing Direction) next in Adjacent4(current.Point))
                {
                    char c = maze[next.Point.Y, next.Point.X];

                    if (c == '#')
                    {
                        continue;
                    }

                    int cost = current.Cost + (current.Direction == next.Direction ? 1 : 1001);

                    // found the first or a closer route to the adjacent node (from the current node)
                    if (closed.GetValueOrDefault(next, int.MaxValue) > cost)
                    {
                        open.Enqueue((next.Point, next.Direction, cost, current.Path.Push(next.Point)), cost);
                    }
                }
            }

            return seats.Count;
        }
    }
}
