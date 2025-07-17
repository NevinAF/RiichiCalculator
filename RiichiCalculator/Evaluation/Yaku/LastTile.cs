namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Last Tile Draw",
            NameJA = "Haitei",
            Desc = "Awarded for winning by self-draw on the last tile from the live wall, i.e. when there are no tiles left in the live wall.",
            Details = "If the last tile is a replacement tile drawn after a quad, only the yaku \"After a Quad\" will be scored. I.e. you cannot score Last Tile Draw and After a Quad together",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int LastTileDraw = 26;

        [Winnable(
            NameEN = "Last Tile Claim",
            NameJA = "Houtei",
            Desc = "Awarded for winning by calling the tile discarded after the last tile of the wall, i.e. when there are no tiles left in the live wall.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int LastTileClaim = 27;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins LastTile()
        {
            return Deal.DeadWallCount != 0
                ? WinCatalog.None
                : IsSelfDraw
                    ? AfterAQuad().Empty ? WinCatalog.LastTileDraw : WinCatalog.None // Last Tile Draw
                    : WinCatalog.LastTileClaim; // Last Tile Claim
        }
    }
}