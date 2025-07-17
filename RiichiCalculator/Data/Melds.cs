using System;

namespace RiichiCalculator
{
    public class Melds
    {
        public const int Capacity = 4;

        private byte count;
        private TileList_4 meld0, meld1, meld2, meld3;

        public int Length { get => count; set => count = (byte)value; }
        public ref TileList_4 this[int index]
        {
            get {
                switch (index)
                {
                    case 0: return ref meld0;
                    case 1: return ref meld1;
                    case 2: return ref meld2;
                    case 3: return ref meld3;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range [0, {Capacity - 1}].");
                }
            }
        }

        public void Add(in TileList_4 meld)
        {
        #if DEBUG
            if (count >= Capacity)
                throw new InvalidOperationException($"Cannot add more melds to the list, it is full ({count}/{Capacity}).");
        #endif
            this[count++] = meld;
        }

        public void Validate<T>(T analysis, string prepend) where T : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            analysis.Assert(count >= 0 && count <= Capacity, $"{prepend}Melds count {count} is out of bounds [0, {Capacity}].");

            TileFlags seat = TileFlags.Seats;
            for (int meld = 0; meld < count; meld++)
            {
                ref TileList_4 tiles = ref this[meld];
                tiles.Validate(analysis, prepend + $"Meld at index {meld}: ");
                analysis.Assert(tiles.Length is 3 or 4, $"{prepend}A meld must always contain 3 (sequence or triplet) or 4 (quad) tiles: {tiles}");

                TileFlags meldSeat;
                TileFlags flags = tiles.GetFlags();
                if (tiles.Length == 3)
                {
                    analysis.Assert(flags.HasAnyOf(TileFlags.Seats), $"{prepend}Meld at index {meld} has three tiles and only contains tiles from a single seat. Only concealed quads can be melded without an open tiles. Meld: {tiles}");
                    analysis.Assert(flags.HasExactlyTwoSeats(), $"{prepend}Meld at index {meld} has tiles from three different seats. Melds can only contain the player's seat and one called tile's seat (if applicable): {tiles}");

                    analysis.Assert(flags.HasExactlyOneOf(TileFlags.Suits), prepend + $"Meld at index {meld} contains more than one suit, making it not a sequence or triplet or quad: " + flags);
                    analysis.Assert(flags.HasExactlyOneOf(TileFlags.Ranks) || flags.UsesOneSequence(), prepend + $"Meld at index {meld} contains more than one rank, but those ranks do not form a sequence: " + flags);
                    analysis.Assert(flags.HasNoneOf(TileFlags.Back), prepend + $"Meld at index {meld} contains back tiles, which is not allowed for sequences or triplets: " + flags);

                    if ((tiles[0] & tiles[1]).HasExactlyOneOf(TileFlags.Seats)) 
                        meldSeat = tiles[0] & TileFlags.Seats;
                    else if ((tiles[0] & tiles[2]).HasExactlyOneOf(TileFlags.Seats))
                        meldSeat = tiles[0] & TileFlags.Seats;
                    else // if ((tiles[1] & tiles[2]).HasExactlyOneOf(TileFlags.Seats))
                        meldSeat = tiles[1] & TileFlags.Seats;
                }
                else {
                    flags.ValidateAsQuad(analysis, prepend + $"Meld at index {meld}: ");

                    meldSeat = flags & TileFlags.Seats;
                }

                analysis.Assert(meldSeat.HasAnyOf(seat), $"{prepend}Meld at index {meld} does not match other meld seats. Inferred seat: {seat}, Meld's seat: {meldSeat}");
                seat &= meldSeat; // Combine seats from melds
            }
        }

        public override string ToString()
        {
            string result = "[";
            for (int i = 0; i < count; i++)
            {
                result += this[i].ToString();
                if (i < count - 1)
                    result += ", ";
            }
            result += "]";
            return result;
        }
    }

    public static class MeldsExtensions
    {
        public static void Add(this ref TileCounts counts, in Melds melds)
        {
            for (int i = 0; i < melds.Length; i++)
            {
                counts.Add(melds[i]);
            }
        }
    }
}