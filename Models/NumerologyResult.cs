namespace NumerologyApi.Models
{
    public class NumerologyResult
    {
        public int Bn { get; set; }
        public int Dn { get; set; }
        public int MobileTotal { get; set; }
        public List<int> LuckyNumbers { get; set; } = new();
        public List<int> UnluckyNumbers { get; set; } = new();
        public Dictionary<int, int> DigitCounts { get; set; } = new();
        public List<int[]> DetectedCombinations { get; set; } = new();
        public bool LShapeDetected { get; set; }
        public int PowerfulNumber { get; set; }
    }
}
