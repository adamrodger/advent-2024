using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Utilities
{
    public readonly record struct Point2D(int X, int Y)
    {
        public static implicit operator (int x, int y)(Point2D point)
        {
            return (point.X, point.Y);
        }

        public static implicit operator Point2D((int x, int y) coordinates)
        {
            return new Point2D(coordinates.x, coordinates.y);
        }

        public static implicit operator Point2D(Point3D point)
        {
            return new Point2D(point.X, point.Y);
        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            return new Point2D(a.X + b.X, a.Y + b.Y);
        }

        public static Point2D operator -(Point2D a, Point2D b)
        {
            return new Point2D(a.X - b.X, a.Y - b.Y);
        }

        public IEnumerable<Point2D> Adjacent4()
        {
            yield return new Point2D(this.X, this.Y - 1);
            yield return new Point2D(this.X - 1, this.Y);
            yield return new Point2D(this.X + 1, this.Y);
            yield return new Point2D(this.X, this.Y + 1);
        }

        public IEnumerable<Point2D> Adjacent8()
        {
            yield return new Point2D(this.X - 1, this.Y - 1);
            yield return new Point2D(this.X, this.Y - 1);
            yield return new Point2D(this.X + 1, this.Y - 1);
            yield return new Point2D(this.X - 1, this.Y);
            yield return new Point2D(this.X + 1, this.Y);
            yield return new Point2D(this.X - 1, this.Y + 1);
            yield return new Point2D(this.X, this.Y + 1);
            yield return new Point2D(this.X + 1, this.Y + 1);
        }

        /// <summary>
        /// Move from this position to another in the given direction and number of steps
        /// </summary>
        /// <param name="bearing">Move direction</param>
        /// <param name="steps">Move steps</param>
        /// <returns>New position</returns>
        public Point2D Move(Bearing bearing, int steps = 1) => bearing switch
        {
            Bearing.North => (this.X, this.Y - steps),
            Bearing.South => (this.X, this.Y + steps),
            Bearing.East => (this.X + steps, this.Y),
            Bearing.West => (this.X - steps, this.Y),
            Bearing.NorthEast => (this.X + steps, this.Y - steps),
            Bearing.NorthWest => (this.X - steps, this.Y - steps),
            Bearing.SouthEast => (this.X + steps, this.Y + steps),
            Bearing.SouthWest => (this.X - steps, this.Y + steps),
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Get the number of steps it would take to get from this point to the other on a 2D grid
        /// </summary>
        /// <param name="other">Other point</param>
        /// <returns>Distance in steps</returns>
        public int ManhattanDistance(Point2D other) => Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);

        /// <summary>
        /// Make sure the current point is in bounds of the grid
        /// </summary>
        /// <typeparam name="T">Grid type</typeparam>
        /// <param name="grid">Grid</param>
        /// <returns>Point is still in bounds</returns>
        public bool InBounds<T>(T[,] grid) => this.X >= 0 && this.X < grid.GetLength(1) && this.Y >= 0 && this.Y < grid.GetLength(0);

        /// <summary>
        /// Get all reachable points within the maximum Manhattan distance
        /// </summary>
        /// <param name="distance">Max distance</param>
        /// <returns>All reachable points</returns>
        public IEnumerable<Point2D> ReachablePoints(int distance)
        {
            foreach (int dy in Enumerable.Range(-distance, distance * 2 + 1))
            {
                int stepsLeft = distance - Math.Abs(dy);

                foreach (int dx in Enumerable.Range(-stepsLeft, stepsLeft * 2 + 1))
                {
                    yield return new Point2D(this.X + dx, this.Y + dy);
                }
            }
        }
    }

    public readonly record struct Point3D(int X, int Y, int Z)
    {
        public static implicit operator (int x, int y, int z)(Point3D point)
        {
            return (point.X, point.Y, point.Z);
        }

        public static implicit operator Point3D((int x, int y, int z) coordinates)
        {
            return new Point3D(coordinates.x, coordinates.y, coordinates.z);
        }

        public static implicit operator Point3D(Point2D point)
        {
            return new Point3D(point.X, point.Y, 0);
        }

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3D operator -(Point3D a, Point3D b)
        {
            return new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public IEnumerable<Point3D> Adjacent4()
        {
            yield return new Point3D(this.X, this.Y - 1, this.Z);
            yield return new Point3D(this.X - 1, this.Y, this.Z);
            yield return new Point3D(this.X + 1, this.Y, this.Z);
            yield return new Point3D(this.X, this.Y + 1, this.Z);
        }

        public IEnumerable<Point3D> Adjacent6()
        {
            yield return this with { X = this.X - 1 };
            yield return this with { X = this.X + 1 };
            yield return this with { Y = this.Y - 1 };
            yield return this with { Y = this.Y + 1 };
            yield return this with { Z = this.Z - 1 };
            yield return this with { Z = this.Z + 1 };
        }

        public int ManhattanDistance(Point3D other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y) + Math.Abs(this.Z - other.Z);
        }
    }
}
