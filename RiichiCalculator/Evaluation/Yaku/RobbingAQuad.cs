using System.Diagnostics;

namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Robbing a Quad",
            NameJA = "Chankan",
            Desc = "Awarded to a player winning by calling the tile an opponent uses to promote a quad.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int RobbingAQuad = 42;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins RobbingAQuad()
        {
            if (IsSelfDraw)
                return WinCatalog.None;

        #if DEBUG
            // Redundant, but makes it makes usage clear.
            Debug.Assert(Player.IsMakingAQuad is false, $"A player cannot win off another player's tile while they are still resolving their own quad: Player.IsMakingAQuad={Player.IsMakingAQuad}");
        #endif

            bool anyQuads = Deal.EastHand.IsMakingAQuad
                || Deal.SouthHand.IsMakingAQuad
                || Deal.WestHand.IsMakingAQuad
                || Deal.NorthHand.IsMakingAQuad;

            return anyQuads
                ? WinCatalog.RobbingAQuad
                : WinCatalog.None;
        }
    }
}