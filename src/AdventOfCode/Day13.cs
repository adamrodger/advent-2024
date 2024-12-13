using System;
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

            for (int i = 0; i < input.Length; i += 4)
            {
                var a = input[i].Numbers<int>();
                var b = input[i + 1].Numbers<int>();
                var p = input[i + 2].Numbers<int>();

                cost += MinCost(a[0], a[1], b[0], b[1], p[0], p[1]).ValueOr(0);
            }

            // 36126 low
            return cost;
        }

        public long Part2(string[] input)
        {
            long cost = 0;

            for (int i = 0; i < input.Length; i += 4)
            {
                var a = input[i].Numbers<int>();
                var b = input[i + 1].Numbers<int>();
                var p = input[i + 2].Numbers<long>();

                cost += MinCost2(a[0], a[1], b[0], b[1], p[0] + 10000000000000, p[1] + 10000000000000).ValueOr(0);
            }

            return cost;
        }

        private static Option<int> MinCost(int ax, int ay, int bx, int by, int px, int py)
        {
            int min = int.MaxValue;

            int maxA = Math.Min(px / ax, py / ay) + 1;
            int maxB = Math.Min(px / bx, py / by) + 1;

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

        private static Option<long> MinCost2(int ax, int ay, int bx, int by, long px, long py)
        {
            long a = (px * by - py * bx) / (ax * by - ay * bx);
            long b = (ax * py - ay * px) / (ax * by - ay * bx);

            if (a * ax + b * bx == px && a * ay + b * by == py)
            {
                return Option.Some(3 * a + b);
            }

            return Option.None<long>();
        }
    }
}
