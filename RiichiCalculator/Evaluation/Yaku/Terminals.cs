namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Common Terminals",
            NameJA = "Hohroutou",
            Desc = "Awarded for a hand composed entirely of terminal and honour tiles. The hand must have at least one terminal tile and at least one honour tile.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int CommonTerminals = 43;

        [Winnable(
            NameEN = "Perfect Terminals",
            NameJA = "Chinroutou",
            Desc = "Awarded for a hand composed entirely of terminal tiles, without any honour tile.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int PerfectTerminals = 44;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins Terminals()
        {
            return !(Hand & TileFlags.Tiles).IsSubsetOf(TileFlags.NumberSuits | TileFlags.Terminals | TileFlags.Honors)
                ? WinCatalog.None
                : Hand.HasAnyOf(TileFlags.Honors)
                    ? WinCatalog.CommonTerminals
                    : WinCatalog.PerfectTerminals;
        }
    }
}