namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Mixed Sequences",
            NameJA = "Sanshoku Doujun",
            Desc = "Awarded for having three sequences of the same numbers, one in each suit.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int MixedSequences = 33;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins MixedSequences()
        {
            // Early out (optimization):
            if (Sequences.Length < 3 || !Hand.HasFlag(TileFlags.NumberSuits))
                return WinCatalog.None;

            // Brute force, it's just easier and faster:
            TileFlags flags = Sequences[0] | Sequences[1] | Sequences[2];
            TileFlags ranks = Sequences[0] & TileFlags.NumberRanks;
            if (flags.HasFlag(TileFlags.NumberSuits) && (flags & TileFlags.NumberRanks) == ranks)
                return WinCatalog.MixedSequences;

            if (Sequences.Length == 4)
            {
                // All variations (another 3 in total, one for each omitted sequence):
                flags = Sequences[0] | Sequences[1] | Sequences[3];
                if (flags.HasFlag(TileFlags.NumberSuits) && (flags & TileFlags.NumberRanks) == ranks)
                    return WinCatalog.MixedSequences;
                flags = Sequences[0] | Sequences[2] | Sequences[3];
                if (flags.HasFlag(TileFlags.NumberSuits) && (flags & TileFlags.NumberRanks) == ranks)
                    return WinCatalog.MixedSequences;

                flags = Sequences[1] | Sequences[2] | Sequences[3];
                ranks = Sequences[1] & TileFlags.NumberRanks; // Change ranks now that the first is not used
                if (flags.HasFlag(TileFlags.NumberSuits) && (flags & TileFlags.NumberRanks) == ranks)
                    return WinCatalog.MixedSequences;
            }

            return WinCatalog.None;
        }
    }
}