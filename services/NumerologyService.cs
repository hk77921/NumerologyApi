using NumerologyApi.Models;
using NumerologyApi.Utils;

namespace NumerologyApi.Services
{
    public class NumerologyService : INumerologyService
    {
        public NumerologyResult Analyze(NumerologyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Dob) || request.Dob.Split('-').Length < 3)
            {
                throw new ArgumentException("Invalid DOB format received.");
            }

            if (string.IsNullOrWhiteSpace(request.Mobile))
            {
                throw new ArgumentException("Mobile number cannot be empty.");
            }

            var bn = NumerologyCalculator.CalculateBN(request.Dob);
            var dn = NumerologyCalculator.CalculateDN(request.Dob);
            var mobileTotal = NumerologyCalculator.SumOfDigits(request.Mobile);

            var luckyNumbers = NumerologyCalculator.GetLuckyNumbers(bn, dn);
            var unluckyNumbers = NumerologyCalculator.GetUnluckyNumbers(bn);

            var digitCounts = NumerologyCalculator.CountDigits(request.Mobile);
            var detectedCombinations = NumerologyCalculator.DetectFriendlyCombinations(digitCounts);
            var powerfulNumber = NumerologyCalculator.FindPowerfulNumber(digitCounts);
            var lShapeDetected = NumerologyCalculator.CheckLShape(digitCounts);

            return new NumerologyResult
            {
                Bn = bn,
                Dn = dn,
                MobileTotal = mobileTotal,
                LuckyNumbers = luckyNumbers,
                UnluckyNumbers = unluckyNumbers,
                DigitCounts = digitCounts,
                DetectedCombinations = detectedCombinations,
                PowerfulNumber = powerfulNumber,
                LShapeDetected = lShapeDetected
            };
        }


        public VedicGridResponse GetVedicGridAnalysis(string mobile)
        {
            var grid = new int[3, 3];
            foreach (var c in mobile)
            {
                int digit = int.Parse(c.ToString());
                var (row, col) = GetGridPosition(digit);
                grid[row, col]++;
            }

            var combinations = new List<string> { "3-1", "1-9", "6-7", "5-7", "2-8", "8-4", "9-5", "5-4", "2-6", "8-7", "3-6", "1-7" };
            var found = combinations.Where(c => mobile.Contains(c[0]) && mobile.Contains(c[2])).ToList();

            return new VedicGridResponse
            {
                Grid = grid,
                Combinations = combinations,
                Analysis = found,
                PatternInterpretations = InterpretGridPatterns(grid)
            };
        }

        private List<string> InterpretGridPatterns(int[,] grid)
        {
            var results = new List<string>();

            // Example: Plane 3-1-9 (row 0)
            if (grid[0, 0] > 0 && grid[0, 1] > 0 && grid[0, 2] > 0)
                results.Add("Plane 3-1-9: Educated, respected, professionally involved");

            // Plane 6-7-5 (row 1)
            if (grid[1, 0] > 0 && grid[1, 1] > 0 && grid[1, 2] > 0)
                results.Add("Plane 6-7-5: Love & romance, good speakers, businessmen");

            // Plane 2-8-4 (row 2)
            if (grid[2, 0] > 0 && grid[2, 1] > 0 && grid[2, 2] > 0)
                results.Add("Plane 2-8-4: Negative thoughts, chronic disease, addiction");

            // Diagonal 3-7-4
            if (grid[0, 0] > 0 && grid[1, 1] > 0 && grid[2, 2] > 0)
                results.Add("Diagonal 3-7-4: Obstacles in education, success after struggle");

            // L-shape 3-1 + 1-6
            if (grid[0, 0] > 0 && grid[0, 1] > 0 && grid[1, 0] > 0)
                results.Add("L-shape 3-1-6: Good education, knowledge, wisdom, top position");

            // Add more pattern rules as needed...

            return results;
        }

        private (int, int) GetGridPosition(int digit) => digit switch
        {
            1 => (0, 1),
            2 => (2, 0),
            3 => (0, 0),
            4 => (2, 2),
            5 => (1, 2),
            6 => (1, 0),
            7 => (1, 1),
            8 => (2, 1),
            9 => (0, 2),
            _ => (0, 0)
        };


        private int ReduceToSingleDigit(int number)
        {
            while (number > 9 && number != 11 && number != 22 && number != 33)
                number = number.ToString().Select(c => int.Parse(c.ToString())).Sum();
            return number;
        }

        private int GetCompoundNumber(string mobile)
        {
            return mobile.Select(c => int.Parse(c.ToString())).Sum();
        }

        private List<string> GetInternalCombinations(string mobile)
        {
            var digits = mobile.Select(c => c.ToString()).ToList();
            var pairs = new List<string>();
            for (int i = 0; i < digits.Count - 1; i++)
                pairs.Add($"{digits[i]}-{digits[i + 1]}");
            return pairs.Distinct().ToList();
        }

        private List<string> SuggestPins(int bn, int dn)
        {
            return new List<string> { "1356", "5559", "4566" }; // Placeholder logic
        }

        private string GetCompoundMeaning(int compoundNumber)
        {
            return compoundNumber switch
            {
                14 => "Loan liability, legal notice, health issues",
                19 => "Leader type, self respect, good professional",
                23 => "Win over enemies, extra relations",
                _ => "General compound interpretation"
            };
        }

        private Dictionary<int, List<int>> GetLuckyMap() => new()
    {
        {1, new() {1,3,5,6}}, {2, new() {1,3,5,6}}, {3, new() {1,3,5}},
        {4, new() {1,3,5,6}}, {5, new() {1,3,5,6}}, {6, new() {1,5,6}},
        {7, new() {1,3,5,6}}, {8, new() {3,5,6}}, {9, new() {1,3,5,6}},
    };

    }
}

