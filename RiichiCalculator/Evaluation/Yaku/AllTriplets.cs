namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "All Triplets",
            NameJA = "Toitoi",
            Desc = "Awarded for having four triplets or quads and a pair.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int AllTriplets = 14;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins AllTriplets()
        {
            return Sets.Length == 4
                ? WinCatalog.AllTriplets
                : WinCatalog.None;
        }
    }
}