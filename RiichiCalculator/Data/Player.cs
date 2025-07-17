namespace RiichiCalculator
{
    public class Player
    {
        public TileList_15 Hidden;
        public Melds Melds = new Melds();
        public TileList_27 Discards;
        public PlayerFlags Flags;

        public TileFlags Seat => Hidden.GetFlags() & TileFlags.Seats;
        public bool IsOpen => Melds.Length > 0;

        public bool IsReadyHand {
            get => Flags.HasFlag(PlayerFlags.ReadyHand);
            set {
                if (value)
                    Flags |= PlayerFlags.ReadyHand;
                else
                    Flags &= ~PlayerFlags.ReadyHand;
            }
        }

        public bool IsDoubleReady {
            get => Flags.HasFlag(PlayerFlags.DoubleReady);
            set {
                if (value)
                    Flags |= PlayerFlags.DoubleReady;
                else
                    Flags &= ~PlayerFlags.DoubleReady;
            }
        }

        public bool IsBeforeFirstDiscard {
            get => Flags.HasFlag(PlayerFlags.BeforeFirstDiscard);
            set {
                if (value)
                    Flags |= PlayerFlags.BeforeFirstDiscard;
                else
                    Flags &= ~PlayerFlags.BeforeFirstDiscard;
            }
        }

        public bool IsMakingAQuad {
            get => Flags.HasFlag(PlayerFlags.MakingAQuad);
            set {
                if (value)
                    Flags |= PlayerFlags.MakingAQuad;
                else
                    Flags &= ~PlayerFlags.MakingAQuad;
            }
        }

        public void Validate<A>(A analysis, string prepend) where A : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            analysis.Assert(Hidden.Length == 13 - Melds.Length * 3, $"{prepend}Player must have at least one hidden tile.");
            Hidden.Validate(analysis, prepend + "(Hidden tiles) ");
            Melds.Validate(analysis, prepend + "(Melds) ");

            TileFlags seat = Seat;
            analysis.Assert(seat.HasExactlyOneOf(TileFlags.Seats), $"{prepend}The hidden tiles in players hand must all have the same seat: {seat}");

            for (int index = 0; index < Melds.Length; index++)
            {
                analysis.Assert(Melds[index].GetFlags().HasFlag(seat), 
                    $"{prepend}Meld at index {index} does not match the player's seat ({seat}): {Melds[index]}.");
            }

            analysis.Assert(!Flags.HasFlag(PlayerFlags.DoubleReady) || Flags.HasFlag(PlayerFlags.ReadyHand),
                $"{prepend}Player cannot be double ready without declaring a ready hand: {Flags}");
        }
    }

    public static class PlayerTilesExtensions
    {
        public static void Add(this ref TileCounts counts, in Player player)
        {
            counts.Add(player.Hidden);
            counts.Add(player.Melds);
            counts.Add(player.Discards);
        }
    }
}