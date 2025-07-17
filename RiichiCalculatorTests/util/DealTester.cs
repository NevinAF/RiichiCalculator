using System;
using System.Diagnostics;
using NUnit.Framework;
using RiichiCalculator;

namespace RiichiCalculatorTests
{
    public class DealTester
    {
        public readonly Deal Deal;

        public DealTester()
        {
            Deal = new Deal {
                EastHand = new Player(),
                SouthHand = new Player(),
                WestHand = new Player(),
                NorthHand = new Player(),
                Dora = new TileList_15(),
                PrevalentWind = TileFlags.EastWind,
                LiveWallCount = 136,
                DeadWallCount = 4
            };

            Deal.EastHand.Hidden.Fill(TileFlags.EastSeat | TileFlags.Back, 13);
            Deal.SouthHand.Hidden.Fill(TileFlags.SouthSeat | TileFlags.Back, 13);
            Deal.WestHand.Hidden.Fill(TileFlags.WestSeat | TileFlags.Back, 13);
            Deal.NorthHand.Hidden.Fill(TileFlags.NorthSeat | TileFlags.Back, 13);
        }

        public TileFlags Seat;
        public TileFlags PrevalentWind { get => Deal.PrevalentWind; set => Deal.PrevalentWind = value; }
        public TileFlags[] Dora { set => SetDora(value); }
        public TileFlags[] Hidden { set => SetHidden(value); }
        public TileFlags[][] Melds { set => SetMelds(value); }
        public TileFlags WinningTile { get => Deal.LatestDrawOrDiscard; set => Deal.LatestDrawOrDiscard = value; }
        public bool IsLastTile { get => Deal.DeadWallCount == 0; set => Deal.DeadWallCount = value ? 0 : 4; }

        public Player Player => Deal.Player(Seat);
        public bool IsReadyHand { get => Player.IsReadyHand; set => Player.IsReadyHand = value; }
        public bool IsDoubleReady { get => Player.IsDoubleReady; set => Player.IsDoubleReady = value; }
        public bool IsBeforeFirstDiscard { get => Player.IsBeforeFirstDiscard; set => Player.IsBeforeFirstDiscard = value; }
        public bool IsMakingAQuad { get => Player.IsMakingAQuad; set => Player.IsMakingAQuad = value; }
        public bool IsRobbingQuad { get => Deal.Player(Seat.NextSeat()).IsMakingAQuad; set => Deal.Player(Seat.NextSeat()).IsMakingAQuad = value; }


        private Wins _wins;
        public int[] Wins { set => _wins = new Wins(value); }
        public byte DoraHan = byte.MaxValue;
        public ScoreBasis Basis = new ScoreBasis { Fu = ushort.MaxValue, Han = byte.MaxValue, Yakuman = byte.MaxValue };
        public int Points = -1;

        public void Assert()
        {
            Score score = default;

            try {
                score = GetScore();
            }
            catch (Exception ex) {
                NUnit.Framework.Assert.Fail($"Score calculation failed: {ex.Message}");
            }

            NUnit.Framework.Assert.That(score.Wins, Is.EqualTo(_wins));

            if (DoraHan != byte.MaxValue)
                NUnit.Framework.Assert.That(score.Dora, Is.EqualTo(DoraHan));

            if (Basis.Yakuman != byte.MaxValue || Basis.Fu != ushort.MaxValue || Basis.Han != byte.MaxValue)
                NUnit.Framework.Assert.That(score.Basis, Is.EqualTo(Basis));

            if (Points > -1)
                NUnit.Framework.Assert.That(score.Basis.Points, Is.EqualTo(Points));
        }


        public void SetHidden(params TileFlags[] tiles)
        {
            Player.Hidden.Length = 0;
            for (int i = 0; i < tiles.Length; i++)
            {
                if ((tiles[i] & ~Seat).HasAnyOf(TileFlags.Seats))
                    throw new ArgumentException("Tiles cannot have other seat flags when setting hidden tiles.");
                Player.Hidden.Add(tiles[i] | Seat);
            }
        }

        public void SetDora(params TileFlags[] tiles)
        {
            Deal.Dora.Length = 0;
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].HasAnyOf(TileFlags.Seats))
                    throw new ArgumentException("Dora tiles cannot have seat flags.");
                Deal.Dora.Add(tiles[i]);
            }
        }

        public void SetMelds(params TileFlags[][] melds)
        {
            Player.Melds.Length = 0;
            for (int i = 0; i < melds.Length; i++)
            {
                AddMeld(melds[i]);
            }
        }

        public void AddMeld(params TileFlags[] tiles)
        {
            Debug.Assert(tiles.Length is 3 or 4, "Melds must contain either 3 or 4 tiles.");

            TileList_4 meld = new TileList_4();
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].HasNoneOf(TileFlags.Seats))
                    tiles[i] |= Seat;
                meld.Add(tiles[i]);
            }

            Player.Melds.Add(meld);
        }

        public Score GetScore() => Scorer.CalculateScore(Deal, Seat);
    }
}