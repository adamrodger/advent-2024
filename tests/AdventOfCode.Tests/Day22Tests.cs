using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day22Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day22 solver;

        public Day22Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day22();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day22.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "1",
                "10",
                "100",
                "2024",

                //"123"
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 37327623;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 14392541715;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 22 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 1628;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 22 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
