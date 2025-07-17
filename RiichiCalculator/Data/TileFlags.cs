using System;
using Utilities;

namespace RiichiCalculator
{
    [Flags]
    public enum TileFlags : int
    {
        Tiles = Suits | Ranks,
        Suits = NumberSuits | Honors | Back,
        Ranks = NumberRanks | Honors | Back,

        /// <summary> Represents a facedown tile, usually found only in a closed Kan. </summary>
        Back = 1 << TileExtensions.INDEX_BACK,

        /// <summary> Represents the numbered rank "1" for Man, Pin, and Sou suits. </summary>
        One = 1 << TileExtensions.INDEX_ONE,
        /// <summary> Represents the numbered rank "2" for Man, Pin, and Sou suits. </summary>
        Two = 1 << TileExtensions.INDEX_TWO,
        /// <summary> Represents the numbered rank "3" for Man, Pin, and Sou suits. </summary>
        Three = 1 << TileExtensions.INDEX_THREE,
        /// <summary> Represents the numbered rank "4" for Man, Pin, and Sou suits. </summary>
        Four = 1 << TileExtensions.INDEX_FOUR,
        /// <summary> Represents the numbered rank "5" for Man, Pin, and Sou suits. </summary>
        Five = 1 << TileExtensions.INDEX_FIVE,
        /// <summary> Represents the numbered rank "6" for Man, Pin, and Sou suits. </summary>
        Six = 1 << TileExtensions.INDEX_SIX,
        /// <summary> Represents the numbered rank "7" for Man, Pin, and Sou suits. </summary>
        Seven = 1 << TileExtensions.INDEX_SEVEN,
        /// <summary> Represents the numbered rank "8" for Man, Pin, and Sou suits. </summary>
        Eight = 1 << TileExtensions.INDEX_EIGHT,
        /// <summary> Represents the numbered rank "9" for Man, Pin, and Sou suits. </summary>
        Nine = 1 << TileExtensions.INDEX_NINE,
        /// <summary> Represents all numbered ranks from 1 to 9 for Man, Pin, and Sou suits. </summary>
        Simples = Two | Three | Four | Five | Six | Seven | Eight,
        Terminals = One | Nine,
        NumberRanks = One | Two | Three | Four | Five | Six | Seven | Eight | Nine,

        /// <summary> Represents the East Wind tile for Honor suits. </summary>
        EastWind = 1 << TileExtensions.INDEX_EASTWIND,
        /// <summary> Represents the South Wind tile for Honor suits. </summary>
        SouthWind = 1 << TileExtensions.INDEX_SOUTHWIND,
        /// <summary> Represents the West Wind tile for Honor suits. </summary>
        WestWind = 1 << TileExtensions.INDEX_WESTWIND,
        /// <summary> Represents the North Wind tile for Honor suits. </summary>
        NorthWind = 1 << TileExtensions.INDEX_NORTHWIND,
        /// <summary> Represents all Wind tiles, including East, South, West, and North tiles. </summary>
        Winds = EastWind | SouthWind | WestWind | NorthWind,

        /// <summary> Represents the White Dragon tile for Honor suits. </summary>
        WhiteDragon = 1 << TileExtensions.INDEX_WHITEDRAGON,
        /// <summary> Represents the Red Dragon tile for Honor suits. </summary>
        RedDragon = 1 << TileExtensions.INDEX_REDDRAGON,
        /// <summary> Represents the Green Dragon tile for Honor suits. </summary>
        GreenDragon = 1 << TileExtensions.INDEX_GREENDRAGON,
        /// <summary> Represents all Dragon tiles, including Red, Green, and White tiles. </summary>
        Dragons = RedDragon | GreenDragon | WhiteDragon,

        /// <summary> Represents all Honor tiles, including Winds and Dragons. </summary>
        Honors = Winds | Dragons,

