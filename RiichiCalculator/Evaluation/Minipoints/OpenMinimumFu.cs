namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Open Minimum Fu",
            NameJA = "Open Pinfu",
            Desc = "Awarded for winning on an open hand having four sequences and a pair that is either of a suit tile or a non-value honour; the winning tile must complete a sequence with a two-sided wait and must be called from another player.",
            OpenHand = ScoreBasis.OneHan
        )]
        public const int OpenMinimumFu = 4;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins OpenMinimumFu()
        {
            return Sequences.Length == 4 && IsCalledWin && HonorPair().Empty && WaitsMinipoints().Empty
                ? WinCatalog.OpenMinimumFu
                : WinCatalog.None;
        }
    }
}