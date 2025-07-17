using System;

namespace RiichiCalculator
{
    public unsafe struct TileCounts
    {
        private fixed byte counts[TileExtensions.COUNT_UNIQUE_TYPES];
        public ref byte this[int index]
        {
            get
            {
                if (index < 0 || index >= TileExtensions.COUNT_UNIQUE_TYPES)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range [0, {TileExtensions.COUNT_UNIQUE_TYPES - 1}].");
                return ref counts[index];
            }
        }

        public ref byte this[TileFlags tile]
        {
            get => ref this[tile.TileIndex()];
        }

        public void Add(TileFlags tile)
        {
            counts[tile.TileIndex()]++;
        }

        public void Add(in TileCounts other)
        {
            for (int i = 0; i < TileExtensions.COUNT_UNIQUE_TYPES; i++)
                counts[i] += other[i];
        }

        public void Validate<T>(T analysis, string prepend) where T : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            for (int i = 0; i < TileExtensions.COUNT_UNIQUE_TYPES; i++)
            {
                byte count = this[i];
                analysis.Assert(count >= 0 && count <= 4, $"{prepend}Tile {TileExtensions.IndexsFlags(i)} count appears {count} times. There must be at most 4 of them.");
            }
        }

        public override string ToString()
        {
            string result = "[";
            for (int i = 0; i < TileExtensions.COUNT_UNIQUE_TYPES; i++)
            {
                if (counts[i] > 0)
                {
                    result += $"{TileExtensions.IndexsFlags(i)}: {counts[i]}";
                    if (i < TileExtensions.COUNT_UNIQUE_TYPES - 1)
                        result += ", ";
                }
            }
            result += "]";
            return result;
        }
    }
}