namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Minimum Fu",
            NameJA = "Pinfu",
            Desc = "Awarded for winning on a closed hand having four sequences and a pair that is either of a suit tile or a non-value honour; the winning tile must complete a sequence with a two-sided wait.",
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int MinimumFu = 32;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins MinimumFu()
        {
            return !IsOpenHand && Sequences.Length == 4 && HonorPair().Empty && WaitsCanHaveNoFu()
                ? WinCatalog.MinimumFu
                : WinCatalog.None;
        }
    }
}