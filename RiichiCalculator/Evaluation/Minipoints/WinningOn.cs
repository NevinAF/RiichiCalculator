namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Win on Self-Draw",
            Desc = "Awarded by drawing the winning tile, instead of calling from another player.",
            ClosedHand = ScoreBasis.TwentyTwoFu,
            OpenHand = ScoreBasis.TwentyTwoFu
        )]
        public const int SelfDrawWin = 8;
        [Winnable(
            NameEN = "Win on Self-Draw (pinfu)",
            Desc = "Awarded by drawing the winning tile, instead of calling from another player. This is a special case for pinfu hands where the 2 additional fu for self-draw are dropped.",
            ClosedHand = ScoreBasis.TwentyFu,
            OpenHand = ScoreBasis.TwentyFu
        )]
        public const int SelfDrawWinPinfu = 84;

        [Winnable(
            NameEN = "Win on Call",
            Desc = "Awarded winning by calling a tile on a closed hand.",
            ClosedHand = ScoreBasis.ThirtyFu
        )]
        public const int CallWin = 9;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins WinningOn()
        {
            return IsSelfDraw
                ? (MinimumFu().Empty ? WinCatalog.SelfDrawWin : WinCatalog.SelfDrawWinPinfu)
                : WinCatalog.CallWin; // Awards nothing on open hands.
        }
    }
}