namespace RiichiCalculator
{
    internal unsafe partial struct Winnables
    {
        internal struct BestResult
        {
            public Wins Wins;
            public ScoreBasis ScoreBasis;
            public TileFlags Pair;
            public TileList_4 Sequences;
            public TileList_4 Sets;
        }

        /* Data from deal and hand */

        public readonly Deal Deal;
        public readonly TileFlags SeatWind;

        /* Inferrable that has been cached. */

        public readonly Player Player;
            // => SeatWind switch
            // {
            //     TileFlags.EastSeat => &Deal.EastHand,
            //     TileFlags.SouthSeat => &Deal.SouthHand,
            //     TileFlags.WestSeat => &Deal.WestHand,
            //     TileFlags.NorthSeat => &Deal.NorthHand,
            //     _ => throw new ArgumentOutOfRangeException()
            // };
            // Player.Hidden.GetFlags();
        public readonly TileFlags Hand;


        /* Computed tile groupings */

        public TileFlags Pair;
        public TileList_4 Sequences;
        public TileList_4 Sets;

        /* Computed Scoring */

        internal BestResult Best;
        internal byte DoraCount;


        /* Wrappers for clarity */

        public readonly TileFlags WinningTile => Deal.LatestDrawOrDiscard;
        public readonly TileFlags Seat => SeatWind.WindToSeat();
        public readonly bool IsOpenHand => Player.IsOpen;
        public readonly bool IsSelfDraw => WinningTile.HasFlag(Seat);
        public readonly bool IsCalledWin => WinningTile.HasNoneOf(Seat);
    }
}