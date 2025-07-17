namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Little Dragons",
            NameJA = "Shousangen",
            Desc = "Awarded for having two triplets or quads of dragons and a pair of dragons.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int LittleDragons = 28;

        [Winnable(
            NameEN = "Big Dragons",
            NameJA = "Daisangen",
            Desc = "Awarded for having three triplets or quads of dragons.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int BigDragons = 29;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins LittleBigDragons()
        {
            return Hand.HasFlag(TileFlags.Dragons)
                ? (Pair.HasAnyOf(TileFlags.Dragons)
                    ? WinCatalog.LittleDragons
                    : WinCatalog.BigDragons)
                : WinCatalog.None;
        }
    }
}