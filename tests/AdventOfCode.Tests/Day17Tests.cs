using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day17Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day17 solver;

        public Day17Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day17();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day17.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "Register A: 729",
                "Register B: 0",
                "Register C: 0",
                "",
                "Program: 0,1,5,4,3,0",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = "4,6,3,5,6,3,5,2,1,0";

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_BstCommand_Works()
        {
            var expected = "1";

            var result = solver.Part1([
                "Register A: 0",
                "Register B: 0",
                "Register C: 9",
                "",
                "Program: 2,6,5,5",
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_OutCommand_Works()
        {
            var expected = "0,1,2";

            var result = solver.Part1([
                "Register A: 10",
                "Register B: 0",
                "Register C: 0",
                "",
                "Program: 5,0,5,1,5,4",
            ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_JnzCommand_Works()
        {
            var expected = "4,2,5,6,7,7,7,7,3,1,0,0";

            var result = solver.Part1([
                "Register A: 2024",
                "Register B: 0",
                "Register C: 0",
                "",
                "Program: 0,1,5,4,3,0,5,4",
            ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_BxlCommand_Works()
        {
            var expected = "2";

            var result = solver.Part1([
                "Register A: 0",
                "Register B: 29",
                "Register C: 0",
                "",
                "Program: 1,7,5,5",
            ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_BxcCommand_Works()
        {
            var expected = "2";

            var result = solver.Part1([
                "Register A: 0",
                "Register B: 2024",
                "Register C: 43690",
                "",
                "Program: 4,0,5,5",
            ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = "7,4,2,0,5,0,5,3,7";

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 17 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 117440;

            var result = solver.Part2([
                "Register A: 2024",
                "Register B: 0",
                "Register C: 0",
                "",
                "Program: 0,3,5,4,3,0",
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 202991746427434;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 17 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
