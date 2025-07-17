namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Common Ends",
            NameJA = "Chanta",
            Desc = "Awarded if all groups and the pair contain at least one terminal or honour tile. The hand must have at least one sequence and at least one honour tile.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int CommonEnds = 20;

        [Winnable(
            NameEN = "Perfect Ends",
            NameJA = "Junchan",
            Desc = "Awarded if all groups and the pair contain at least one terminal, without any honour tile. The hand must have at least one sequence.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.ThreeHan
        )]
        public const int PerfectEnds = 21;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins Ends()
        {
            // The hand check is a little bit of an optimization, but not necessary.
            if (Sequences.Length == 0 || Hand.HasAnyOf(TileFlags.Four | TileFlags.Five | TileFlags.Six))
                return WinCatalog.None;

            // Make sure all sequences have at least one terminal tile
            for (int seq = 0; seq < Sequences.Length; seq++)
            {
                if (Sequences[seq].HasNoneOf(TileFlags.Terminals))
                    return WinCatalog.None;
            }

            // The pair and sets must only contain terminals (and maybe honours).
            // This cannot be done with sets because they may still contain any 2/3/7/8 tiles.
            TileFlags pairAndSets = Pair;
            for (int set = 0; set < Sets.Length; set++)
                pairAndSets |= Sets[set];

            return !(pairAndSets & TileFlags.Tiles).IsSubsetOf(TileFlags.NumberSuits | TileFlags.Terminals | TileFlags.Honors)
                ? WinCatalog.None
                : pairAndSets.HasAnyOf(TileFlags.Honors)
                    ? WinCatalog.CommonEnds
                    : WinCatalog.PerfectEnds;
        }
    }
}