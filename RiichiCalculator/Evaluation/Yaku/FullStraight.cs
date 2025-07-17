namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Full Straight",
            NameJA = "Ikkitsoukan/Ittsou",
            Desc = "Awarded for having three sequences numbered 1-2-3, 4-5-6 and 7-8-9 in the same suit.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int FullStraight = 24;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins FullStraight()
        {
            // Early out (optimization):
            if (Sequences.Length < 3 || !Hand.HasFlag(TileFlags.NumberRanks))
                return WinCatalog.None;

            // Brute force, it's just easier and faster:
            TileFlags flags = Sequences[0] | Sequences[1] | Sequences[2];
            if (flags.HasFlag(TileFlags.NumberRanks) && flags.HasExactlyOneOf(TileFlags.NumberSuits))
                return WinCatalog.FullStraight;

            if (Sequences.Length == 4)
            {
                // All variations (another 3 in total, one for each omitted sequence):
                flags = Sequences[0] | Sequences[1] | Sequences[3];
                if (flags.HasFlag(TileFlags.NumberRanks) && flags.HasExactlyOneOf(TileFlags.NumberSuits))
                    return WinCatalog.FullStraight;
                flags = Sequences[0] | Sequences[2] | Sequences[3];
                if (flags.HasFlag(TileFlags.NumberRanks) && flags.HasExactlyOneOf(TileFlags.NumberSuits))
                    return WinCatalog.FullStraight;
                flags = Sequences[1] | Sequences[2] | Sequences[3];
                if (flags.HasFlag(TileFlags.NumberRanks) && flags.HasExactlyOneOf(TileFlags.NumberSuits))
                    return WinCatalog.FullStraight;
            }

            return WinCatalog.None;
        }
    }
}