        /// <summary> Represents the Man suit, commonly known as Characters. </summary>
        Char = 1 << TileExtensions.INDEX_CHAR,
        /// <summary> Represents the Pin suit, commonly known as Dots. </summary>
        Dot = 1 << TileExtensions.INDEX_DOT,
        /// <summary> Represents the Sou suit, commonly known as Bamboos. </summary>
        Bamboo = 1 << TileExtensions.INDEX_BAMBOO,

        /// <summary> Represents all numbered suits, including Man, Pin, and Sou. </summary>
        NumberSuits = Char | Dot | Bamboo,

        RedFive = 1 << TileExtensions.INDEX_REDDORA,

        Seats = EastSeat | SouthSeat | WestSeat | NorthSeat,
        EastSeat = 1 << TileExtensions.INDEX_EASTSEAT,
        SouthSeat = 1 << TileExtensions.INDEX_SOUTHSEAT,
        WestSeat = 1 << TileExtensions.INDEX_WESTSEAT,
        NorthSeat = 1 << TileExtensions.INDEX_NORTHSEAT,

        /// <summary> Placeholder for any rank, used for deducing tile through inference. </summary>
        Inference = 1 << 31,
    }

    public static class TileExtensions
    {
        public const int INDEX_ONE = 0;
        public const int INDEX_TWO = 1;
        public const int INDEX_THREE = 2;
        public const int INDEX_FOUR = 3;
        public const int INDEX_FIVE = 4;
        public const int INDEX_SIX = 5;
        public const int INDEX_SEVEN = 6;
        public const int INDEX_EIGHT = 7;
        public const int INDEX_NINE = 8;
        public const int INDEX_BACK = 9;
        public const int INDEX_EASTWIND = 10;
        public const int INDEX_SOUTHWIND = 11;
        public const int INDEX_WESTWIND = 12;
        public const int INDEX_NORTHWIND = 13;
        public const int INDEX_WHITEDRAGON = 14;
        public const int INDEX_GREENDRAGON = 15;
        public const int INDEX_REDDRAGON = 16;
        public const int INDEX_CHAR = 17;
        public const int INDEX_DOT = 18;
        public const int INDEX_BAMBOO = 19;
        public const int INDEX_EASTSEAT = 20;
        public const int INDEX_SOUTHSEAT = 21;
        public const int INDEX_WESTSEAT = 22;
        public const int INDEX_NORTHSEAT = 23;

        public const int INDEX_REDDORA = 24;

        public const int INDEX_RANKS = INDEX_ONE;
        public const int INDEX_RANKS_END = INDEX_REDDRAGON;
        public const int INDEX_SUITS = INDEX_BACK;
        public const int INDEX_SUITS_END = INDEX_BAMBOO;

        public const int INDEX_NUMRANKS = INDEX_ONE;
        public const int INDEX_NUMSUITS = INDEX_CHAR;
        public const int INDEX_WINDS = INDEX_EASTWIND;
        public const int INDEX_DRAGONS = INDEX_WHITEDRAGON;
        public const int INDEX_SEATS = INDEX_EASTSEAT;

        public const int COUNT_RANKLESS = INDEX_NUMSUITS - INDEX_SUITS;
        public const int COUNT_NUMRANKS = INDEX_NINE - INDEX_ONE + 1;
        public const int COUNT_NUMSUITS = INDEX_BAMBOO - INDEX_CHAR + 1;
        public const int COUNT_UNIQUE_TYPES = COUNT_RANKLESS + (COUNT_NUMRANKS * COUNT_NUMSUITS);


    #if DEBUG
        /*
         * Ensure that the TileFlags enum is laid out correctly.
         * This is important for the bitwise operations to work as expected.
         */
    #pragma warning disable IDE0051

