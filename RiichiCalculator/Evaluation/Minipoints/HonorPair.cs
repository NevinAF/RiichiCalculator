using System.Diagnostics;

namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Value Honor Pair",
            Desc = "Awarded for having a pair of value honor tiles (Red Dragon, Green Dragon, White Dragon, seat wind, prevalent wind).",
            ClosedHand = ScoreBasis.TwoFu,
            OpenHand = ScoreBasis.TwoFu
        )]
        public const int HonorPair = 3;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins HonorPair()
        {
        #if DEBUG
            // Redundant, but makes it makes usage clear.
            Debug.Assert(Deal.PrevalentWind.IsSubsetOf(TileFlags.Winds) && Deal.PrevalentWind.HasOnlyOneFlag(), $"[ERROR] Prevalent wind must have exactly one wind tile, and contain only that wind tile: Deal.PrevalentWind={Deal.PrevalentWind}");
            Debug.Assert(SeatWind.IsSubsetOf(TileFlags.Winds) && SeatWind.HasOnlyOneFlag(), $"[ERROR] Seat wind must have exactly one seat tile, and contain only that seat tile: SeatWind={SeatWind}");
        #endif

            return Pair.HasAnyOf(TileFlags.Dragons | SeatWind | (Deal.PrevalentWind & TileFlags.Winds))
                ? WinCatalog.HonorPair
                : WinCatalog.None;
        }
    }
}