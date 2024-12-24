using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day24Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day24 solver;

        public Day24Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day24();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day24.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return new string[]
            {
                "x00: 1",
                "x01: 0",
                "x02: 1",
                "x03: 1",
                "x04: 0",
                "y00: 1",
                "y01: 1",
                "y02: 1",
                "y03: 1",
                "y04: 1",
                "",
                "ntg XOR fgs -> mjb",
                "y02 OR x01 -> tnw",
                "kwq OR kpj -> z05",
                "x00 OR x03 -> fst",
                "tgd XOR rvg -> z01",
                "vdt OR tnw -> bfw",
                "bfw AND frj -> z10",
                "ffh OR nrd -> bqk",
                "y00 AND y03 -> djm",
                "y03 OR y00 -> psh",
                "bqk OR frj -> z08",
                "tnw OR fst -> frj",
                "gnj AND tgd -> z11",
                "bfw XOR mjb -> z00",
                "x03 OR x00 -> vdt",
                "gnj AND wpb -> z02",
                "x04 AND y00 -> kjc",
                "djm OR pbm -> qhw",
                "nrd AND vdt -> hwm",
                "kjc AND fst -> rvg",
                "y04 OR y02 -> fgs",
                "y01 AND x02 -> pbm",
                "ntg OR kjc -> kwq",
                "psh XOR fgs -> tgd",
                "qhw XOR tgd -> z09",
                "pbm OR djm -> kpj",
                "x03 XOR y03 -> ffh",
                "x00 XOR y04 -> ntg",
                "bfw OR bqk -> z06",
                "nrd XOR fgs -> wpb",
                "frj XOR qhw -> z04",
                "bqk OR frj -> z07",
                "y03 OR x01 -> nrd",
                "hwm AND bqk -> z03",
                "tgd XOR rvg -> z12",
                "tnw OR pbm -> gnj",
            };
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 2024;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 51107420031718;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 24 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = -1;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = -1;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 24 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
