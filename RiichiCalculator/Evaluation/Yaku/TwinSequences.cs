using System;

namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Twin Sequences",
            NameJA = "Iipeikou",
            Desc = "Awarded for having two sequences of the same numbers in the same suit.",
            Details = "The winning tile can be called even if it is a part of this sequence. Two sets of two sequences, double twin sequences, supersedes this win.",
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int TwinSequences = 45;

        [Winnable(
            NameEN = "Double Twin Sequences",
            NameJA = "Ryanpeikou",
            Desc = "Awarded for having four sequences forming two independent Twin Sequences. The Twin Sequences can be either of different or the same numbers and suits.",
            Details = "Double Twin Sequences does not combine with Twin Sequences and Seven Pairs. ",
            ClosedHand = ScoreBasis.ThreeHan
        )]
        public const int DoubleTwinSequences = 46;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins TwinSequences()
        {
            int doubleSequenceCount = 0;
            switch (Sequences.Length)
            {
                case 4:
                    if ((Sequences[0] & TileFlags.Tiles) == (Sequences[2] & TileFlags.Tiles))
                        doubleSequenceCount++;
                    if ((Sequences[0] & TileFlags.Tiles) == (Sequences[3] & TileFlags.Tiles))
                        doubleSequenceCount++;
                    if ((Sequences[1] & TileFlags.Tiles) == (Sequences[3] & TileFlags.Tiles))
                        doubleSequenceCount++;
                    goto case 3; // Fall through to handle both 3 and 2 sequences.
                case 3:
                    if ((Sequences[1] & TileFlags.Tiles) == (Sequences[2] & TileFlags.Tiles))
                        doubleSequenceCount++;
                    if ((Sequences[0] & TileFlags.Tiles) == (Sequences[2] & TileFlags.Tiles))
                        doubleSequenceCount++;
                    goto case 2; // Fall through to handle 2 sequences.
                case 2:
                    if ((Sequences[0] & TileFlags.Tiles) == (Sequences[1] & TileFlags.Tiles))
                        doubleSequenceCount++;
                    break;
            }

        #if DEBUG
            if (doubleSequenceCount > 2)
                throw new Exception("Hand should never be able to have more than two pure double sequences because it can never have more than 4 sequences: " + doubleSequenceCount);
        #endif

            return doubleSequenceCount switch
            {
                2 => WinCatalog.DoubleTwinSequences,
                1 => WinCatalog.TwinSequences,
                _ => WinCatalog.None
            };
        }
    }
}