        const byte seq_num_ranks = INDEX_ONE + 1 == INDEX_TWO &&
            INDEX_TWO + 1 == INDEX_THREE &&
            INDEX_THREE + 1 == INDEX_FOUR &&
            INDEX_FOUR + 1 == INDEX_FIVE &&
            INDEX_FIVE + 1 == INDEX_SIX &&
            INDEX_SIX + 1 == INDEX_SEVEN &&
            INDEX_SEVEN + 1 == INDEX_EIGHT &&
            INDEX_EIGHT + 1 == INDEX_NINE ? 0 : -1;

        /// <summary>
        /// Although the order technically does not matter, it is important that each of the suits are different and contiguous.
        /// </summary>
        const byte seq_num_suits = INDEX_CHAR + 1 == INDEX_DOT &&
            INDEX_DOT + 1 == INDEX_BAMBOO ? 0 : -1;

        const byte num_after_rankless = INDEX_NUMSUITS > INDEX_WINDS &&
            INDEX_NUMSUITS > INDEX_BACK &&
            INDEX_NUMSUITS > INDEX_DRAGONS ? 0 : -1;

        const byte seq_winds = INDEX_EASTWIND + 1 == INDEX_SOUTHWIND &&
            INDEX_SOUTHWIND + 1 == INDEX_WESTWIND &&
            INDEX_WESTWIND + 1 == INDEX_NORTHWIND ? 0 : -1;

        const byte seq_dragons = INDEX_WHITEDRAGON + 1 == INDEX_GREENDRAGON &&
            INDEX_GREENDRAGON + 1 == INDEX_REDDRAGON ? 0 : -1;

        const byte seq_seats = INDEX_EASTSEAT + 1 == INDEX_SOUTHSEAT &&
            INDEX_SOUTHSEAT + 1 == INDEX_WESTSEAT &&
            INDEX_WESTSEAT + 1 == INDEX_NORTHSEAT ? 0 : -1;

        const byte ranks_count = INDEX_RANKS_END - INDEX_RANKS + 1 == (9 + 7 + 1) ? 0 : -1;
        const byte suits_count = INDEX_SUITS_END - INDEX_SUITS + 1 == (3 + 7 + 1) ? 0 : -1;

        const byte ranks_includes_unique = (INDEX_ONE +
            INDEX_TWO +
            INDEX_THREE +
            INDEX_FOUR +
            INDEX_FIVE +
            INDEX_SIX +
            INDEX_SEVEN +
            INDEX_EIGHT +
            INDEX_NINE +
            INDEX_BACK +
            INDEX_EASTWIND +
            INDEX_SOUTHWIND +
            INDEX_WESTWIND +
            INDEX_NORTHWIND +
            INDEX_REDDRAGON +
            INDEX_GREENDRAGON +
            INDEX_WHITEDRAGON) == (INDEX_RANKS_END - INDEX_RANKS + 1) * (INDEX_RANKS_END + INDEX_RANKS) / 2 ? 0 : -1;

        const byte suits_includes_unique = (INDEX_BACK +
            INDEX_EASTWIND +
            INDEX_SOUTHWIND +
            INDEX_WESTWIND +
            INDEX_NORTHWIND +
            INDEX_REDDRAGON +
            INDEX_GREENDRAGON +
            INDEX_WHITEDRAGON +
            INDEX_CHAR +
            INDEX_DOT +
            INDEX_BAMBOO) == (INDEX_SUITS_END - INDEX_SUITS + 1) * (INDEX_SUITS_END + INDEX_SUITS) / 2 ? 0 : -1;

    #pragma warning restore IDE0051
    #endif

        public static bool RepresentsOneOf(this TileFlags tile, TileFlags other)
        {
            return tile.IsSubsetOf(other) && tile.HasOnlyOneFlag();
        }

        public static TileFlags SeatToWind(this TileFlags seat)
        {
            const int Shift = INDEX_SEATS - INDEX_WINDS;
            return (TileFlags)((uint)(seat & TileFlags.Seats) >> Shift);
        }

