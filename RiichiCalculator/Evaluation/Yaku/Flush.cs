namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Common Flush",
            NameJA = "Hon'itsu",
            Desc = "Awarded for a hand composed entirely of tiles from one single suit and honours. The hand must have at least one suit tile and one honour tile.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.ThreeHan
        )]
        public const int CommonFlush = 22;

        [Winnable(
            NameEN = "Perfect Flush",
            NameJA = "Chin'itsu",
            Desc = "Awarded for a hand composed entirely of tiles from one single suit, without any honour tile. Perfect Flush does not combine with Common Flush as Common Flush must contain at least one honour tile.",
            OpenHand = ScoreBasis.FiveHan,
            ClosedHand = ScoreBasis.SixHan
        )]
        public const int PerfectFlush = 23;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins Flush()
        {
            return Hand.HasExactlyOneOf(TileFlags.NumberSuits)
                ? (Hand.HasAnyOf(TileFlags.Honors)
                    ? WinCatalog.CommonFlush
                    : WinCatalog.PerfectFlush)
                : WinCatalog.None;
        }
    }
}