namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "After a Quad",
            NameJA = "Rinshan Kaihou",
            Desc = "Awarded for winning on the replacement tile drawn after declaring a quad.",
            Details = "It counts as a self-drawn win; the two minipoints for winning by self-draw are awarded. Since the quad has been successfully made, a kan dora indicator is revealed for it.",
            OpenHand = ScoreBasis.OneHan,
            ClosedHand = ScoreBasis.OneHan
        )]
        public const int AfterAQuad = 10;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins AfterAQuad()
        {
            return Player.IsMakingAQuad
                ? WinCatalog.AfterAQuad
                : WinCatalog.None;
        }
    }
}