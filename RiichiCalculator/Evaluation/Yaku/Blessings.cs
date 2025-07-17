namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Blessing of Man",
            NameJA = "Renhou",
            Desc = "Awarded to a player who wins by calling a tile before their first draw, as long as no tile calls or concealed quad declarations have been made before. Riichi declarations don't break Blessing of Man. Blessing of Man does not combine with other yaku and dora",
            ClosedHand = ScoreBasis.FiveHan
        )]
        public const int BlessingOfMan = 15;

        [Winnable(
            NameEN = "Blessing of Heaven",
            NameJA = "Tenhou",
            Desc = "Awarded to the East player if they win by self-draw on the initial deal, provided they have not made a concealed quad before.",
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int BlessingOfHeaven = 16;

        [Winnable(
            NameEN = "Blessing of Earth",
            NameJA = "Chiihou",
            Desc = "Awarded to a non-East player who wins by self-draw on their first draw, as long as no tile calls or concealed quad declarations have been made before. Riichi declarations don't break Blessing of Earth.",
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int BlessingOfEarth = 17;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins BlessingOfMan()
        {
            return Player.IsBeforeFirstDiscard && IsCalledWin && !Player.IsReadyHand
                ? WinCatalog.BlessingOfMan
                : WinCatalog.None;
        }

        public readonly Wins Blessings()
        {
            return !Player.IsBeforeFirstDiscard || IsCalledWin || Player.IsReadyHand
                ? WinCatalog.None
                : Seat.HasFlag(TileFlags.EastSeat)
                    ? WinCatalog.BlessingOfHeaven
                    : WinCatalog.BlessingOfEarth;
        }
    }
}