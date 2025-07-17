using System.Diagnostics;
using Utilities;

namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Thirteen Orphans",
            NameJA = "Kokushi Musou",
            Desc = "Awarded for having one of each of the thirteen different terminal and honour tiles, plus one extra terminal or honour tile.",
            Details = "Thirteen Orphans is one of the two exceptions of the \"four tile groups and one pair\" rule, requiring 13 unique tiles instead. Attempting Thirteen Orphans with 9 or less unique tiles has less than a 3% win rate. The Expected value for attempting a thirteen orphans hand is greater than average when starting with 10 or more unique tiles (significantly greater the more unique tiles you have).",
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int ThirteenOrphans = 1;

        [Winnable(
            NameEN = "Thirteen Orphans (13 Wait)",
            NameJA = "Kokushi Musou (13 Machi)",
            Desc = "A variation of Thirteen Orphans where the winning tile is a tile already in the hand, that is, the hand already contains all 13 unique \"orphan\" tiles.",
            Details = "Naturally drawing into a 13 wait thirteen orphans hand less then 0.01% and usually requires intentionally completing the hand while in furiten (discarded an orphan tile and therefore cannot call from another players discard).",
            OpenHand = ScoreBasis.OneYakuman
        )]
        public const int ThirteenOrphans13Wait = 2;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins ThirteenOrphans(in TileCounts counts) // Special win because it is needed for grouping.
        {
            const uint ThirteenOrphansFlags = (uint)(TileFlags.NumberSuits | TileFlags.Honors | TileFlags.One | TileFlags.Nine);

            // Winning Hand must contain all flags for Thirteen Orphans.
            if ((uint)(Hand & TileFlags.Tiles) != ThirteenOrphansFlags)
                return WinCatalog.None;

        #if DEBUG
            for (int i = TileFlags.Honors.TileIndex(); i < TileExtensions.COUNT_RANKLESS; i++)
            {
                Debug.Assert(counts[i] == 1 || counts[i] == 2, $"[ERROR] Thirteen Orphans hand must contain one or two of each honor tile, and the hand flags indicated that they should all exist in the hand.");
            }
        #endif

            // Check to make sure all numbered tiles are present (and help with deducing the wait).
            if (counts[TileFlags.Man | TileFlags.One] == 0 ||
                counts[TileFlags.Pin | TileFlags.One] == 0 ||
                counts[TileFlags.Sou | TileFlags.One] == 0 ||
                counts[TileFlags.Man | TileFlags.Nine] == 0 ||
                counts[TileFlags.Pin | TileFlags.Nine] == 0 ||
                counts[TileFlags.Sou | TileFlags.Nine] == 0) {
                return WinCatalog.None; // Missing a numbered tile.
            }

            Wins result = WinCatalog.ThirteenOrphans;

        #if DEBUG
            Debug.Assert(counts[WinningTile] == 1 || counts[WinningTile] == 2, "[ERROR] Thirteen Orphans hand seems valid but the winning tile does not have a count of 1 or 2, which is required for a valid Thirteen Orphans hand.");
        #endif

            if (counts[WinningTile] == 2) // Winning tile was a tile we already had.
            {
                result.Add(WinCatalog.ThirteenOrphans13Wait);
            }

            return result;
        }
    }
}