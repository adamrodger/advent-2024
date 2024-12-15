using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdventOfCode.Utilities;

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
                    case '#':
                        walls.Add(point);
                        break;
                    case 'O':
                        boxes.Add(new Box{ Left = point, Right = point });
                        break;
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

                var clone = boxes.Select(b => new Box { Left = b.Left, Right = b.Right }).ToList();

                if (TryMove(robot, bearing, walls, clone))
                {
                    boxes = clone;
                    robot = robot.Move(bearing);
                }

                Print(instruction, width, height, robot, walls, boxes, Part.One);
            }

            return boxes.Select(b => b.Left.Y * 100 + b.Left.X).Sum();
        }

        public int Part2(string[] input, int width, int height)
        {
            Point2D robot = default;
            HashSet<Point2D> walls = [];
            List<Box> boxes = [];

            input.Take(height).ForEach((point, c) =>
            {
                point = (point.X * 2, point.Y);

                switch (c)
                {
                    case '@': robot = point; break;
                    case '.': break;
                    case '#':
                        walls.Add(point);
                        walls.Add(point.Move(Bearing.East));
                        break;
                    case 'O':
                        boxes.Add(new Box { Left = point, Right = point.Move(Bearing.East)});
                        break;
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

                var clone = boxes.Select(b => new Box { Left = b.Left, Right = b.Right }).ToList();

                if (TryMove(robot, bearing, walls, clone))
                {
                    boxes = clone;
                    robot = robot.Move(bearing);
                }

                Print(instruction, width, height, robot, walls, boxes, Part.Two);
            }

            return boxes.Select(b => b.Left.Y * 100 + b.Left.X).Sum();
        }

        /// <summary>
        /// Try to move the robot along the given bearing, and if that causes any boxes to need to
        /// move then move those also, which may cause a cascade of moves
        /// </summary>
        /// <param name="robot">Robot starting position</param>
        /// <param name="bearing">Move bearing</param>
        /// <param name="walls">Wall positions</param>
        /// <param name="boxes">Boxes, which will be modified in place</param>
        /// <returns>Whether the move operation succeeded. If not, the boxes should be discarded as it may be invalid</returns>
        private static bool TryMove(Point2D robot, Bearing bearing, HashSet<Point2D> walls, List<Box> boxes)
        {
            Queue<Box> queue = new Queue<Box>();
            HashSet<Box> moved = [];

            // initial move of the robot
            queue.Enqueue(new Box { Left = robot, Right = robot });

            while (queue.Any())
            {
                Box moving = queue.Dequeue();

                if (!moved.Add(moving))
                {
                    // this box was already moved by another one
                    continue;
                }

                moving.Move(bearing);

                if (walls.Contains(moving.Left) || walls.Contains(moving.Right))
                {
                    // tried to move something into a wall - this instruction isn't possible
                    return false;
                }

                foreach (Box next in boxes.Where(b => b.Overlaps(moving)))
                {
                    // queue anything which now overlaps with the thing that moved
                    queue.Enqueue(next);
                }
            }

            // managed to move everything that needed to move without hitting a wall
            return true;
        }

        /// <summary>
        /// Print the state of the grid
        /// </summary>
        /// <param name="move">Current move instruction</param>
        /// <param name="width">Original grid width</param>
        /// <param name="height">Grid height</param>
        /// <param name="robot">Robot position</param>
        /// <param name="walls">Wall positions</param>
        /// <param name="boxes">Box positions</param>
        /// <param name="part">Puzzle part - note that part two makes the grid twice as wide</param>
        private static void Print(char move, int width, int height, Point2D robot, HashSet<Point2D> walls, List<Box> boxes, Part part = Part.One)
        {
            if (!Debugger.IsAttached)
            {
                return;
            }

            if (part == Part.Two)
            {
                width *= 2;
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
                    else if (boxes.Any(b => b.Left == point))
                    {
                        s.Append(part == Part.One ? 'O' : '[');
                    }
                    else if (part == Part.Two && boxes.Any(b => b.Right == point))
                    {
                        s.Append(']');
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

        /// <summary>
        /// A box, which is a mutable wrapper around two immutable points
        /// </summary>
        /// <remarks>
        /// In part one, boxes are one wide so left and right are equal. In part two boxes are two units wide
        /// </remarks>
        private class Box : IEquatable<Box>
        {
            /// <summary>
            /// Left side of the box
            /// </summary>
            public Point2D Left { get; set; }

            /// <summary>
            /// Right side of the box
            /// </summary>
            public Point2D Right { get; set; }

            /// <summary>
            /// Move the box
            /// </summary>
            /// <param name="bearing">Move direction</param>
            public void Move(Bearing bearing)
            {
                this.Left = this.Left.Move(bearing);
                this.Right = this.Right.Move(bearing);
            }

            /// <summary>
            /// Check if this box overlaps with the other box
            /// </summary>
            /// <param name="other">Other box</param>
            /// <returns>Boxes overlap</returns>
            public bool Overlaps(Box other) => this.Left == other.Left || this.Right == other.Left || this.Left == other.Right;

            /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>
            /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
            public bool Equals(Box other)
            {
                if (other is null)
                {
                    return false;
                }

                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                return this.Left.Equals(other.Left)
                    && this.Right.Equals(other.Right);
            }
        }
    }
}
