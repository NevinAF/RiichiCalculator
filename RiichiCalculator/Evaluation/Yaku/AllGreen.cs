namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "All Green",
            NameJA = "Ryouiisou",
            Desc = "Awarded for a hand composed entirely of tiles among the 2, 3, 4, 6, 8 of bamboo and the Green dragon. The hand is not required to include the Green dragon.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int AllGreen = 11;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins AllGreen()
        {
            const uint green = (uint)(TileFlags.Bamboo | TileFlags.Two | TileFlags.Three | TileFlags.Four | TileFlags.Six | TileFlags.Eight | TileFlags.GreenDragon);

            return (Hand & TileFlags.Tiles).IsSubsetOf((TileFlags)green)
                ? WinCatalog.AllGreen
                : WinCatalog.None;
        }
    }
}