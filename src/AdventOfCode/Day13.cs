using System;
using System.Linq;
using AdventOfCode.Utilities;
using Optional;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 13
    /// </summary>
    public class Day13
    {
        public int Part1(string[] input)
        {
            int cost = 0;

            foreach (string[] chunk in input.Chunk(4))
            {
                var a = chunk[0].Numbers<int>();
                var b = chunk[1].Numbers<int>();
                var p = chunk[2].Numbers<int>();

                cost += MinCost(a[0], a[1], b[0], b[1], p[0], p[1]).ValueOr(0);
            }

            // 36126 low
            return cost;
        }

        public int Part2(string[] input)
        {
            foreach (string line in input)
            {
                throw new NotImplementedException("Part 2 not implemented");
            }

            return 0;
        }

        private static Option<int> MinCost(int ax, int ay, int bx, int by, int px, int py)
        {
            int min = int.MaxValue;

            int maxA = Math.Min(px / ax, py / ay);
            int maxB = Math.Min(px / bx, py / by);

            for (int a = 0; a < maxA; a++)
            {
                int rx = px - (a * ax);
                int ry = py - (a * ay);

                (int qbx, int rbx) = Math.DivRem(rx, bx);
                (int qby, int rby) = Math.DivRem(ry, by);

                if (rbx == 0 && rby == 0 && qbx == qby && qbx < 100 && qby < 100)
                {
                    // possible
                    int cost = (a * 3) + qbx;
                    min = Math.Min(min, cost);
                }
            }

            return min.SomeWhen(m => m != int.MaxValue);
        }
    }
}
