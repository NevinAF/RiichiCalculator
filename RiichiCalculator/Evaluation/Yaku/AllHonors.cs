namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "All Honors",
            NameJA = "Tsuuiisou",
            Desc = "Awarded for a hand composed entirely of honour tiles.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int AllHonors = 12;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins AllHonors()
        {
            return (Hand & TileFlags.Tiles).IsSubsetOf(TileFlags.Honors)
                ? WinCatalog.AllHonors
                : WinCatalog.None;
        }
    }
}