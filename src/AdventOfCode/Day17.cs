using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 17
    /// </summary>
    public class Day17
    {
        public string Part1(string[] input)
        {
            long a = input[0].Numbers<long>().First();
            long b = input[1].Numbers<long>().First();
            long c = input[2].Numbers<long>().First();
            long[] instructions = input[4].Numbers<long>();

            var computer = new Computer(a, b, c, instructions);
            computer.Execute();

            // 742050537 wrong ... just forgot the commas :D
            return string.Join(',', computer.Output);
        }

        public long Part2(string[] input)
        {
            /*
            2,4,1,1,7,5,4,4,1,4,0,3,5,5,3,0

            bst 4
            bxl 1
            cdv 5
            bxc 4
            bxl 4
            adv 3
            out 5
            jnz 0
            
            A = 123456789
            while A != 0:
                B = A % 8       # B is last 3 bits of A
                B = B ^ 1       # toggle the list bit of B (add or subtract 1)
                C = A / (2**B)  # C = A >> B
                B = B ^ C       # 
                B = B ^ A
                A = A / (2**3)  # A = A >> 3  or just A / 8, discarding last 3 bits of A
                print B % 8

            Each output digit is determined by the digits of A in chunks of 3
            so we're looking for a 16*3=48 bit number where each group of 3
            bits gives the expected output digit in the correct order

            That also means there are only 8 possible inputs for each 3 bit section
            and once we've worked out which are valid they'll never change
             */

            long b = input[1].Numbers<long>().First();
            long c = input[2].Numbers<long>().First();
            long[] instructions = input[4].Numbers<long>();

            long target = instructions.Aggregate(0L, (acc, i) => acc * 10 + i);

            List<long> possibleA = [0L];
            var nextPossibleA = new List<long>();

            for (int i = 0; i < instructions.Length; i++)
            {
                long targetEnd = target % (long)Math.Pow(10, i + 1);

                foreach (long a in possibleA)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        long shifted = (a << 3) + j;

                        Computer computer = new Computer(shifted, b, c, instructions);
                        computer.Execute();

                        long actual = computer.Output.Aggregate(0L, (acc, x) => acc * 10 + x);

                        if (actual == targetEnd)
                        {
                            nextPossibleA.Add(shifted);
                        }
                    }
                }

                possibleA = nextPossibleA;
                nextPossibleA = new List<long>();
            }

            return possibleA.Min();
        }

        private class Computer(long a, long b, long c, long[] instructions)
        {
            public long Pointer { get; set; }

            public long A { get; set; } = a;

            public long B { get; set; } = b;

            public long C { get; set; } = c;

            public long[] Instructions { get; set; } = instructions;

            public List<long> Output { get; set; } = [];

            public void Execute()
            {
                while (true)
                {
                    if (this.Pointer < 0 || this.Pointer > Instructions.Length - 2)
                    {
                        return;
                    }

                    var instruction = new Instruction(this.Instructions[this.Pointer]);
                    var operand = new Operand(this.Instructions[this.Pointer + 1]);

                    InstructionResult result = instruction.Execute(this, operand);

                    if (result == InstructionResult.Advance)
                    {
                        this.Pointer += 2;
                    }
                }
            }
        }

        private enum InstructionType
        {
            Adv,
            Bxl,
            Bst,
            Jnz,
            Bxc,
            Out,
            Bdv,
            Cdv,
        }

        private enum InstructionResult
        {
            Advance,
            Jumped
        }

        private class Instruction(long code)
        {
            private readonly InstructionType type = code switch
            {
                0 => InstructionType.Adv,
                1 => InstructionType.Bxl,
                2 => InstructionType.Bst,
                3 => InstructionType.Jnz,
                4 => InstructionType.Bxc,
                5 => InstructionType.Out,
                6 => InstructionType.Bdv,
                7 => InstructionType.Cdv,
                _ => throw new ArgumentOutOfRangeException()
            };

            public InstructionResult Execute(Computer computer, Operand operand)
            {
                switch (this.type)
                {
                    case InstructionType.Adv:
                        (computer.A, _) = Math.DivRem(computer.A, (long)Math.Pow(2, operand.ReadCombo(computer)));
                        break;
                    case InstructionType.Bdv:
                        (computer.B, _) = Math.DivRem(computer.A, (long)Math.Pow(2, operand.ReadCombo(computer)));
                        break;
                    case InstructionType.Cdv:
                        (computer.C, _) = Math.DivRem(computer.A, (long)Math.Pow(2, operand.ReadCombo(computer)));
                        break;
                    case InstructionType.Bxl:
                        computer.B ^= operand.ReadLiteral();
                        break;
                    case InstructionType.Bxc:
                        computer.B ^= computer.C;
                        break;
                    case InstructionType.Bst:
                        computer.B = operand.ReadCombo(computer) % 8;
                        break;
                    case InstructionType.Jnz:
                        if (computer.A != 0)
                        {
                            computer.Pointer = operand.ReadLiteral();
                            return InstructionResult.Jumped;
                        }
                        break;
                    case InstructionType.Out:
                        computer.Output.Add(operand.ReadCombo(computer) % 8);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return InstructionResult.Advance;
            }
        }

        private class Operand(long code)
        {
            public long ReadLiteral() => code;

            public long ReadCombo(Computer computer) => code switch
            {
                < 4 => code,
                4 => computer.A,
                5 => computer.B,
                6 => computer.C,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
