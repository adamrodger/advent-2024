using System;

namespace AdventOfCode.Utilities
{
    /// <summary>
    /// Compass bearing
    /// </summary>
    public enum Bearing { North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest };

    /// <summary>
    /// Turn direction
    /// </summary>
    public enum TurnDirection { Left = 0, Right = 1 };

    /// <summary>
    /// Extensions methods to do with moving around a grid
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Turn in the given direction
        /// </summary>
        /// <param name="bearing">Current bearing</param>
        /// <param name="turn">Turn direction</param>
        /// <returns>New bearing</returns>
        public static Bearing Turn(this Bearing bearing, TurnDirection turn)
        {
            return bearing switch
            {
                Bearing.North => turn == TurnDirection.Left ? Bearing.West : Bearing.East,
                Bearing.South => turn == TurnDirection.Left ? Bearing.East : Bearing.West,
                Bearing.East => turn == TurnDirection.Left ? Bearing.North : Bearing.South,
                Bearing.West => turn == TurnDirection.Left ? Bearing.South : Bearing.North,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
