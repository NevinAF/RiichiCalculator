using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Utilities
{
    public interface BitMask {}

    /// <summary>
    /// Extension methods for unmanaged types that can be interpreted as a bit mask.
    /// </summary>
    public unsafe static class BitMaskExtensions
    {
        /// <summary>The number of bits in a ulong. </summary>
        public const int bitsPerLong = sizeof(ulong) * 8;

        /// <summary> The number set bits in the given data. </summary>
        public static int PopCount<T>(in T source) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> data = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in source)), sizeof(T) / sizeof(ulong));

            int count = 0;
            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                count += BitOperations.PopCount(data[i]);
            return count;
        }

        /// <summary> Returns a list of indices representing the set bits in the given data, int ascending order. </summary>
        /// <remarks> This method is faster than <see cref="GetBits_Sparse"> when there is a lot of bits set in the data. </remarks>
        public static int GetBits_Packed<T>(in T source, Span<int> results) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> data = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in source)), sizeof(T) / sizeof(ulong));
            int count = 0;

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
            {
                ulong bits = data[i];
                for (int j = 0; j < bitsPerLong; j++)
                {
                    if ((bits & (1ul << j)) != 0)
                    {
                        if (count < results.Length)
                            results[count++] = i * bitsPerLong + j;
                    }
                }
            }

            return count;
        }

        /// <summary> Returns a list of indices representing the set bits in the given data, int ascending order. </summary>
        /// <remarks> This method is faster than <see cref="GetBits_Packed"> when there are few bits set in the data. </remarks>
        public static int GetBits_Sparse<T>(in T source, Span<int> results) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> data = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in source)), sizeof(T) / sizeof(ulong));

            int count = 0;
            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
            {
                ulong bits = data[i];
                while (bits != 0)
                {
                    int bit = BitOperations.TrailingZeroCount(bits);
                    if (count < results.Length)
                        results[count++] = i * bitsPerLong + bit;
                    bits &= ~(1ul << bit);
                }
            }
            return count;
        }

        /// <summary> Returns the index of the first set bit in the given data, or -1 if none are set. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FirstBit<T>(in T source) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> data = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in source)), sizeof(T) / sizeof(ulong));

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
            {
                ulong bits = data[i];
                if (bits != 0)
                {
                    int trail = BitOperations.TrailingZeroCount(bits);
                    return i * bitsPerLong + trail;
                }
            }

            return -1;
        }

        /// <summary> Returns a new T with the result of ANDing the bits of <paramref name="a"/> and <paramref name="b"/>. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T And<T>(in T a, in T b) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> dataA = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in a)), sizeof(T) / sizeof(ulong));
            ReadOnlySpan<ulong> dataB = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in b)), sizeof(T) / sizeof(ulong));

            T result = default;
            Span<ulong> resultData = MemoryMarshal.CreateSpan(
                ref Unsafe.As<T, ulong>(ref result), sizeof(T) / sizeof(ulong));

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                resultData[i] = dataA[i] & dataB[i];
            return result;
        }

        /// <summary> Returns a new T with the result of ORing the bits of <paramref name="a"/> and <paramref name="b"/>. </summary>
        /// <typeparam name="T"> The type of data. </typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Or<T>(in T a, in T b) where T : unmanaged, BitMask
        {
            T result = a;
            Add(ref result, b);
            return result;
        }

        /// <summary> Returns a new T that is equal to <paramref name="a"/> with the bit at <paramref name="b"/> set. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Or<T>(in T a, int b) where T : unmanaged, BitMask
        {
            T result = a;
            Add(ref result, b);
            return result;
        }

        /// <summary> Modifies <paramref name="a"/>, setting the <paramref name="b"/>'th bit </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this ref T a, int b) where T : unmanaged, BitMask
        {
            #if DEBUG
            if (b >= sizeof(T) * 8) throw new System.ArgumentException("Bit index out of range.");
            #endif

            Span<ulong> data = MemoryMarshal.CreateSpan(
                ref Unsafe.As<T, ulong>(ref a), sizeof(T) / sizeof(ulong));

            data[b / bitsPerLong] |= 1ul << (b % bitsPerLong);
        }

        /// <summary> Modifies <paramref name="a"/> by ANDing it with <paramref name="b"/> </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this ref T a, in T b) where T : unmanaged, BitMask
        {
            Span<ulong> dataA = MemoryMarshal.CreateSpan(
                ref Unsafe.As<T, ulong>(ref a), sizeof(T) / sizeof(ulong));
            ReadOnlySpan<ulong> bData = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in b)), sizeof(T) / sizeof(ulong));

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                dataA[i] |= bData[i];
        }

        /// <summary> Modifies <paramref name="a"/>, clearing the <paramref name="b"/>'th bit </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T>(this ref T a, int b) where T : unmanaged, BitMask
        {
            #if DEBUG
            if (b >= sizeof(T) * 8) throw new System.ArgumentException("Bit index out of range.");
            #endif

            Span<ulong> data = MemoryMarshal.CreateSpan(
                ref Unsafe.As<T, ulong>(ref a), sizeof(T) / sizeof(ulong));
            data[b / bitsPerLong] &= ~(1ul << (b % bitsPerLong));
        }

        /// <summary> Modifies <paramref name="a"/> by ANDing it with NOT <paramref name="b"/> </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T>(this ref T a, in T b) where T : unmanaged, BitMask
        {
            Span<ulong> dataA = MemoryMarshal.CreateSpan(
                ref Unsafe.As<T, ulong>(ref a), sizeof(T) / sizeof(ulong));
            ReadOnlySpan<ulong> bData = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in b)), sizeof(T) / sizeof(ulong));

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                dataA[i] &= ~bData[i];
        }

        /// <summary> Modifies <paramref name="a"/> by ANDing it with <paramref name="b"/> </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Mask<T>(this ref T a, in T b) where T : unmanaged, BitMask
        {
            Span<ulong> dataA = MemoryMarshal.CreateSpan(
                ref Unsafe.As<T, ulong>(ref a), sizeof(T) / sizeof(ulong));
            ReadOnlySpan<ulong> bData = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in b)), sizeof(T) / sizeof(ulong));

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                dataA[i] &= bData[i];
        }

        /// <summary> True if <paramref name="a"/> contains no set bits. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BitsEmpty<T>(in T a) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> data = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in a)), sizeof(T) / sizeof(ulong));
            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                if (data[i] != 0)
                    return false;
            return true;
        }

        /// <summary> True if <paramref name="a"/> contains any set bits. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyBits<T>(in T a) where T : unmanaged, BitMask => !BitsEmpty(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreEqual<T>(in T a, in T b) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> dataA = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in a)), sizeof(T) / sizeof(ulong));
            ReadOnlySpan<ulong> dataB = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in b)), sizeof(T) / sizeof(ulong));

            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
                if (dataA[i] != dataB[i])
                    return false;
            return true;
        }

        /// <summary> Returns a hex string representing the bits in <paramref name="a"/>. </summary>
        public static string Hex<T>(ref T a) where T : unmanaged, BitMask
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(
                MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref a), sizeof(T) / sizeof(byte))
            );

            Span<char> charBuffer = stackalloc char[bytes.Length * 3 - 1]; // Max length: 2 chars per byte + 1 space between
            int charIndex = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i].TryFormat(charBuffer.Slice(charIndex, 2), out int charsWritten, "X2");
                charIndex += charsWritten;
                if (i < bytes.Length - 1)
                {
                    charBuffer[charIndex++] = ' ';
                }
            }
            return charBuffer.Slice(0, charIndex).ToString();
        }

        public static int GetHashCode<T>(in T a) where T : unmanaged, BitMask
        {
            ReadOnlySpan<ulong> data = MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.As<T, ulong>(ref Unsafe.AsRef(in a)), sizeof(T) / sizeof(ulong));

            int hash = 17;
            for (int i = 0; i < sizeof(T) / sizeof(ulong); i++)
            {
                hash = hash * 31 + data[i].GetHashCode();
            }
            return hash;
        }
    }
}