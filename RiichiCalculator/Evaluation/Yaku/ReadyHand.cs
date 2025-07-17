using Utilities;

namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Ready Hand",
            NameJA = "Riichi",
            Desc = "A declaration made by a player when they are one tile away from winning.",
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int ReadyHand = 39;

        [Winnable(
            NameEN = "One Shot Ready",
            NameJA = "Ippatsu",
            Desc = "Awarded to a player who declares riichi and wins before their next discard, as long as no tile calls and no quad declarations have been made in between. Riichi declarations don't break Unbroken.",
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int OneShotReady = 40;

        [Winnable(
            NameEN = "Double Ready",
            NameJA = "Blessed Riichi",
            Desc = "Awarded to a player who declares riichi on their first discard, as long as no tile calls or concealed quad declarations have been made before",
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int DoubleReady = 41;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins ReadyHand()
        {
            if (!Player.IsReadyHand)
                return WinCatalog.None;

            Wins wins = WinCatalog.ReadyHand;

            if (Player.IsDoubleReady)
                wins.Add(WinCatalog.DoubleReady);
            if (Player.IsBeforeFirstDiscard)
                wins.Add(WinCatalog.OneShotReady);

            return wins;
        }
    }
}