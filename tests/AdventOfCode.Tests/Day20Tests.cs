using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day20Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day20 solver;

        public Day20Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day20();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day20.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "###############",
                "#...#...#.....#",
                "#.#.#.#.#.###.#",
                "#S#...#.#.#...#",
                "#######.#.#.###",
                "#######.#.#...#",
                "#######.#.###.#",
                "###..E#...#...#",
                "###.#######.###",
                "#...###...#...#",
                "#.#####.#.###.#",
                "#.#...#.#.#...#",
                "#.#.#.#.#.#.###",
                "#...#...#...###",
                "###############",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 44;

            var result = solver.Part1(GetSampleInput(), 1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 1518;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 20 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 285;

            var result = solver.Part2(GetSampleInput(), 50);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 1032257;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 20 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
