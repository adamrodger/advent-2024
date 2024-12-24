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
            // this is topological sort, but I can't remember that and it's small enough to brute force

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

            // 1604176604 low
            // 23079013084 low .... use longs dummy
            return result;
        }

        public int Part2(string[] input)
        {
            foreach (string line in input)
            {
                throw new NotImplementedException("Part 2 not implemented");
            }

            return 0;
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
