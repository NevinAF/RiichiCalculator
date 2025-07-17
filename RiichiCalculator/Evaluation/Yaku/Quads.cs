namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Three Quads",
            NameJA = "Sankantsu",
            Desc = "Awarded for having three quads.",
            OpenHand = ScoreBasis.TwoFu,
            ClosedHand = ScoreBasis.TwoFu
        )]
        public const int ThreeQuads = 37;

        [Winnable(
            NameEN = "Four Quads",
            NameJA = "Suukantsu",
            Desc = "Awarded for having four quads.",
            OpenHand = ScoreBasis.OneYakuman,
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int FourQuads = 38;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins Quads()
        {
            if (Player.Melds.Length < 3 || Sets.Length < 3)
                return WinCatalog.None;

            int quads = 0;
            for (int quad = 0; quad < Player.Melds.Length; quad++)
                if (Player.Melds[quad].Length == 4)
                    quads++;

            return quads switch
            {
                3 => WinCatalog.ThreeQuads,
                4 => WinCatalog.FourQuads,
                _ => WinCatalog.None
            };
        }
    }
}