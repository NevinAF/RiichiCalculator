using Utilities;

namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Red Dragons",
            NameJA = "Yakuhai",
            Desc = "Awarded for a hand with a group of red dragon tiles.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int RedDragons = 47;

        [Winnable(
            NameEN = "Green Dragons",
            NameJA = "Yakuhai",
            Desc = "Awarded for a hand with a group of green dragon tiles.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int GreenDragons = 48;

        [Winnable(
            NameEN = "White Dragons",
            NameJA = "Yakuhai",
            Desc = "Awarded for a hand with a group of white dragon tiles.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int WhiteDragons = 49;

        [Winnable(
            NameEN = "Seat Wind",
            NameJA = "Yakuhai",
            Desc = "Awarded for a hand with a group of tiles matching the player's seat wind.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int SeatWind = 50;

        [Winnable(
            NameEN = "Prevalent Wind",
            NameJA = "Yakuhai",
            Desc = "Awarded for a hand with a group of tiles matching the prevalent wind.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int PrevalentWind = 51;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins ValueHonors()
        {
            // Optimization but also avoids counting 7-pairs.
            if (Sets.Length == 0)
                return WinCatalog.None; // No melds, no value honors.

            TileFlags nonpair = Hand & ~Pair; // Honor must come in sets or a pair, so this excludes the pair honors.

            // TODO: Look at opcode. Does this optimize out the branching? Math can be used to avoid branching.
            // Would that even be faster?

            Wins result = WinCatalog.None;

            if (nonpair.HasFlag(TileFlags.RedDragon))
                result.Add(WinCatalog.RedDragons);

            if (nonpair.HasFlag(TileFlags.GreenDragon))
                result.Add(WinCatalog.GreenDragons);

            if (nonpair.HasFlag(TileFlags.WhiteDragon))
                result.Add(WinCatalog.WhiteDragons);

            if (nonpair.HasFlag(SeatWind))
                result.Add(WinCatalog.SeatWind);

            if (nonpair.HasFlag(Deal.PrevalentWind))
                result.Add(WinCatalog.PrevalentWind);

            return result;
        }
    }
}