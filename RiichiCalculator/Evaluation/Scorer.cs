using System.Diagnostics;

namespace RiichiCalculator
{
    internal unsafe ref partial struct Winnables
    {
        internal Winnables(Deal deal, TileFlags seat)
        {
        #if DEBUG
            Debug.Assert(deal != null, "[ERROR] Deal cannot be null.");
            Debug.Assert(seat.IsSubsetOf(TileFlags.Seats) && seat.HasOnlyOneFlag(), $"[ERROR] Seat wind must have exactly one seat tile, and contain only that seat tile: {seat}");
        #endif

            Deal = deal;
            SeatWind = seat.SeatToWind();
            Player = deal.Player(seat);


            Hand = deal.LatestDrawOrDiscard;
            for (int index = 0; index < Player.Hidden.Length; index++)
            {
                TileFlags tile = Player.Hidden[index];
                Hand |= tile; // OR for full hand.
            }

            Pair = default;
            Sequences = default;
            Sets = default;

            for (int index = 0; index < Player.Melds.Length; index++)
            {
                TileFlags meld = Player.Melds[index].GetFlags();
                if (meld.UsesOneTile())
                    Sets.Add(meld);
                else
                    Sequences.Add(meld);

                Hand |= meld; // OR for full hand.
            }

            Best = default;
            DoraCount = 0;
        }

        public readonly void ToScore(out Score score)
        {
            score.Basis = Best.ScoreBasis;
            score.Wins = Best.Wins;
            score.Dora = DoraCount;
            score.Pair = Best.Pair;
            score.Groups = default;
            score.Groups.Add(Best.Sequences);
            score.Groups.Add(Best.Sets);
        }
    }

    public unsafe static class Scorer
    {
        public static Score CalculateScore(Deal deal, TileFlags seatWind)
        {
            deal.Validate(default(AssertAnalysis));
            Winnables winnables = new Winnables(deal, seatWind);
            winnables.FormGroups(default(AssertAnalysis));
            winnables.ToScore(out Score result);

            return result;
        }
    }
}