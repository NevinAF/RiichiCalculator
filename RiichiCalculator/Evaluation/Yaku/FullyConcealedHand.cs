namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Fully Concealed Hand",
            NameJA = "Menzen Tsumo",
            Desc = "Awarded for winning by self-draw with a hand composed entirely of concealed hand.",
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int FullyConcealedHand = 25;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins FullyConcealedHand()
        {
            return Hand.HasExactlyOneOf(TileFlags.Seats)
                ? WinCatalog.FullyConcealedHand
                : WinCatalog.None;
        }
    }
}