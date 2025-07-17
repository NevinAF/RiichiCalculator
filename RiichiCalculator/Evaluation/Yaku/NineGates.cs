namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Nine Gates",
            NameJA = "Chouren pouto",
            Desc = "Awarded for having the tiles 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9 in a single suit, plus one extra tile of the same suit",
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int NineGates = 35;

        [Winnable(
            NameEN = "Nine Gates (9 wait)",
            NameJA = "Chouren pouto (9 Machi)",
            Desc = "Awarded with a nine gates hand in which the winning tile is the one extra tile of the same suit, that is, the hand already contains all 13 of the nine gates tiles and the winning tile could be any of the 1s or 9s.",
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int NineGates9Wait = 36;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins NineGates()
        {
            if (Hand.HasAnyOf(TileFlags.Honors) ||
                !Hand.HasExactlyOneOf(TileFlags.NumberSuits))
            {
                return WinCatalog.None;
            }

            // Nine gates can be checked based on the position of the pair rather than counting the tiles.
            // This makes it easier to check for the 9 wait variant.
            switch (Pair & TileFlags.NumberRanks)
            {
                case TileFlags.One: {
                    TileFlags seq = Sequences[0] | Sequences[1] | Sequences[2];
                    // (extra 3) 1-1, 1-2-3, 3-4-5, 6-7-8, 9-9-9
                    // (extra 6) 1-1, 1-2-3, 4-5-6, 6-7-8, 9-9-9
                    // (extra 9) 1-1, 1-2-3, 4-5-6, 7-8-9, 9-9-9
                    return Sets.Length != 1 || Sets[0].HasNoneOf(TileFlags.Nine) || seq.HasNoneOf(TileFlags.One)
                        ? WinCatalog.None
                        : (seq.HasFlag(TileFlags.Nine) && WinningTile.HasAnyOf(TileFlags.Nine)) ||
                            WinningTile.HasAnyOf(TileFlags.Three | TileFlags.Six)
                            ? ((Wins)WinCatalog.NineGates) | WinCatalog.NineGates9Wait // 9 wait
                            : WinCatalog.NineGates; // 1 wait
                }
                case TileFlags.Nine: {
                    TileFlags seq = Sequences[0] | Sequences[1] | Sequences[2];
                    // (extra 1) 1-1-1, 1-2-3, 4-5-6, 7-8-9, 9-9
                    // (extra 4) 1-1-1, 2-3-4, 4-5-6, 7-8-9, 9-9
                    // (extra 7) 1-1-1, 2-3-4, 5-6-7, 7-8-9, 9-9
                    return Sets.Length != 1 || Sets[0].HasNoneOf(TileFlags.One) || seq.HasNoneOf(TileFlags.Nine)
                        ? WinCatalog.None
                        : (seq.HasFlag(TileFlags.One) && WinningTile.HasAnyOf(TileFlags.One)) ||
                            WinningTile.HasAnyOf(TileFlags.Four | TileFlags.Seven)
                            ? ((Wins)WinCatalog.NineGates) | WinCatalog.NineGates9Wait // 9 wait
                            : WinCatalog.NineGates; // 1 wait
                }
                case TileFlags.Two: // (extra 2) 1-1-1, 2-2, 3-4-5, 6-7-8, 9-9-9
                case TileFlags.Five: // (extra 5) 1-1-1, 2-3-4, 5-5, 6-7-8, 9-9-9
                case TileFlags.Eight: // (extra 7) 1-1-1, 2-3-4, 5-6-7, 8-8, 9-9-9
                    return Sets.Length != 2 || !(Sets[0] | Sets[1]).HasFlag(TileFlags.One | TileFlags.Nine)
                        ? WinCatalog.None
                        : Pair.HasFlag(WinningTile) // Winning tile is the pair.
                            ? ((Wins)WinCatalog.NineGates) | WinCatalog.NineGates9Wait // 9 wait
                            : WinCatalog.NineGates; // 1 wait

                default: // Pair cannot be 3, 4, 6, or 7 when the hand is a nine gates hand.
                    return WinCatalog.None;
            }
        }
    }
}