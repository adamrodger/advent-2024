using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            return input.Sum(i => int.Parse(i[..^1]) * FewestPresses(i));
        }

        public int Part2(string[] input)
        {
            foreach (string line in input)
            {
                throw new NotImplementedException("Part 2 not implemented");
            }

            return 0;
        }

        private int FewestPresses(ReadOnlySpan<char> sequence)
        {
            char numpad = 'A';
            char robot1 = 'A';
            char robot2 = 'A';
            char me = 'A';
            StringBuilder s = new StringBuilder();
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            StringBuilder s3 = new StringBuilder();
            StringBuilder s4 = new StringBuilder();

            foreach (char c in sequence)
            {
                foreach (char n1 in NumpadSequence(numpad, c).Append('A'))
                {
                    foreach (char n2 in DirectionPadSequence(robot1, n1).Append('A'))
                    {
                        foreach (char n3 in DirectionPadSequence(robot2, n2).Append('A'))
                        {
                            s1.Append(n3);
                            me = n3;
                        }

                        s2.Append(n2);
                        robot2 = n2;
                    }

                    s3.Append(n1);
                    robot1 = n1;
                }

                s4.Append(c);
                numpad = c;
            }

            Debug.WriteLine(s1.ToString());
            Debug.WriteLine(s2.ToString());
            Debug.WriteLine(s3.ToString());
            Debug.WriteLine(s4.ToString());
            
            return s1.Length;
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
        private string NumpadSequence(char start, char end) => (start, end) switch
        {
            ('A', 'A') => "",
            ('A', '0') => "<",
            ('A', '1') => "^<<",
            ('A', '2') => "<^",
            ('A', '3') => "^",
            ('A', '4') => "^^<<",
            ('A', '5') => "<^^",
            ('A', '6') => "^^",
            ('A', '7') => "^^^<<",
            ('A', '8') => "<^^^",
            ('A', '9') => "^^^",

            ('0', 'A') => ">",
            ('0', '0') => "",
            ('0', '1') => "^<",
            ('0', '2') => "^",
            ('0', '3') => "^>",
            ('0', '4') => "^^<",
            ('0', '5') => "^^",
            ('0', '6') => "^^>",
            ('0', '7') => "^^^<",
            ('0', '8') => "^^^",
            ('0', '9') => ">^^^",

            ('1', 'A') => ">>v",
            ('1', '0') => ">v",
            ('1', '1') => "",
            ('1', '2') => ">",
            ('1', '3') => ">>",
            ('1', '4') => "^",
            ('1', '5') => ">^",
            ('1', '6') => ">>^",
            ('1', '7') => "^^",
            ('1', '8') => ">^^",
            ('1', '9') => ">>^^",

            ('2', 'A') => "v>",
            ('2', '0') => "v",
            ('2', '1') => "<",
            ('2', '2') => "",
            ('2', '3') => ">",
            ('2', '4') => "<^",
            ('2', '5') => "^",
            ('2', '6') => ">^",
            ('2', '7') => "<^^",
            ('2', '8') => "^^",
            ('2', '9') => ">^^",

            ('3', 'A') => "v",
            ('3', '0') => "<v",
            ('3', '1') => "<<",
            ('3', '2') => "<",
            ('3', '3') => "",
            ('3', '4') => "<<^",
            ('3', '5') => "<^",
            ('3', '6') => "^",
            ('3', '7') => "<<^^",
            ('3', '8') => "<^^",
            ('3', '9') => "^^",

            ('4', 'A') => ">>vv",
            ('4', '0') => ">vv",
            ('4', '1') => "v",
            ('4', '2') => "v>",
            ('4', '3') => "v>>",
            ('4', '4') => "",
            ('4', '5') => ">",
            ('4', '6') => ">>",
            ('4', '7') => "^",
            ('4', '8') => ">^",
            ('4', '9') => ">>^",

            ('5', 'A') => "vv>",
            ('5', '0') => "vv",
            ('5', '1') => "<v",
            ('5', '2') => "v",
            ('5', '3') => "v>",
            ('5', '4') => "<",
            ('5', '5') => "",
            ('5', '6') => ">",
            ('5', '7') => "<^",
            ('5', '8') => "^",
            ('5', '9') => "^>",

            ('6', 'A') => "vv",
            ('6', '0') => "<vv",
            ('6', '1') => "<<v",
            ('6', '2') => "<v",
            ('6', '3') => "v",
            ('6', '4') => "<<",
            ('6', '5') => "<",
            ('6', '6') => "",
            ('6', '7') => "<<^",
            ('6', '8') => "<^",
            ('6', '9') => "^",

            ('7', 'A') => ">>vvv",
            ('7', '0') => ">vvv",
            ('7', '1') => "vv",
            ('7', '2') => "vv>",
            ('7', '3') => "vv>>",
            ('7', '4') => "v",
            ('7', '5') => "v>",
            ('7', '6') => "v>>",
            ('7', '7') => "",
            ('7', '8') => ">",
            ('7', '9') => ">>",

            ('8', 'A') => "vvv>",
            ('8', '0') => "vvv",
            ('8', '1') => "<vv",
            ('8', '2') => "vv",
            ('8', '3') => "vv>",
            ('8', '4') => "<v",
            ('8', '5') => "v",
            ('8', '6') => "v>",
            ('8', '7') => "<",
            ('8', '8') => "",
            ('8', '9') => ">",

            ('9', 'A') => "vvv",
            ('9', '0') => "<vvv",
            ('9', '1') => "<<vv",
            ('9', '2') => "<vv",
            ('9', '3') => "vv",
            ('9', '4') => "<<v",
            ('9', '5') => "<v",
            ('9', '6') => "v",
            ('9', '7') => "<<",
            ('9', '8') => "<",
            ('9', '9') => "",

            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        ///     +---+---+
        ///     | ^ | A |
        /// +---+---+---+
        /// | < | v | > |
        /// +---+---+---+
        /// </summary>
        private string DirectionPadSequence(char start, char end) => (start, end) switch
        {
            ('A', '<') => "v<<",
            ('A', 'v') => "v<",
            ('A', '>') => "v",
            ('A', '^') => "<",
            ('A', 'A') => "",

            ('^', '<') => "v<",
            ('^', 'v') => "v",
            ('^', '>') => "v>",
            ('^', '^') => "",
            ('^', 'A') => ">",

            ('v', '<') => "<",
            ('v', 'v') => "",
            ('v', '>') => ">",
            ('v', '^') => "^",
            ('v', 'A') => "^>",

            ('<', '<') => "",
            ('<', 'v') => ">",
            ('<', '>') => ">>",
            ('<', '^') => ">^",
            ('<', 'A') => ">>^",

            ('>', '<') => "<<",
            ('>', 'v') => "<",
            ('>', '>') => "",
            ('>', '^') => "^<",
            ('>', 'A') => "^",

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
