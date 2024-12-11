using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day11Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day11 solver;

        public Day11Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day11();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day11.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "125 17"
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 55312;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 183620;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 11 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 220377651399268;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 11 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
