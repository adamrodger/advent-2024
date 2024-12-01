using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 1
    /// </summary>
    public class Day1
    {
        public int Part1(string[] input)
        {
            (List<int> left, List<int> right) = ParseInput(input);

            left.Sort();
            right.Sort();

            return left.Zip(right).Sum(pair => Math.Abs(pair.First - pair.Second));
        }

        public int Part2(string[] input)
        {
            (List<int> left, List<int> right) = ParseInput(input);

            return left.Sum(l => l * right.Count(r => r == l));
        }

        private static (List<int> Left, List<int> Right) ParseInput(ICollection<string> input)
        {
            var left = new List<int>(input.Count);
            var right = new List<int>(input.Count);

            foreach (string line in input)
            {
                int[] numbers = line.Numbers<int>();
                left.Add(numbers[0]);
                right.Add(numbers[1]);
            }

            return (left, right);
        }
    }
}
