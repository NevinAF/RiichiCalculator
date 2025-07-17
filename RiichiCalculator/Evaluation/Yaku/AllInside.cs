namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "All Inside",
            NameJA = "Tan'yao",
            Desc = "Awarded for a hand composed entirely of suit tiles numbered from two to eight.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int AllInside = 13;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins AllInside()
        {
            return Hand.HasNoneOf(TileFlags.Terminals | TileFlags.Honors)
                ? WinCatalog.AllInside
                : WinCatalog.None;
        }
    }
}