using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day23Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day23 solver;

        public Day23Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day23();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day23.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "kh-tc",
                "qp-kh",
                "de-cg",
                "ka-co",
                "yn-aq",
                "qp-ub",
                "cg-tb",
                "vc-aq",
                "tb-ka",
                "wh-tc",
                "yn-cg",
                "kh-ub",
                "ta-co",
                "de-co",
                "tc-td",
                "tb-wq",
                "wh-td",
                "ta-ka",
                "td-qp",
                "aq-cg",
                "wq-ub",
                "ub-vc",
                "de-ta",
                "wq-aq",
                "wq-vc",
                "wh-yn",
                "ka-de",
                "kh-ta",
                "co-tc",
                "wh-qp",
                "tb-vc",
                "td-yn",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 7;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 1599;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 23 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = "co,de,ka,ta";

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = "av,ax,dg,di,dw,fa,ge,kh,ki,ot,qw,vz,yw";

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 23 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
