using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day21Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day21 solver;

        public Day21Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day21();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day21.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "029A",
                "980A",
                "179A",
                "456A",
                "379A",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 126384;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 157230;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 21 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 195969155897936;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 21 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
