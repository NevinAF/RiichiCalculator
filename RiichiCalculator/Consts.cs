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
        public readonly static TileFlags C1 = TileFlags.One | TileFlags.Char;
        public readonly static TileFlags C2 = TileFlags.Two | TileFlags.Char;
        public readonly static TileFlags C3 = TileFlags.Three | TileFlags.Char;
        public readonly static TileFlags C4 = TileFlags.Four | TileFlags.Char;
        public readonly static TileFlags C5 = TileFlags.Five | TileFlags.Char;
        public readonly static TileFlags C6 = TileFlags.Six | TileFlags.Char;
        public readonly static TileFlags C7 = TileFlags.Seven | TileFlags.Char;
        public readonly static TileFlags C8 = TileFlags.Eight | TileFlags.Char;
        public readonly static TileFlags C9 = TileFlags.Nine | TileFlags.Char;
        public readonly static TileFlags D1 = TileFlags.One | TileFlags.Dot;
        public readonly static TileFlags D2 = TileFlags.Two | TileFlags.Dot;
        public readonly static TileFlags D3 = TileFlags.Three | TileFlags.Dot;
        public readonly static TileFlags D4 = TileFlags.Four | TileFlags.Dot;
        public readonly static TileFlags D5 = TileFlags.Five | TileFlags.Dot;
        public readonly static TileFlags D6 = TileFlags.Six | TileFlags.Dot;
        public readonly static TileFlags D7 = TileFlags.Seven | TileFlags.Dot;
        public readonly static TileFlags D8 = TileFlags.Eight | TileFlags.Dot;
        public readonly static TileFlags D9 = TileFlags.Nine | TileFlags.Dot;
        public readonly static TileFlags B1 = TileFlags.One | TileFlags.Bamboo;
        public readonly static TileFlags B2 = TileFlags.Two | TileFlags.Bamboo;
        public readonly static TileFlags B3 = TileFlags.Three | TileFlags.Bamboo;
        public readonly static TileFlags B4 = TileFlags.Four | TileFlags.Bamboo;
        public readonly static TileFlags B5 = TileFlags.Five | TileFlags.Bamboo;
        public readonly static TileFlags B6 = TileFlags.Six | TileFlags.Bamboo;
        public readonly static TileFlags B7 = TileFlags.Seven | TileFlags.Bamboo;
        public readonly static TileFlags B8 = TileFlags.Eight | TileFlags.Bamboo;
        public readonly static TileFlags B9 = TileFlags.Nine | TileFlags.Bamboo;
    }

    public static class SEAT {
        public readonly static TileFlags E = TileFlags.EastSeat;
        public readonly static TileFlags S = TileFlags.SouthSeat;
        public readonly static TileFlags W = TileFlags.WestSeat;
        public readonly static TileFlags N = TileFlags.NorthSeat;
    }
}