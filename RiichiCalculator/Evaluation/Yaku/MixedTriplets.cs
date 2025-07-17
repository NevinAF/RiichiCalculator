namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Mixed Triplets",
            NameJA = "Sanshoku Doukou",
            Desc = "Awarded for having three triplets or quads of the same numbers, one in each suit.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int MixedTriplets = 34;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins MixedTriplets()
        {
            // Early out (optimization):
            if (Sets.Length < 3 || !Hand.HasFlag(TileFlags.NumberSuits))
                return WinCatalog.None;

            // Brute force, it's just easier and faster:
            TileFlags flags = Sets[0] | Sets[1] | Sets[2];
            if (flags.HasFlag(TileFlags.NumberSuits) && flags.HasExactlyOneOf(TileFlags.NumberRanks))
                return WinCatalog.MixedTriplets;

            if (Sets.Length == 4)
            {
                // All variations (another 3 in total, one for each omitted sequence):
                flags = Sets[0] | Sets[1] | Sets[3];
                if (flags.HasFlag(TileFlags.NumberSuits) && flags.HasExactlyOneOf(TileFlags.NumberRanks))
                    return WinCatalog.MixedTriplets;
                flags = Sets[0] | Sets[2] | Sets[3];
                if (flags.HasFlag(TileFlags.NumberSuits) && flags.HasExactlyOneOf(TileFlags.NumberRanks))
                    return WinCatalog.MixedTriplets;
                flags = Sets[1] | Sets[2] | Sets[3];
                if (flags.HasFlag(TileFlags.NumberSuits) && flags.HasExactlyOneOf(TileFlags.NumberRanks))
                    return WinCatalog.MixedTriplets;
            }

            return WinCatalog.None;
        }
    }
}