        public static TileFlags WindToSeat(this TileFlags wind)
        {
            const int Shift = INDEX_SEATS - INDEX_WINDS;
            return (TileFlags)((uint)(wind & TileFlags.Winds) << Shift);
        }

        public static TileFlags NextSeat(this TileFlags seat)
        {
            // Returns the next seat in the clockwise direction.
            
            return (TileFlags)(((uint)(seat - INDEX_SEATS) + 1) % 4);
        }

        public static bool HasOnlyOneFlag(this TileFlags tile)
        {
            return BitOperations.IsPowerOfTwo((uint)tile);
        }

        public static bool HasExactlyOneOf(this TileFlags tile, TileFlags other)
        {
            return (tile & other).HasOnlyOneFlag();
        }

        public static bool HasZeroOrOneOf(this TileFlags tile, TileFlags other)
        {
            TileFlags intersection = tile & other;
            return intersection == 0 || intersection.HasOnlyOneFlag();
        }

        public static bool HasTwoOrMoreOf(this TileFlags tile, TileFlags other)
        {
            TileFlags intersection = tile & other;
            return intersection != 0 && !intersection.HasOnlyOneFlag();
        }

        public static bool HasNoneOf(this TileFlags tile, TileFlags other)
        {
            return (tile & other) == 0;
        }

        public static bool HasAnyOf(this TileFlags tile, TileFlags other)
        {
            return (tile & other) != 0;
        }

        public static bool IsSubsetOf(this TileFlags tile, TileFlags other)
        {
            return (tile & other) == tile;
            // Same as return (tile | other) == other and (tile & ~other) == 0;
        }

        public static bool UsesOneTile(this TileFlags tile)
        {
            return tile.HasExactlyOneOf(TileFlags.Ranks) &&
                   tile.HasExactlyOneOf(TileFlags.Suits);
        }

        public static bool RepresentsSet(this TileFlags tile)
        {
            return (tile & ~TileFlags.Back).UsesOneTile(); // Includes concealed quad by excluding Back
        }

        public static bool UsesOneSequence(this TileFlags tile)
        {
            uint ranks = (uint)(tile & TileFlags.NumberRanks);
            return tile.HasExactlyOneOf(TileFlags.NumberSuits) &&
                // Has only three consecutive bits.
                (ranks & (ranks >> 1) & (ranks >> 2)) * 7 == ranks;
        }

        public static bool RanksContainSequence(this TileFlags tile)
        {
            // Checks if the ranks contain a sequence, which is three consecutive ranks.
            uint ranks = (uint)(tile & TileFlags.NumberRanks);
            return (ranks & (ranks >> 1) & (ranks >> 2)) * 7 != 0;
        }

        public static bool HasExactlyTwoSeats(this TileFlags tile)
        {
            return BitOperations.PopCount((uint)(tile & TileFlags.Seats)) == 2;
        }

        public static bool HasSeatAndAtMostOneMore(this TileFlags tile, TileFlags seat)
        {
            return tile.HasFlag(seat) && (tile & ~seat).HasZeroOrOneOf(TileFlags.Seats);
        }

        public static void ValidateAsSingleTile<A>(this TileFlags tile, A analysis, string prepend = null) where A : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            analysis.Assert(tile.HasExactlyOneOf(TileFlags.Ranks), prepend + "Tile does not have exactly one rank: " + tile);
            analysis.Assert(tile.HasExactlyOneOf(TileFlags.Suits), prepend + "Tile does not have exactly one suit: " + tile);
            analysis.Assert(tile.HasZeroOrOneOf(TileFlags.Seats), prepend + "Tile does not have exactly one seat. A revealed tile must've been drawn by someone: " + tile);
        }

