using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day19Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day19 solver;

        public Day19Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day19();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day19.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "r, wr, b, g, bwu, rb, gb, br",
                "",
                "brwrr",
                "bggr",
                "gbbr",
                "rrbgbr",
                "ubwu",
                "bwurrg",
                "brgr",
                "bbrgwb",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 6;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 226;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 19 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 16;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 601201576113503;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 19 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
