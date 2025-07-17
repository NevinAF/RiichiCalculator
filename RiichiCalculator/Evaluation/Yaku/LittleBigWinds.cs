namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Little Winds",
            NameJA = "Shou suushi",
            Desc = "Awarded for having three triplets or quads of winds and a pair of winds.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int LittleWinds = 30;

        [Winnable(
            NameEN = "Big Winds",
            NameJA = "Daisuushi",
            Desc = "Awarded for having four triplets or quads of winds.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int BigWinds = 31;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins LittleBigWinds()
        {
            return Hand.HasFlag(TileFlags.Winds)
                ? (Pair.HasAnyOf(TileFlags.Winds)
                    ? WinCatalog.LittleWinds
                    : WinCatalog.BigWinds)
                : WinCatalog.None;
        }
    }
}