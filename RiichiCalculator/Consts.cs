namespace RiichiCalculator
{
    public static class TILE {
        public readonly static TileFlags BK = TileFlags.Back;
        public readonly static TileFlags WD = TileFlags.WhiteDragon;
        public readonly static TileFlags RD = TileFlags.RedDragon;
        public readonly static TileFlags GD = TileFlags.GreenDragon;
        public readonly static TileFlags EW = TileFlags.EastWind;
        public readonly static TileFlags SW = TileFlags.SouthWind;
        public readonly static TileFlags WW = TileFlags.WestWind;
        public readonly static TileFlags NW = TileFlags.NorthWind;
        public readonly static TileFlags M1 = TileFlags.One | TileFlags.Man;
        public readonly static TileFlags M2 = TileFlags.Two | TileFlags.Man;
        public readonly static TileFlags M3 = TileFlags.Three | TileFlags.Man;
        public readonly static TileFlags M4 = TileFlags.Four | TileFlags.Man;
        public readonly static TileFlags M5 = TileFlags.Five | TileFlags.Man;
        public readonly static TileFlags M6 = TileFlags.Six | TileFlags.Man;
        public readonly static TileFlags M7 = TileFlags.Seven | TileFlags.Man;
        public readonly static TileFlags M8 = TileFlags.Eight | TileFlags.Man;
        public readonly static TileFlags M9 = TileFlags.Nine | TileFlags.Man;
        public readonly static TileFlags P1 = TileFlags.One | TileFlags.Pin;
        public readonly static TileFlags P2 = TileFlags.Two | TileFlags.Pin;
        public readonly static TileFlags P3 = TileFlags.Three | TileFlags.Pin;
        public readonly static TileFlags P4 = TileFlags.Four | TileFlags.Pin;
        public readonly static TileFlags P5 = TileFlags.Five | TileFlags.Pin;
        public readonly static TileFlags P6 = TileFlags.Six | TileFlags.Pin;
        public readonly static TileFlags P7 = TileFlags.Seven | TileFlags.Pin;
        public readonly static TileFlags P8 = TileFlags.Eight | TileFlags.Pin;
        public readonly static TileFlags P9 = TileFlags.Nine | TileFlags.Pin;
        public readonly static TileFlags S1 = TileFlags.One | TileFlags.Sou;
        public readonly static TileFlags S2 = TileFlags.Two | TileFlags.Sou;
        public readonly static TileFlags S3 = TileFlags.Three | TileFlags.Sou;
        public readonly static TileFlags S4 = TileFlags.Four | TileFlags.Sou;
        public readonly static TileFlags S5 = TileFlags.Five | TileFlags.Sou;
        public readonly static TileFlags S6 = TileFlags.Six | TileFlags.Sou;
        public readonly static TileFlags S7 = TileFlags.Seven | TileFlags.Sou;
        public readonly static TileFlags S8 = TileFlags.Eight | TileFlags.Sou;
        public readonly static TileFlags S9 = TileFlags.Nine | TileFlags.Sou;
    }

    public static class SEAT {
        public readonly static TileFlags E = TileFlags.EastSeat;
        public readonly static TileFlags S = TileFlags.SouthSeat;
        public readonly static TileFlags W = TileFlags.WestSeat;
        public readonly static TileFlags N = TileFlags.NorthSeat;
    }
}