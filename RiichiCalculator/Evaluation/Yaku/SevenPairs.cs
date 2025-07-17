namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Seven Pairs",
            Desc = "A hand consisting of seven pairs of tiles instead of the usual four melds and a pair.",
            ClosedHand = ScoreBasis.TwoHan | ScoreBasis.TwentyFiveFu
        )]
        public const int SevenPairs = 0;
    }
    
    internal unsafe ref partial struct Winnables
    {
        public readonly Wins SevenPairs(in TileList_15 hand, in TileCounts counts) // Special win because it is needed for grouping.
        {
            for (int i = 0; i < hand.Length; i++)
            {
                if (counts[hand[i]] != 2)
                    return WinCatalog.None; // Not a valid seven pairs hand.
            }

            return WinCatalog.SevenPairs;
        }
    }
}