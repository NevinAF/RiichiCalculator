using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Utilities;

namespace RiichiCalculator
{
    public unsafe interface ITileList
    {
        int Capacity { get; }
        int Length { get; set; }
        ref TileFlags this[int index] { get; }
    }

    public unsafe struct TileList_4 : ITileList
    {
        public const int _capacity = 4;
        private byte count;
        private fixed int items[_capacity];

        public readonly int Capacity => _capacity;
        public int Length { readonly get => count; set => count = (byte)value; }

        public ref TileFlags this[int index]
        {
            get {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range [{0}, {count - 1}].");
                return ref Unsafe.As<int, TileFlags>(ref items[index]);
            }
        }

        public override string ToString() => this.Format();
    }

    public unsafe struct TileList_15 : ITileList
    {
        public const int _capacity = 15;
        private byte count;
        private fixed int items[_capacity];

        public readonly int Capacity => _capacity;
        public int Length { readonly get => count; set => count = (byte)value; }

        public ref TileFlags this[int index]
        {
            get {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range [{0}, {count - 1}].");
                return ref Unsafe.As<int, TileFlags>(ref items[index]);
            }
        }

        public override string ToString() => this.Format();
    }

    public unsafe struct TileList_27 : ITileList
    {
        public const int _capacity = 27;
        private byte count;
        private fixed int items[_capacity];

        public readonly int Capacity => _capacity;
        public int Length { readonly get => count; set => count = (byte)value; }

        public ref TileFlags this[int index]
        {
            get {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range [{0}, {count - 1}].");
                return ref Unsafe.As<int, TileFlags>(ref items[index]);
            }
        }

        public override string ToString() => this.Format();
    }

    [StructLayout(LayoutKind.Sequential, Size = (TileExtensions.COUNT_UNIQUE_TYPES + 63) / 64 * sizeof(long))]
    public struct TileMask : BitMask {}

    public unsafe static class TileListExtensions
    {
        public static void Add<T>(this ref T list, TileFlags value) where T : unmanaged, ITileList
        {
            if (list.Length >= list.Capacity)
                throw new InvalidOperationException($"Cannot add more items to the list, it is full ({list.Length}/{list.Capacity}).");
            list[list.Length++] = value;
        }

        public static void Add<T, U>(this ref T list, in U other) where T : unmanaged, ITileList where U : unmanaged, ITileList
        {
            if (list.Length + other.Length > list.Capacity)
                throw new InvalidOperationException($"Cannot add more items to the list, it would exceed the capacity ({list.Length + other.Length}/{list.Capacity}).");

            for (int i = 0; i < other.Length; i++)
            {
                list[list.Length++] = other[i];
            }
        }

        public static void Fill<T>(this ref T list, TileFlags value, int count) where T : unmanaged, ITileList
        {
            list.Length = 0;
            for (int i = 0; i < count && i < list.Capacity; i++)
            {
                list.Add(value);
            }
        }

        public static TileFlags GetFlags<T>(this ref T list) where T : unmanaged, ITileList
        {
            TileFlags flags = default;
            for (int i = 0; i < list.Length; i++)
                flags |= list[i];
            return flags;
        }

        public static string Format<T>(this ref T list) where T : unmanaged, ITileList
        {
            return Format(ref list, 0, list.Length);
        }

        public static string Format<T>(this ref T list, int start, int count) where T : unmanaged, ITileList
        {
            if (count < 0)
                count = list.Length - start;

            if (start < 0 || start + count > list.Length)
                throw new ArgumentOutOfRangeException($"Invalid range: start={start}, count={count}, list.Length={list.Length}");

            string result = "[";
            for (int i = 0; i < count; i++)
            {
                result += "(" + list[start + i].ToString() + ")";
                if (i < count - 1)
                    result += ", ";
            }
            result += "]";
            return result;
        }

        public static void Validate<T, A>(this ref T list, A analysis, string prepend = null) where T : unmanaged, ITileList where A : struct, IAnalysis
        {
            if (!analysis.Enabled)
                return;

            analysis.Assert(list.Length >= 0 && list.Length < list.Capacity, $"{prepend}List length {list.Length} is out of bounds [0, {list.Capacity}).");

            for (int i = 0; i < list.Length; i++)
            {
                TileFlags tile = list[i];
                tile.ValidateAsSingleTile(analysis, $"{prepend}Tile at index {i} is invalid: ");
            }
        }

        public static void Add<T>(this ref TileCounts counts, in T list) where T : unmanaged, ITileList
        {
            for (int i = 0; i < list.Length; i++)
                counts.Add(list[i]);
        }
    }
}