        public static void ValidateAsQuad<A>(this TileFlags tile, A analysis, string prepend = null) where A : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            if (tile.HasFlag(TileFlags.Back))
            {
                tile &= ~TileFlags.Back; // Remove Back tile for validation
                analysis.Assert(tile.HasExactlyOneOf(TileFlags.Seats), prepend + "Concealed quad does not have exactly one seat. Quads with any Back tiles must be concealed and therefore all tiles must belong to one person: " + tile);
            }
            else {
                analysis.Assert(tile.HasExactlyTwoSeats(), prepend + "Open quad does not have exactly two seats. Quads without and Back tiles must be open, and therefore the tiles must belong to two people: " + tile);
            }

            analysis.Assert(tile.HasExactlyOneOf(TileFlags.Ranks), prepend + "Open quad does not have exactly one rank: " + tile);
            analysis.Assert(tile.HasExactlyOneOf(TileFlags.Suits), prepend + "Open quad does not have exactly one suit: " + tile);
        }

        public static int TileIndex(this TileFlags tile)
        {
        #if DEBUG
            if ((tile & TileFlags.Ranks) == 0 || (tile & TileFlags.Suits) == 0)
                throw new ArgumentException("TileFlags must represent have a rank and a suit.", nameof(tile));

            // As long as there is a rank and suit, this will just return the first index in the flags.
            // if (!tile.UsesOneTile())
            //     throw new ArgumentException("TileFlags must represent a single tile.", nameof(tile));
        #endif

            int suit = BitOperations.TrailingZeroCount((uint)(tile & TileFlags.Suits));

            if (suit < INDEX_NUMSUITS) // Must be a tile back or honor tile:
            {
                return suit - INDEX_SUITS; // [0, INDEX_NumSuits)
            }

            int numsuitIndex = suit - INDEX_NUMSUITS;
            int rank = BitOperations.TrailingZeroCount((uint)(tile & TileFlags.Ranks));

            return COUNT_RANKLESS + numsuitIndex * COUNT_NUMRANKS + (rank - INDEX_RANKS);
        }

        public static int DoraTileIndex(this TileFlags tile)
        {
            // Dora tiles are always the first tile in the flags.
            // If the tile is a Dora, it must be a single tile.
            if (!tile.UsesOneTile())
                throw new ArgumentException("Dora tile must represent a single tile.", nameof(tile));

            int suit = BitOperations.TrailingZeroCount((uint)(tile & TileFlags.Suits));

            if (suit < INDEX_WINDS + 4) // Must be a tile back or honor tile:
            {
                return ((suit - INDEX_WINDS + 1) % 4) + INDEX_WINDS - INDEX_SUITS;
            }
            else if (suit < INDEX_DRAGONS + 3) // Must be a tile back or honor tile:
            {
                return ((suit - INDEX_DRAGONS + 1) % 3) + INDEX_DRAGONS - INDEX_SUITS;
            }
            else if (suit >= INDEX_NUMSUITS) // Must be a numbered suit tile:
            {
                int numsuitIndex = suit - INDEX_NUMSUITS;
                int rank = BitOperations.TrailingZeroCount((uint)(tile & TileFlags.Ranks));
                return COUNT_RANKLESS + numsuitIndex * COUNT_NUMRANKS + ((rank + 1 - INDEX_RANKS) % COUNT_NUMRANKS);
            }

            throw new ArgumentException("Dora tile must be a numbered suit tile or an honor tile.", nameof(tile));
        }

        public static TileFlags IndexsFlags(int index)
        {
            if (index < 0 || index >= COUNT_UNIQUE_TYPES)
                return default;

            if (index < COUNT_RANKLESS)
                return (TileFlags)(1 << (index + INDEX_SUITS)); // Rankless are always the first suits.

            int numindex = index - COUNT_RANKLESS;
            int numsuit = numindex / COUNT_NUMRANKS;
            int numrank = numindex % COUNT_NUMRANKS;

            return (TileFlags)(1 << (numsuit + INDEX_NUMSUITS)) | (TileFlags)(1 << (numrank + INDEX_RANKS));
        }
    }
}