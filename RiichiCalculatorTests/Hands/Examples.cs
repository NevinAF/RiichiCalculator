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
                WinningTile = TILE.P2 | SEAT.E,
                Hidden = new[] { TILE.WD, TILE.WD, TILE.WD, TILE.M1, TILE.M2, TILE.M3, TILE.M4, TILE.M5, TILE.M6, TILE.P2 },
                Melds = new[] {
                    new[] { TILE.M7, TILE.M8 | SEAT.S, TILE.M9 }
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
                Dora = new[] { TILE.P9 },
                WinningTile = TILE.S7 | SEAT.S,
                Hidden = new[] { TILE.P1, TILE.P2, TILE.P3, TILE.P5, TILE.P6, TILE.P7, TILE.M7, TILE.M8, TILE.M9, TILE.S5, TILE.S6, TILE.SW, TILE.SW },
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
                Dora = new[] { TILE.P9 },
                WinningTile = TILE.S7 | SEAT.N,
                Hidden = new[] { TILE.P1, TILE.P2, TILE.P3, TILE.P5, TILE.P6, TILE.P7, TILE.M7, TILE.M8, TILE.M9, TILE.S5, TILE.S6, TILE.SW, TILE.SW },
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
                Dora = new[] { TILE.P9 },
                WinningTile = TILE.S7 | SEAT.S,
                Hidden = new[] { TILE.P1, TILE.P2, TILE.P3, TILE.P5, TILE.P6, TILE.P7, TILE.M7, TILE.M8, TILE.M9, TILE.S5, TILE.S6, TILE.S7, TILE.S7 },
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