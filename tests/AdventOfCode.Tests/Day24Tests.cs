using System.IO;
using System.Linq;
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
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = "cpm,ghp,gpr,krs,nks,z10,z21,z33";

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 24 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0x7FFFFFFFFFF, 0, 0x7FFFFFFFFFF)] // 42 high bits
        [InlineData(1, 2, 3)]
        [InlineData(1, 0, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(4, 0, 4)]
        [InlineData(8, 0, 8)]
        [InlineData(16, 0, 16)]
        [InlineData(32, 0, 32)]
        [InlineData(64, 0, 64)]
        [InlineData(128, 0, 128)]
        [InlineData(256, 0, 256)]
        [InlineData(512, 0, 512)]
        [InlineData(1 << 10, 0, 1 << 10)] // broken - htv,gpr,z10
        [InlineData(1 << 11, 0, 1 << 11)]
        [InlineData(1 << 12, 0, 1 << 12)]
        [InlineData(1 << 13, 0, 1 << 13)]
        [InlineData(1 << 14, 0, 1 << 14)]
        [InlineData(1 << 15, 0, 1 << 15)]
        [InlineData(1 << 16, 0, 1 << 16)]
        [InlineData(1 << 17, 0, 1 << 17)]
        [InlineData(1 << 18, 0, 1 << 18)]
        [InlineData(1 << 19, 0, 1 << 19)]
        [InlineData(1 << 20, 0, 1 << 20)]
        [InlineData(1 << 21, 0, 1 << 21)] // broken - z21,nks
        [InlineData(1 << 22, 0, 1 << 22)]
        [InlineData(1 << 23, 0, 1 << 23)]
        [InlineData(1 << 24, 0, 1 << 24)]
        [InlineData(1 << 25, 0, 1 << 25)]
        [InlineData(1 << 26, 0, 1 << 26)]
        [InlineData(1 << 27, 0, 1 << 27)]
        [InlineData(1 << 28, 0, 1 << 28)]
        [InlineData(1 << 29, 0, 1 << 29)]
        [InlineData(1 << 30, 0, 1 << 30)]
        [InlineData(1L << 31, 0, 1L << 31)]
        [InlineData(1L << 32, 0, 1L << 32)]
        [InlineData(1L << 33, 0, 1L << 33)] // broken - z33,ghp
        [InlineData(1L << 34, 0, 1L << 34)]
        [InlineData(1L << 35, 0, 1L << 35)]
        [InlineData(1L << 36, 0, 1L << 36)]
        [InlineData(1L << 37, 0, 1L << 37)]
        [InlineData(1L << 38, 0, 1L << 38)]
        [InlineData(1L << 39, 0, 1L << 39)] // broken - cpm,z39,krs
        [InlineData(1L << 40, 0, 1L << 40)]
        [InlineData(1L << 41, 0, 1L << 41)]
        [InlineData(1L << 42, 0, 1L << 42)]
        [InlineData(1L << 43, 0, 1L << 43)]
        [InlineData(1L << 44, 0, 1L << 44)]
        public void Add_WhenCalled_AddsInputs(long x, long y, long expected)
        {
            string[] input = File.ReadAllLines("inputs/day24.txt");
            SwapOutputs(input, "z10", "gpr");
            SwapOutputs(input, "z21", "nks");
            SwapOutputs(input, "z33", "ghp");
            SwapOutputs(input, "krs", "cpm");

            // cpm,ghp,htv,nks,z10,z21,z33,z39
            // cpm,ghp,gpr,krs,nks,z10,z21,z33

            long result = this.solver.Add(x, y, input);

            Assert.Equal(expected, result);

            result = this.solver.Add(y, x, input);

            Assert.Equal(expected, result);
        }

        private static void SwapOutputs(string[] input, string left, string right)
        {
            int index1 = input.Index().First(l => l.Item.EndsWith(left)).Index;
            int index2 = input.Index().First(l => l.Item.EndsWith(right)).Index;

            input[index1] = input[index1][..^3] + right;
            input[index2] = input[index2][..^3] + left;
        }
    }
}
