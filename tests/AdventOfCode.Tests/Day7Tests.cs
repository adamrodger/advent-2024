using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day7Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day7 solver;

        public Day7Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day7();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day7.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "190: 10 19",
                "3267: 81 40 27",
                "83: 17 5",
                "156: 15 6",
                "7290: 6 8 6 15",
                "161011: 16 10 13",
                "192: 17 8 14",
                "21037: 9 7 18 13",
                "292: 11 6 16 20",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 3749;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 5837374519342;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 7 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 11387;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 492383931650959;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 7 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
