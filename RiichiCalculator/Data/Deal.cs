using System;

namespace RiichiCalculator
{
    public class Deal
    {
        public TileFlags PrevalentWind;
        public int LiveWallCount;
        public int DeadWallCount;
        public TileList_15 Dora;

        public TileFlags LatestDrawOrDiscard;

        public Player EastHand;
        public Player SouthHand;
        public Player WestHand;
        public Player NorthHand;

        public Player Player(TileFlags seat) => (seat & TileFlags.Seats) switch
        {
            TileFlags.EastSeat => EastHand,
            TileFlags.SouthSeat => SouthHand,
            TileFlags.WestSeat => WestHand,
            TileFlags.NorthSeat => NorthHand,
            _ => throw new ArgumentException("Invalid seat wind.")
        };

        public void Validate<T>(T analysis, string prepend = null) where T : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            analysis.Assert(PrevalentWind.HasExactlyOneOf(TileFlags.Winds), $"{prepend}Prevalent wind {PrevalentWind} must have exactly one wind tile.");
            analysis.Assert(LiveWallCount >= 0 && LiveWallCount <= 136, $"{prepend}Live wall count {LiveWallCount} is out of bounds [0, 136].");
            analysis.Assert(DeadWallCount >= 0 && DeadWallCount <= 4, $"{prepend}Dead wall count {DeadWallCount} is out of bounds [0, 4].");
            analysis.Assert(Dora.Length <= 10, $"{prepend}Dora count {Dora.Length} exceeds maximum of 10.");
            Dora.Validate(analysis, prepend + "(Dora) ");

            LatestDrawOrDiscard.ValidateAsSingleTile(analysis, prepend + "(Latest draw or discard) ");

            EastHand.Validate(analysis, prepend + "(East hand) ");
            analysis.Assert(EastHand.Seat == TileFlags.EastSeat, $"{prepend}East hand is composed of hidden tiles not belonging to the East seat: {EastHand.Seat}");

            SouthHand.Validate(analysis, prepend + "(South hand) ");
            analysis.Assert(SouthHand.Seat == TileFlags.SouthSeat, $"{prepend}South hand is composed of hidden tiles not belonging to the South seat: {SouthHand.Seat}");

            WestHand.Validate(analysis, prepend + "(West hand) ");
            analysis.Assert(WestHand.Seat == TileFlags.WestSeat, $"{prepend}West hand is composed of hidden tiles not belonging to the West seat: {WestHand.Seat}");

            NorthHand.Validate(analysis, prepend + "(North hand) ");
            analysis.Assert(NorthHand.Seat == TileFlags.NorthSeat, $"{prepend}North hand is composed of hidden tiles not belonging to the North seat: {NorthHand.Seat}");

        }
    }

    public static class DealExtensions
    {
        public static void Add(this ref TileCounts counts, in Deal deal)
        {
            counts.Add(deal.EastHand);
            counts.Add(deal.SouthHand);
            counts.Add(deal.WestHand);
            counts.Add(deal.NorthHand);
            counts.Add(deal.Dora);
            counts[deal.LatestDrawOrDiscard.TileIndex()]++;
        }
    }
}