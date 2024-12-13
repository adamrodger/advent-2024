using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day13Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day13 solver;

        public Day13Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day13();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day13.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "Button A: X+94, Y+34",
                "Button B: X+22, Y+67",
                "Prize: X=8400, Y=5400",
                "",
                "Button A: X+26, Y+66",
                "Button B: X+67, Y+21",
                "Prize: X=12748, Y=12176",
                "",
                "Button A: X+69, Y+23",
                "Button B: X+27, Y+71",
                "Prize: X=18641, Y=10279",
                "",
                "Button A: X+17, Y+86",
                "Button B: X+84, Y+37",
                "Prize: X=7870, Y=6450",
                "",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 480;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 36571;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 13 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 85527711500010;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 13 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
