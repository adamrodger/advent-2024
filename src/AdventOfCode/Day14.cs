using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 14
    /// </summary>
    public class Day14
    {
        public int Part1(string[] input, int width, int height)
        {
            ICollection<Robot> bots = Parse(input);

            for (int i = 0; i < 100; i++)
            {
                MoveBots(bots, width, height);
            }

            int q1 = 0;
            int q2 = 0;
            int q3 = 0;
            int q4 = 0;
            int midX = width / 2;
            int midY = height / 2;

            foreach (Robot bot in bots)
            {
                if (bot.Position.X < midX)
                {
                    if (bot.Position.Y < midY)
                    {
                        q1++;
                    }
                    else if (bot.Position.Y > midY)
                    {
                        q2++;
                    }
                }
                else if (bot.Position.X > midX)
                {
                    if (bot.Position.Y < midY)
                    {
                        q3++;
                    }
                    else if (bot.Position.Y > midY)
                    {
                        q4++;
                    }
                }
            }

            return q1 * q2 * q3 * q4;
        }

        public int Part2(string[] input, int width, int height)
        {
            ICollection<Robot> bots = Parse(input);

            int i = 0;

            while (true)
            {
                i++;

                MoveBots(bots, width, height);

                var positions = bots.Select(b => b.Position).ToHashSet();

                // there's a 32x34 'frame' around the image with the top corner at 40,47 which I found empirically the first time :D
                if (positions.Count(p => p.X == 40) == 34 && positions.Count(p => p.Y == 47) == 32)
                {
                    Print(i, positions, width, height);
                    return i;
                }
            }
        }

        /// <summary>
        /// Parse the input
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Parsed robots</returns>
        private static ICollection<Robot> Parse(string[] input) => input.Select(line => line.Numbers<int>())
                                                                        .Select(n => new Robot
                                                                         {
                                                                             Position = (n[0], n[1]),
                                                                             Vector = (n[2], n[3])
                                                                         })
                                                                        .ToArray();

        /// <summary>
        /// Move all the bots - this mutates the given collection in-place
        /// </summary>
        /// <param name="bots">Bots</param>
        /// <param name="width">Grid width</param>
        /// <param name="height">Grid height</param>
        private static void MoveBots(ICollection<Robot> bots, int width, int height)
        {
            foreach (Robot bot in bots)
            {
                bot.Position = ((bot.Position.X + bot.Vector.X + width) % width,
                                (bot.Position.Y + bot.Vector.Y + height) % height);
            }
        }

        /// <summary>
        /// Print the grid and indicate where all the bots are
        /// </summary>
        /// <param name="i">Number of moves to get to this point</param>
        /// <param name="bots">Bot positions</param>
        /// <param name="width">Grid width</param>
        /// <param name="height">Grid height</param>
        private static void Print(int i, ISet<Point2D> bots, int width, int height)
        {
            if (!Debugger.IsAttached)
            {
                return;
            }

            StringBuilder s = new StringBuilder(width * height + height * Environment.NewLine.Length);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    s.Append(bots.Contains((x, y)) ? '#' : '.');
                }

                s.AppendLine();
            }

            Debug.WriteLine($"Step {i}");
            Debug.WriteLine(s.ToString());
        }

        private class Robot
        {
            public Point2D Position { get; set; }
            public Point2D Vector { get; init; }
        } 
    }
}
