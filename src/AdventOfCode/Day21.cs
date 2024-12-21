using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 21
    /// </summary>
    public class Day21
    {
        public int Part1(string[] input)
        {
            // 32811 low
            // 163246 high
            // 159558 high
            return input.Sum(i => int.Parse(i[..^1]) * (int)FewestPresses(i, 3));
        }

        public long Part2(string[] input)
        {
            // 361374212849220 high
            // 196166290013580 high
            return input.Sum(i => int.Parse(i[..^1]) * FewestPresses(i, 26));
        }

        /// <summary>
        /// Count the fewest number of key presses required to trigger the given sequence of presses
        /// </summary>
        /// <param name="sequence">Sequence of key presses to check</param>
        /// <param name="totalRobots">Total number of robots between you and the numeric pad</param>
        /// <returns>Number of key presses required</returns>
        private static long FewestPresses(string sequence, int totalRobots)
            => Presses(sequence, 0, totalRobots, new Dictionary<(string, int), long>());

        /// <summary>
        /// Count the fewest number of key presses required to trigger the given sequence of presses
        /// </summary>
        /// <param name="sequence">Sequence of key presses to check</param>
        /// <param name="depth">Current depth, where depth 0 is the numeric pad and every depth after that is a directional pad</param>
        /// <param name="maxDepth">Maximum depth, which is number of intermediate robots, plus one for the human and one for the numeric pad</param>
        /// <param name="cache">Cache</param>
        /// <returns>Number of key presses required</returns>
        private static long Presses(string sequence, int depth, int maxDepth, Dictionary<(string, int), long> cache)
        {
            if (depth == maxDepth)
            {
                return sequence.Length;
            }

            if (cache.TryGetValue((sequence, depth), out long value))
            {
                return value;
            }

            value = ('A' + sequence).Zip(sequence)
                                    .Select(pair => depth == 0
                                                        ? NumpadSequences(pair.First, pair.Second)
                                                        : DirectionPadSequences(pair.First, pair.Second))
                                    .Sum(possible => possible.Min(next => Presses(next + 'A', depth + 1, maxDepth, cache)));
            cache[(sequence, depth)] = value;
            return value;
        }

        /// <summary>
        /// +---+---+---+
        /// | 7 | 8 | 9 |
        /// +---+---+---+
        /// | 4 | 5 | 6 |
        /// +---+---+---+
        /// | 1 | 2 | 3 |
        /// +---+---+---+
        ///     | 0 | A |
        ///     +---+---+
        /// </summary>
        /// <remarks>
        /// Observed that to be most efficient, you must:
        /// 
        /// - move left before up
        /// - move up before right
        /// - move right before down (except 2 -> A which is the opposite for some reason)
        /// - move down before left
        ///
        /// You must always take the maximum number of steps in a direction, never zigzagging
        /// 
        /// Sometimes it's not possible to follow those rules, because you'd have to pass through the disallowed
        /// bottom left square, so then we flip the order of horizontal and vertical moves, e.g. A -> 1
        ///
        /// I'm leaving all the possibilities in because it's too hard to prove/debug the above. It could be the case
        /// that my input and the sample inputs just don't exercise some of these, and those may be exceptions like
        /// the 2 -> A exception that my real input definitely needs.
        /// </remarks>
        private static string[] NumpadSequences(char start, char end) => (start, end) switch
        {
            ('A', 'A') => [""],
            ('A', '0') => ["<"],
            ('A', '1') => ["^<<"],
            ('A', '2') => ["<^'", "^<"],
            ('A', '3') => ["^"],
            ('A', '4') => ["^^<<"],
            ('A', '5') => ["<^^", "^^<"],
            ('A', '6') => ["^^"],
            ('A', '7') => ["^^^<<"],
            ('A', '8') => ["<^^^", "^^^<"],
            ('A', '9') => ["^^^"],

            ('0', 'A') => [">"],
            ('0', '0') => [""],
            ('0', '1') => ["^<"],
            ('0', '2') => ["^"],
            ('0', '3') => [">^", "^>"],
            ('0', '4') => ["^^<"],
            ('0', '5') => ["^^"],
            ('0', '6') => [">^^", "^^>"],
            ('0', '7') => ["^^^<"],
            ('0', '8') => ["^^^"],
            ('0', '9') => [">^^^", "^^^>"],

            ('1', 'A') => [">>v"],
            ('1', '0') => [">v"],
            ('1', '1') => [""],
            ('1', '2') => [">"],
            ('1', '3') => [">>"],
            ('1', '4') => ["^"],
            ('1', '5') => [">^", "^>"],
            ('1', '6') => [">>^", "^>>"],
            ('1', '7') => ["^^"],
            ('1', '8') => [">^^", "^^>"],
            ('1', '9') => [">>^^", "^^>>"],

            ('2', 'A') => [">v", "v>"],
            ('2', '0') => ["v"],
            ('2', '1') => ["<"],
            ('2', '2') => [""],
            ('2', '3') => [">"],
            ('2', '4') => ["<^", "^<"],
            ('2', '5') => ["^"],
            ('2', '6') => [">^", "^>"],
            ('2', '7') => ["<^^", "^^<"],
            ('2', '8') => ["^^"],
            ('2', '9') => [">^^", "^^>"],

            ('3', 'A') => ["v"],
            ('3', '0') => ["<v", "v<"],
            ('3', '1') => ["<<"],
            ('3', '2') => ["<"],
            ('3', '3') => [""],
            ('3', '4') => ["<<^", "^<<"],
            ('3', '5') => ["<^", "^<"],
            ('3', '6') => ["^"],
            ('3', '7') => ["<<^^", "^^<<"],
            ('3', '8') => ["<^^", "^^<"],
            ('3', '9') => ["^^"],

            ('4', 'A') => [">>vv"],
            ('4', '0') => [">vv"],
            ('4', '1') => ["v"],
            ('4', '2') => [">v", "v>"],
            ('4', '3') => [">>v", "v>>"],
            ('4', '4') => [""],
            ('4', '5') => [">"],
            ('4', '6') => [">>"],
            ('4', '7') => ["^"],
            ('4', '8') => [">^", "^>"],
            ('4', '9') => [">>^", "^>>"],

            ('5', 'A') => [">vv", "vv>"],
            ('5', '0') => ["vv"],
            ('5', '1') => ["<v", "v<"],
            ('5', '2') => ["v"],
            ('5', '3') => [">v", "v>"],
            ('5', '4') => ["<"],
            ('5', '5') => [""],
            ('5', '6') => [">"],
            ('5', '7') => ["<^", "^<"],
            ('5', '8') => ["^"],
            ('5', '9') => [">^", "^>"],

            ('6', 'A') => ["vv"],
            ('6', '0') => ["<vv", "vv<"],
            ('6', '1') => ["<<v", "v<<"],
            ('6', '2') => ["<v", "v<"],
            ('6', '3') => ["v"],
            ('6', '4') => ["<<"],
            ('6', '5') => ["<"],
            ('6', '6') => [""],
            ('6', '7') => ["<<^", "^<<"],
            ('6', '8') => ["<^", "^<"],
            ('6', '9') => ["^"],

            ('7', 'A') => [">>vvv"],
            ('7', '0') => [">vvv"],
            ('7', '1') => ["vv"],
            ('7', '2') => [">vv", "vv>"],
            ('7', '3') => [">>vv", "vv>>"],
            ('7', '4') => ["v"],
            ('7', '5') => [">v", "v>"],
            ('7', '6') => [">>v", "v>>"],
            ('7', '7') => [""],
            ('7', '8') => [">"],
            ('7', '9') => [">>"],

            ('8', 'A') => [">vvv", "vvv>"],
            ('8', '0') => ["vvv"],
            ('8', '1') => ["<vv", "vv<"],
            ('8', '2') => ["vv"],
            ('8', '3') => [">vv", "vv>"],
            ('8', '4') => ["<v", "v<"],
            ('8', '5') => ["v"],
            ('8', '6') => [">v", "v>"],
            ('8', '7') => ["<"],
            ('8', '8') => [""],
            ('8', '9') => [">"],

            ('9', 'A') => ["vvv"],
            ('9', '0') => ["<vvv", "vvv<"],
            ('9', '1') => ["<<vv", "vv<<"],
            ('9', '2') => ["<vv", "vv<"],
            ('9', '3') => ["vv"],
            ('9', '4') => ["<<v", "v<<"],
            ('9', '5') => ["<v", "v<"],
            ('9', '6') => ["v"],
            ('9', '7') => ["<<"],
            ('9', '8') => ["<"],
            ('9', '9') => [""],

            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        ///     +---+---+
        ///     | ^ | A |
        /// +---+---+---+
        /// | < | v | > |
        /// +---+---+---+
        /// </summary>
        private static string[] DirectionPadSequences(char start, char end) => (start, end) switch
        {
            ('A', '<') => ["v<<"],
            ('A', 'v') => ["<v", "v<"],
            ('A', '>') => ["v"],
            ('A', '^') => ["<"],
            ('A', 'A') => [""],

            ('^', '<') => ["v<"],
            ('^', 'v') => ["v"],
            ('^', '>') => [">v", "v>"],
            ('^', '^') => [""],
            ('^', 'A') => [">"],

            ('v', '<') => ["<"],
            ('v', 'v') => [""],
            ('v', '>') => [">"],
            ('v', '^') => ["^"],
            ('v', 'A') => [">^", "^>"],

            ('<', '<') => [""],
            ('<', 'v') => [">"],
            ('<', '>') => [">>"],
            ('<', '^') => [">^"],
            ('<', 'A') => [">>^"],

            ('>', '<') => ["<<"],
            ('>', 'v') => ["<"],
            ('>', '>') => [""],
            ('>', '^') => ["<^", "^<"],
            ('>', 'A') => ["^"],

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
