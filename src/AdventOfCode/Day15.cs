using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdventOfCode.Utilities;
using Optional;
using Optional.Unsafe;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 15
    /// </summary>
    public class Day15
    {
        public int Part1(string[] input, int width, int height)
        {
            Point2D robot = default;
            HashSet<Point2D> walls = [];
            List<Box> boxes = [];

            input.Take(height).ForEach((point, c) =>
            {
                switch (c)
                {
                    case '@': robot = point; break;
                    case '.': break;
                    case '#': walls.Add(point); break;
                    case 'O': boxes.Add(new Box { Position = point }); break;
                    default: throw new ArgumentOutOfRangeException();
                }
            });

            Debug.Assert(robot != default);

            IEnumerable<char> instructions = input.Skip(height).SelectMany(line => line);

            foreach (char instruction in instructions)
            {
                Bearing bearing = instruction switch
                {
                    '^' => Bearing.North,
                    'v' => Bearing.South,
                    '>' => Bearing.East,
                    '<' => Bearing.West,
                    _ => throw new ArgumentOutOfRangeException()
                };

                Point2D wall = ClosestWall(robot, walls, bearing);
                Option<Point2D> space = ClosestSpace(robot, wall, boxes, bearing);

                if (!space.HasValue || wall.ManhattanDistance(robot) < space.ValueOrDefault().ManhattanDistance(robot))
                {
                    // can't move because we'd hit a wall before we hit a space
                    Print(instruction, width, height, robot, walls, boxes);
                    continue;
                }

                IEnumerable<Box> obstacles = BoxesToPush(robot, space.ValueOrDefault(), boxes, bearing);

                foreach (Box obstacle in obstacles)
                {
                    obstacle.Position = obstacle.Position.Move(bearing);
                }

                robot = robot.Move(bearing);

                Print(instruction, width, height, robot, walls, boxes);
            }

            // 1411249 low
            return boxes.Select(b => b.Position.Y * 100 + b.Position.X).Sum();
        }

        public int Part2(string[] input)
        {
            foreach (string line in input)
            {
                throw new NotImplementedException("Part 2 not implemented");
            }

            return 0;
        }

        private static Point2D ClosestWall(Point2D start, HashSet<Point2D> walls, Bearing bearing)
        {
            switch (bearing)
            {
                case Bearing.North:
                    return walls.Where(w => w.X == start.X && w.Y < start.Y).MaxBy(w => w.Y);
                case Bearing.South:
                    return walls.Where(w => w.X == start.X && w.Y > start.Y).MinBy(w => w.Y);
                case Bearing.East:
                    return walls.Where(w => w.Y == start.Y && w.X > start.X).MinBy(w => w.X);
                case Bearing.West:
                    return walls.Where(w => w.Y == start.Y && w.X < start.X).MaxBy(w => w.X);
                default:
                    throw new ArgumentOutOfRangeException(nameof(bearing), bearing, null);
            }
        }

        private static Option<Point2D> ClosestSpace(Point2D start, Point2D end, List<Box> boxes, Bearing bearing)
        {
            while (start != end)
            {
                start = start.Move(bearing);

                if (start != end && boxes.All(b => b.Position != start))
                {
                    return start.Some();
                }
            }

            return Option.None<Point2D>();
        }

        private static IEnumerable<Box> BoxesToPush(Point2D start, Point2D end, List<Box> boxes, Bearing bearing)
        {
            switch (bearing)
            {
                case Bearing.North:
                    return boxes.Where(w => w.Position.X == start.X && w.Position.Y < start.Y && w.Position.Y > end.Y);
                case Bearing.South:
                    return boxes.Where(w => w.Position.X == start.X && w.Position.Y > start.Y && w.Position.Y < end.Y);
                case Bearing.East:
                    return boxes.Where(w => w.Position.Y == start.Y && w.Position.X > start.X && w.Position.X < end.X);
                case Bearing.West:
                    return boxes.Where(w => w.Position.Y == start.Y && w.Position.X < start.X && w.Position.X > end.X);
                default:
                    throw new ArgumentOutOfRangeException(nameof(bearing), bearing, null);
            }
        }

        private static void Print(char move, int width, int height, Point2D robot, HashSet<Point2D> walls, List<Box> boxes)
        {
            if (!Debugger.IsAttached)
            {
                return;
            }

            StringBuilder s = new StringBuilder(height * (width * Environment.NewLine.Length));

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point2D point = (x, y);

                    if (point == robot)
                    {
                        s.Append('@');
                    }
                    else if (walls.Contains(point))
                    {
                        s.Append('#');
                    }
                    else if (boxes.Any(b => b.Position == point))
                    {
                        s.Append('O');
                    }
                    else
                    {
                        s.Append('.');
                    }
                }

                s.AppendLine();
            }

            Debug.WriteLine($"Move: {move}:");
            Debug.WriteLine(s.ToString());
        }

        private class Box
        {
            public Point2D Position { get; set; }
        }
    }
}
