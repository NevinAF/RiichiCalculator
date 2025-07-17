using System.Runtime.CompilerServices;

namespace Utilities
{
	public unsafe static class BitOperations
	{
		public struct Log2DeBruijnTable
		{
			public fixed byte Log2DeBruijn[32];

			public Log2DeBruijnTable(byte[] bytes)
			{
				fixed (byte* ptr = Log2DeBruijn)
				{
					for (int i = 0; i < 32; i++)
					{
						ptr[i] = bytes[i];
					}
				}
			}
		}
;

		private static Log2DeBruijnTable TrailingLog2DeBruijn = new Log2DeBruijnTable(new byte[32]
		{
			00, 01, 28, 02, 29, 14, 24, 03,
			30, 22, 20, 15, 25, 17, 04, 08,
			31, 27, 13, 23, 21, 19, 16, 07,
			26, 12, 18, 06, 11, 05, 10, 09
		});

		private static Log2DeBruijnTable Log2DeBruijn = new Log2DeBruijnTable(new byte[32]
		{
			00, 09, 01, 10, 13, 21, 02, 29,
			11, 14, 16, 18, 22, 25, 03, 30,
			08, 12, 20, 28, 15, 17, 24, 07,
			19, 27, 23, 06, 26, 05, 04, 31
		});

		/// <summary>
		/// Returns the population count (number of bits set) of a mask.
		/// Similar in behavior to the x86 instruction POPCNT.
		/// </summary>
		/// <param name="value">The value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(uint value)
		{
			const uint c1 = 0x_55555555u;
			const uint c2 = 0x_33333333u;
			const uint c3 = 0x_0F0F0F0Fu;
			const uint c4 = 0x_01010101u;

			value -= (value >> 1) & c1;
			value = (value & c2) + ((value >> 2) & c2);
			value = (((value + (value >> 4)) & c3) * c4) >> 24;

			return (int)value;
		}

		/// <summary>
		/// Returns the population count (number of bits set) of a mask.
		/// Similar in behavior to the x86 instruction POPCNT.
		/// </summary>
		/// <param name="value">The value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(ulong value)
		{
#if TARGET_32BIT
			return PopCount((uint)value) // lo
				+ PopCount((uint)(value >> 32)); // hi
#else
			const ulong c1 = 0x_55555555_55555555ul;
			const ulong c2 = 0x_33333333_33333333ul;
			const ulong c3 = 0x_0F0F0F0F_0F0F0F0Ful;
			const ulong c4 = 0x_01010101_01010101ul;

			value -= (value >> 1) & c1;
			value = (value & c2) + ((value >> 2) & c2);
			value = (((value + (value >> 4)) & c3) * c4) >> 56;

			return (int)value;
#endif
		}

		/// <summary>
		/// Returns the integer (floor) log of the specified value, base 2.
		/// Note that by convention, input value 0 returns 0 since Log(0) is undefined.
		/// Does not directly use any hardware intrinsics, nor does it incur branching.
		/// </summary>
		/// <param name="value">The value.</param>
		private static int Log2SoftwareFallback(uint value)
		{
			// No AggressiveInlining due to large method size
			// Has conventional contract 0->0 (Log(0) is undefined)

			// Fill trailing zeros with ones, eg 00010010 becomes 00011111
			value |= value >> 01;
			value |= value >> 02;
			value |= value >> 04;
			value |= value >> 08;
			value |= value >> 16;

			// // uint.MaxValue >> 27 is always in range [0 - 31] so we use Unsafe.AddByteOffset to avoid bounds check
			// return Unsafe.AddByteOffset(
			// 	// Using deBruijn sequence, k=2, n=5 (2^5=32) : 
			// 	ref MemoryMarshal.GetReference(0b_0000_0111_1100_0100_1010_1100_1101_1101u),
			// 	// uint|long -> IntPtr cast on 32-bit platforms does expensive overflow checks not needed here
			// 	(IntPtr)(int)((value * 0x07C4ACDDu) >> 27));

			return (int)Log2DeBruijn.Log2DeBruijn[(value * 0x_07C4ACDDu) >> 27];
		}

		/// <summary>
		/// Count the number of leading zero bits in a mask.
		/// </summary>
		/// <param name="value">The value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(ulong value)
		{

			uint hi = (uint)(value >> 32);

			if (hi == 0)
			{
				return 32 + LeadingZeroCount((uint)value);
			}

			return LeadingZeroCount(hi);
		}

		/// <summary>
		/// Count the number of leading zero bits in a mask.
		/// </summary>
		/// <param name="value">The value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(uint value)
		{
			if (value == 0)
			{
				return 32;
			}

			return 31 ^ Log2SoftwareFallback(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(uint value)
		{
			// Unguarded fallback contract is 0->0, BSF contract is 0->undefined
			if (value == 0)
			{
				return 32;
			}

			return (int)TrailingLog2DeBruijn.Log2DeBruijn[((value & (uint)-(int)value) * 0x077CB531u) >> 27];
		}

		/// <summary>
		/// Count the number of trailing zero bits in a mask.
		/// Similar in behavior to the x86 instruction TZCNT.
		/// </summary>
		/// <param name="value">The value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(ulong value)
		{
			uint lo = (uint)value;

			if (lo == 0)
			{
				return 32 + TrailingZeroCount((uint)(value >> 32));
			}

			return TrailingZeroCount(lo);
		}

		/// <summary>
		/// Checks if the value is a power of two.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>True if the value is a power of two, false otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPowerOfTwo(uint value)
		{
			// Unguarded fallback contract is 0->false, BSF contract is 0->true
			if (value == 0)
			{
				return false;
			}

			return (value & (value - 1)) == 0;
		}

		/// <summary>
		/// Checks if the value is a power of two.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>True if the value is a power of two, false otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPowerOfTwo(ulong value)
		{
			// Unguarded fallback contract is 0->false, BSF contract is 0->true
			if (value == 0)
			{
				return false;
			}

			return (value & (value - 1)) == 0;
		}
	}
}