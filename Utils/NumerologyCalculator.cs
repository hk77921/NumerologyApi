using System.Text.RegularExpressions;

namespace NumerologyApi.Utils
{
    public static class NumerologyCalculator
    {
        private static readonly int[][] friendlyPairs = new int[][]
        {
            new[] {1, 5}, new[] {2, 7}, new[] {3, 6}, new[] {5, 9}, new[] {1, 6}, new[] {2, 8}
        };

        private static readonly int[] gridTemplate = { 3, 1, 9, 6, 7, 5, 2, 8, 4 };

        private static readonly List<int[]> lShapePatterns = new()
        {
            new[] { 3, 1, 9, 5 }, // sample L shape 1
            new[] { 6, 7, 5, 8 }, // sample L shape 2
            new[] { 2, 8, 4, 9 }  // sample L shape 3
        };

        public static int CalculateBN(string dob)
        {
            var day = int.Parse(dob.Split('-')[2]);
            return ReduceToSingleDigit(day);
        }

        public static int CalculateDN(string dob)
        {
            var digits = Regex.Replace(dob, "[^0-9]", "");
            int sum = digits.Select(c => int.Parse(c.ToString())).Sum();
            return ReduceToSingleDigit(sum);
        }

        public static int SumOfDigits(string number)
        {
            return ReduceToSingleDigit(number.Where(char.IsDigit).Sum(c => c - '0'));
        }

        public static int ReduceToSingleDigit(int num)
        {
            while (num > 9)
            {
                num = num.ToString().Sum(c => c - '0');
            }
            return num;
        }

        public static Dictionary<int, int> CountDigits(string mobile)
        {
            var counts = new Dictionary<int, int>();
            foreach (var c in mobile)
            {
                if (char.IsDigit(c))
                {
                    var digit = c - '0';
                    if (!counts.ContainsKey(digit)) counts[digit] = 0;
                    counts[digit]++;
                }
            }
            return counts;
        }

        public static List<int[]> DetectFriendlyCombinations(Dictionary<int, int> digitCounts)
        {
            var detected = new List<int[]>();
            foreach (var pair in friendlyPairs)
            {
                if (digitCounts.ContainsKey(pair[0]) && digitCounts.ContainsKey(pair[1]))
                {
                    detected.Add(pair);
                }
            }
            return detected;
        }

        public static int FindPowerfulNumber(Dictionary<int, int> digitCounts)
        {
            if (digitCounts.Count == 0) return -1;
            return digitCounts.OrderByDescending(x => x.Value).First().Key;
        }

        public static bool CheckLShape(Dictionary<int, int> digitCounts)
        {
            var numbersPresent = digitCounts.Keys.ToHashSet();
            foreach (var pattern in lShapePatterns)
            {
                if (pattern.All(numbersPresent.Contains))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<int> GetLuckyNumbers(int bn, int dn)
        {
            var map = new Dictionary<int, List<int>>
            {
                { 1, new() { 1, 3, 5, 6 } },
                { 2, new() { 1, 3, 5, 6 } },
                { 3, new() { 1, 3, 5 } },
                { 4, new() { 1, 3, 5, 6 } },
                { 5, new() { 1, 3, 5, 6 } },
                { 6, new() { 1, 5, 6 } },
                { 7, new() { 1, 3, 5, 6 } },
                { 8, new() { 3, 5, 6 } },
                { 9, new() { 1, 3, 5, 6 } }
            };
            return map.ContainsKey(bn) && map.ContainsKey(dn)
                ? map[bn].Intersect(map[dn]).Distinct().ToList()
                : new List<int>();
        }

        public static List<int> GetUnluckyNumbers(int bn)
        {
            var map = new Dictionary<int, List<int>>
            {
                { 1, new() { 8 } },
                { 2, new() { 4, 9 } },
                { 3, new() { 6 } },
                { 4, new() { 9, 2 } },
                { 6, new() { 3 } },
                { 8, new() { 1 } },
                { 9, new() { 2, 4 } }
            };
            return map.ContainsKey(bn) ? map[bn] : new List<int>();
        }
    }
}
