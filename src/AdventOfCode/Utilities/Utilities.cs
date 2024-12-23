using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Utilities
{
    public static partial class Utilities
    {
        [GeneratedRegex("-?\\d+")]
        private static partial Regex NumberRegex();

        public static T[] Numbers<T>(this string input)
        {
            MatchCollection matches = NumberRegex().Matches(input);

            return matches
                  .Select(m => m.Value)
                  .Select(m => (T)Convert.ChangeType(m, typeof(T)))
                  .ToArray();
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key)
        {
            @this.TryGetValue(key, out TValue value);
            return value;
        }

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) where TValue : new()
        {
            if (!@this.TryGetValue(key, out TValue value))
            {
                value = new TValue();
                @this.Add(key, value);
            }

            return value;
        }

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> factory)
        {
            if (!@this.TryGetValue(key, out TValue value))
            {
                value = factory();
                @this.Add(key, value);
            }

            return value;
        }

        public static IEnumerable<(int i, T item)> Enumerate<T>(this IEnumerable<T> @this, int start = 0)
        {
            int i = start;

            foreach (T item in @this)
            {
                yield return (i++, item);
            }
        }

        public static IEnumerable<T> Cycle<T>(this ICollection<T> @this)
        {
            if (@this.Count == 0)
            {
                yield break;
            }

            while (true)
            {
                foreach (T item in @this)
                {
                    yield return item;
                }
            }
        }
    }
}
