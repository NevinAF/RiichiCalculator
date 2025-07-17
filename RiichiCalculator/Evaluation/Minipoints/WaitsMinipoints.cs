namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Pair Wait",
            Desc = "Awarded when the winning tile is used to complete a pair.",
            ClosedHand = ScoreBasis.TwoFu,
            OpenHand = ScoreBasis.TwoFu
        )]
        public const int PairWait = 5;

        [Winnable(
            NameEN = "Edge Wait",
            Desc = "Awarded when the winning tile is the 3 in 1-2-3 or 7 in 7-8-9 sequence.",
            ClosedHand = ScoreBasis.TwoFu,
            OpenHand = ScoreBasis.TwoFu
        )]
        public const int EdgeWait = 6;

        [Winnable(
            NameEN = "Middle Wait",
            Desc = "Awarded when the winning tile is the central tile of a sequence (e.g., 3 in 2-3-4).",
            ClosedHand = ScoreBasis.TwoFu,
            OpenHand = ScoreBasis.TwoFu
        )]
        public const int MiddleWait = 7;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins WaitsMinipoints()
        {
            if (MinimumFu().Empty is false) // The winning tile can be placed such that no fu is awarded for the hand.
                return WinCatalog.None;

            // Pair wait
            if (Pair.HasFlag(WinningTile & TileFlags.Tiles))
                return WinCatalog.PairWait;

            // Cycle through all sequences to see if the winning tile is part of that sequence and if it is a special wait
            for (int i = 0; i < Sequences.Length; i++)
            {
                TileFlags sequence = Sequences[i];
                if (!sequence.HasFlag(WinningTile & TileFlags.Tiles))
                    continue;

                // 1-2-3 Edge Wait and 7-8-9 Edge Wait
                if ((WinningTile.HasFlag(TileFlags.Three) && sequence.HasFlag(TileFlags.One | TileFlags.Two | TileFlags.Three)) ||
                    (WinningTile.HasFlag(TileFlags.Seven) && sequence.HasFlag(TileFlags.Eight | TileFlags.Nine | TileFlags.Seven)))
                    return WinCatalog.EdgeWait;

                // Middle wait:
                uint meld = (uint)(sequence & TileFlags.NumberRanks);
                uint win = (uint)(WinningTile & TileFlags.NumberRanks);
                if (((meld << 1) & win) != 0 && ((meld >> 1) & win) != 0)
                    return WinCatalog.MiddleWait;
            }

            return WinCatalog.None;
        }

        /// <summary>
        /// In "Minimum Fu" hands (Pinfu), we don't want to know if fu can be awarded, but if fu can not be awarded. This returns true if there is a way the winning tile can be a part of a wait that has no fu.
        /// </summary>
        /// <returns></returns>
        public readonly bool WaitsCanHaveNoFu()
        {
            for (int i = 0; i < Sets.Length; i++)
            {
                if (Sets[i].HasFlag(WinningTile & TileFlags.Tiles))
                    return true;
            }

            for (int i = 0; i < Sequences.Length; i++)
            {
                TileFlags sequence = Sequences[i];
                if (sequence.HasFlag(WinningTile & TileFlags.Tiles))
                {
                    // Two sided wait:
                    TileFlags meld_nowin = sequence & TileFlags.NumberRanks & ~WinningTile;
                    if (meld_nowin.HasNoneOf(TileFlags.One | TileFlags.Nine) &&
                        ((uint)meld_nowin & ((uint)meld_nowin >> 1)) != 0) // Next to each other
                        return true;
                }
            }

            return false;
        }

        public void FixWinningWait()
        {
            if (!Pair.HasFlag(WinningTile & TileFlags.Tiles))
                return;

            for (int i = 0; i < Sequences.Length; i++)
            {
                ref TileFlags sequence = ref Sequences[i];
                if (!sequence.HasFlag(WinningTile & TileFlags.Tiles))
                    continue;

                if (MinimumFu().Empty) // We want the winning tile in the sequence:
                {
                    Pair     &= ~(WinningTile & ~TileFlags.Tiles); // Remove special flags given from the winning tile.
                    sequence |= WinningTile; // Add the winning tile to the sequence.
                }
                else {
                    Pair     |= WinningTile; // The winning tile will always be worth more (or same) as a pair.
                    sequence &= ~(WinningTile & ~TileFlags.Tiles); // Remove special flags given from the winning tile.
                }
            }
        }
    }
}