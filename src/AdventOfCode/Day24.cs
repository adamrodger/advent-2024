using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 24
    /// </summary>
    public class Day24
    {
        public long Part1(string[] input)
        {
            (Dictionary<string, bool> state, List<Rule> rules) = Parse(input);
            
            // 1604176604 low
            // 23079013084 low .... use longs dummy
            return Check(state, rules);
        }

        public string Part2(string[] input)
        {
            // did this by hand - see unit tests
            return "cpm,ghp,gpr,krs,nks,z10,z21,z33";
        }

        /// <summary>
        /// For part 2 - set x and y state to the given values and run the
        /// rules (which may have been modified from original)
        /// </summary>
        /// <param name="x">Initial X</param>
        /// <param name="y">Initial Y</param>
        /// <param name="input">Input</param>
        /// <returns>Resulting Z</returns>
        public long Add(long x, long y, string[] input)
        {
            (Dictionary<string, bool> state, List<Rule> rules) = Parse(input);

            for (int i = 0; i < 45; i++)
            {
                (x, long rem) = Math.DivRem(x, 2);
                state[$"x{i:D2}"] = rem == 1;

                (y, rem) = Math.DivRem(y, 2);
                state[$"y{i:D2}"] = rem == 1;
            }

            long result = Check(state, rules);
            return result;
        }

        /// <summary>
        /// Parse the input
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Parsed initial state and rules</returns>
        private static (Dictionary<string, bool> State, List<Rule> Rules) Parse(string[] input)
        {
            Dictionary<string, bool> state = input.TakeWhile(l => !string.IsNullOrEmpty(l))
                                                  .Select(line => line.Split(": "))
                                                  .ToDictionary(a => a[0], a => a[1][0] == '1');

            var rules = new List<Rule>();

            foreach (string line in input.Skip(state.Count + 1))
            {
                string[] parts = line.Split(' ');

                var type = Enum.Parse<RuleType>(parts[1], true);

                rules.Add(new Rule(parts[0], parts[2], type, parts[4]));
            }

            return (state, rules);
        }

        /// <summary>
        /// Run the rules against the initial state
        /// </summary>
        /// <param name="state">State</param>
        /// <param name="rules">Rules</param>
        /// <returns>Resulting number in Z positions</returns>
        private static long Check(Dictionary<string, bool> state, List<Rule> rules)
        {
            // this is topological sort, but I can't remember that and it's small enough to brute force

            while (rules.Any())
            {
                foreach (Rule rule in rules.ToArray())
                {
                    if (state.TryGetValue(rule.Left, out bool left) && state.TryGetValue(rule.Right, out bool right))
                    {
                        state[rule.Output] = rule.Type switch
                        {
                            RuleType.And => left && right,
                            RuleType.Or => left || right,
                            RuleType.Xor => left ^ right,
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        rules.Remove(rule);
                    }
                }
            }

            long result = 0;

            foreach ((int i, string key) in state.Keys.Where(k => k.StartsWith('z')).Order().Enumerate())
            {
                if (state[key])
                {
                    result += (long)Math.Pow(2, i);
                }
            }


            return result;
        }

        private enum RuleType
        {
            And,
            Or,
            Xor
        }

        private record Rule(string Left, string Right, RuleType Type, string Output);
    }
}
