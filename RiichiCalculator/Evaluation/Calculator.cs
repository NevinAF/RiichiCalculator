using System.Diagnostics;
using Utilities;

namespace RiichiCalculator
{
    internal unsafe ref partial struct Winnables {

        [Conditional("DEBUG")]
        internal readonly void AssertStandardGroupings()
        {
            Debug.Assert(Pair != default && Sequences.Length + Sets.Length == 4, $"[ERROR] To score a standard hand, it must either have a pair and 4 groups: pair={Pair}, sequences={Sequences.Length}, sets={Sets.Length}");

            Debug.Assert(Pair.UsesOneTile(), $"[ERROR] To score a standard hand, the pair must represent a single tile: {Pair}");
            // if ((Pair & TileFlags.Tiles) == (WinningTile & TileFlags.Tiles))
            //     Debug.Assert((Pair & ~Seat & ~WinningTile & TileFlags.Seats) == 0, $"[ERROR] A Pair that contains the winning tile must only contain the seat wind and the called wind (if any): {Pair}");
            // else
            //     Debug.Assert((Pair & TileFlags.Seats) == Seat, $"[ERROR] A Pair that does not contain the winning tile must only contain the seat wind: {Pair}");

            int meldSets = 0;
            int meldSequences = 0;

            for (int meld = 0; meld < Player.Melds.Length; meld++)
            {
                if (Player.Melds[meld].GetFlags().UsesOneTile())
                    meldSets++;
                else meldSequences++;
            }

            Debug.Assert(meldSets <= Sets.Length, $"[ERROR] The number of meld sets {meldSets} cannot exceed the number of total sets {Sets.Length}.");
            Debug.Assert(meldSequences <= Sequences.Length, $"[ERROR] The number of meld sequences {meldSequences} cannot exceed the number of total sequences {Sequences.Length}.");

            // Remaining sets and sequences must be unmelded, i.e. closed triplets/sequences only.
            for (int sets = meldSets; sets < Sets.Length; sets++)
            {
                Debug.Assert(Sets[sets].HasExactlyOneOf(TileFlags.Suits), $"[ERROR] To score a standard hand, all sets must contain exactly one type of tile suit: {Sets[sets]}");
                Debug.Assert(Sets[sets].HasExactlyOneOf(TileFlags.Ranks), $"[ERROR] To score a standard hand, all sets must contain exactly one type of tile rank: {Sets[sets]}");
            }

            for (int sequences = meldSequences; sequences < Sequences.Length; sequences++)
            {
                Debug.Assert(Sequences[sequences].HasExactlyOneOf(TileFlags.Suits), $"[ERROR] To score a standard hand, all sequences must contain exactly one type of tile suit: {Sequences[sequences]}");
                Debug.Assert(Sequences[sequences].UsesOneSequence(), $"[ERROR] To score a standard hand, all sequences must exactly three ranks in order: {Sequences[sequences]}");
            }
        }

        // internal readonly void 

        internal void CalculateAndReplaceWins()
        {
            AssertStandardGroupings(); // Debug only

            Wins wins = WinCatalog.None;
            AddYaku(ref wins);

            if (wins.Empty)
                return;

            AddMinipoints(ref wins);

            ReplaceBest(ref wins);
        }

        internal void ReplaceBest(ref Wins wins)
        {
            if (wins.Empty)
                return;

            ScoreBasis score = IsOpenHand ? WinCatalog.ScoreOpenWins(ref wins) : WinCatalog.ScoreClosedWins(ref wins);
            score.Han += DoraCount;

            if (score > Best.ScoreBasis)
            {
                Best.Wins = wins;
                Best.ScoreBasis = score;
                Best.Pair = Pair;
                Best.Sequences = Sequences;
                Best.Sets = Sets;
            }
        }

        internal readonly void AddYaku(ref Wins wins)
        {
            wins.Add(AfterAQuad());
            wins.Add(AllGreen());
            wins.Add(AllHonors());
            wins.Add(AllTriplets());
            wins.Add(Blessings());
            wins.Add(ConcealedTriplets());
            wins.Add(Ends());
            wins.Add(Flush());
            wins.Add(FullStraight());
            wins.Add(LastTile());
            wins.Add(LittleBigDragons());
            wins.Add(LittleBigWinds());
            wins.Add(MixedSequences());
            wins.Add(MixedTriplets());
            wins.Add(Quads());
            wins.Add(RobbingAQuad());
            wins.Add(Terminals());
            wins.Add(ValueHonors());

            if (!IsOpenHand)
            {
                wins.Add(ReadyHand());
                wins.Add(MinimumFu());
                wins.Add(TwinSequences());
                wins.Add(FullyConcealedHand());
                wins.Add(BlessingOfMan());
                wins.Add(NineGates());
            }
        }

        internal readonly void AddMinipoints(ref Wins wins)
        {
            wins.Add(HonorPair());
            wins.Add(WaitsMinipoints());
            wins.Add(WinningOn());
            wins.Add(SetsMinipoints());

            if (IsOpenHand)
            {
                wins.Add(OpenMinimumFu());
            }
        }
    }
}