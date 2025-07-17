using NUnit.Framework;
using RiichiCalculator;

using WINS = RiichiCalculator.WinCatalog;

namespace RiichiCalculatorTests
{
    public class Examples
    {
        [Test]
        public void WhiteDragonsAndFullStraight()
        {
            new DealTester()
            {
                Seat = SEAT.E,
                Dora = new[] { TILE.RD, TILE.GD },
                WinningTile = TILE.D2 | SEAT.E,
                Hidden = new[] { TILE.WD, TILE.WD, TILE.WD, TILE.C1, TILE.C2, TILE.C3, TILE.C4, TILE.C5, TILE.C6, TILE.D2 },
                Melds = new[] {
                    new[] { TILE.C7, TILE.C8 | SEAT.S, TILE.C9 }
                },
                // PrevalentWind = SEAT.E,
                // IsLastTile = false,
                // IsReadyHand = false,
                // IsDoubleReady = false,
                // IsBeforeFirstDiscard = false,
                // IsMakingAQuad = false,
                // IsRobbingQuad = false,

                Wins = new[] { // Yaku and Minipoints separated for clarity.
                    WINS.WhiteDragons, WINS.FullStraight,
                    WINS.PairWait, WINS.SelfDrawWin, WINS.ConcealedTerminalHonorTriplet
                },
                DoraHan = 3,
                Basis = new ScoreBasis { Fu = 32, Han = 5, Yakuman = 0 },
                Points = 2000
            }.Assert();
        }

        [Test]
        public void Pinfu()
        {
            new DealTester()
            {
                Seat = SEAT.N,
                Dora = new[] { TILE.D9 },
                WinningTile = TILE.B7 | SEAT.S,
                Hidden = new[] { TILE.D1, TILE.D2, TILE.D3, TILE.D5, TILE.D6, TILE.D7, TILE.C7, TILE.C8, TILE.C9, TILE.B5, TILE.B6, TILE.SW, TILE.SW },
                // Melds = new[] {},
                // PrevalentWind = SEAT.E,
                // IsLastTile = false,
                // IsReadyHand = false,
                // IsDoubleReady = false,
                // IsBeforeFirstDiscard = false,
                // IsMakingAQuad = false,
                // IsRobbingQuad = false,

                Wins = new[] { // Yaku and Minipoints separated for clarity.
                    WINS.MinimumFu,
                    WINS.CallWin
                },
                DoraHan = 1,
                Basis = new ScoreBasis { Fu = 30, Han = 2, Yakuman = 0 },
                Points = 500
            }.Assert();
        }

        [Test]
        public void PinfuFail_HonorPair()
        {
            new DealTester()
            {
                Seat = SEAT.S,
                Dora = new[] { TILE.D9 },
                WinningTile = TILE.B7 | SEAT.N,
                Hidden = new[] { TILE.D1, TILE.D2, TILE.D3, TILE.D5, TILE.D6, TILE.D7, TILE.C7, TILE.C8, TILE.C9, TILE.B5, TILE.B6, TILE.SW, TILE.SW },
                // Melds = new[] {},
                // PrevalentWind = SEAT.E,
                // IsLastTile = false,
                // IsReadyHand = false,
                // IsDoubleReady = false,
                // IsBeforeFirstDiscard = false,
                // IsMakingAQuad = false,
                // IsRobbingQuad = false,

                // Wins = new[] {
                // },
                // DoraHan = 0,
                // Basis = new ScoreBasis { Fu = 0, Han = 0, Yakuman = 0 },
                // Points = 0
            }.Assert();
        }

        [Test]
        public void Pinfu_PairAndTwoSidedWait()
        {
            new DealTester()
            {
                Seat = SEAT.S,
                Dora = new[] { TILE.D9 },
                WinningTile = TILE.B7 | SEAT.S,
                Hidden = new[] { TILE.D1, TILE.D2, TILE.D3, TILE.D5, TILE.D6, TILE.D7, TILE.C7, TILE.C8, TILE.C9, TILE.B5, TILE.B6, TILE.B7, TILE.B7 },
                // Melds = new[] {},
                // PrevalentWind = SEAT.E,
                // IsLastTile = false,
                // IsReadyHand = false,
                // IsDoubleReady = false,
                // IsBeforeFirstDiscard = false,
                // IsMakingAQuad = false,
                // IsRobbingQuad = false,

                Wins = new[] { // Yaku and Minipoints separated for clarity.
                    WINS.MinimumFu,
                    WINS.SelfDrawWinPinfu, WINS.FullyConcealedHand
                },
                DoraHan = 1,
                Basis = new ScoreBasis { Fu = 20, Han = 3, Yakuman = 0 },
                Points = 700
            }.Assert();
        }
    }
}