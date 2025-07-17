namespace RiichiCalculator
{
    public static partial class WinCatalog
    {
        [Winnable(
            NameEN = "Three Concealed Triplets",
            NameJA = "San'ankou",
            Desc = "Awarded for having three concealed triplets or quads. Triplets must be self-drawn, else they are considered open.",
            OpenHand = ScoreBasis.TwoHan,
            ClosedHand = ScoreBasis.TwoHan
        )]
        public const int ThreeConcealedTriplets = 18;

        [Winnable(
            NameEN = "Four Concealed Triplets",
            NameJA = "Suu'ankou",
            Desc = "Awarded for having four concealed triplets or quads. Must be won by self-draw, or by calling a tile to complete the pair.",
            ClosedHand = ScoreBasis.OneYakuman
        )]
        public const int FourConcealedTriplets = 19;
    }

    internal unsafe ref partial struct Winnables
    {
        public readonly Wins ConcealedTriplets()
        {
            // Early out (optimization):
            if (Sets.Length < 3)
                return WinCatalog.None;

            int count = 0;
            for (int i = 0; i < Sets.Length; i++)
            {
                if (Sets[i].HasExactlyOneOf(TileFlags.Seats))
                    count++;
            }

            return count switch
            {
                3 => WinCatalog.ThreeConcealedTriplets,
                4 => WinCatalog.FourConcealedTriplets,
                _ => WinCatalog.None
            };
        }
    }
}