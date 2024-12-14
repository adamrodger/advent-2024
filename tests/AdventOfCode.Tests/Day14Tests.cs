using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day14Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day14 solver;

        public Day14Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day14();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day14.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "p=0,4 v=3,-3",
                "p=6,3 v=-1,-3",
                "p=10,3 v=-1,2",
                "p=2,0 v=2,-1",
                "p=0,0 v=1,3",
                "p=3,0 v=-2,-2",
                "p=7,6 v=-1,-3",
                "p=3,0 v=-1,-2",
                "p=9,3 v=2,3",
                "p=7,3 v=-1,2",
                "p=2,4 v=2,-3",
                "p=9,5 v=-3,-3",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 12;

            var result = solver.Part1(GetSampleInput(), 11, 7);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 211692000;

            var result = solver.Part1(GetRealInput(), 101, 103);
            output.WriteLine($"Day 14 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 6587;

            var result = solver.Part2(GetRealInput(), 101, 103);
            output.WriteLine($"Day 14 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
