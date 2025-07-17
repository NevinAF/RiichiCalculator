using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Utilities;

namespace RiichiCalculator
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class WinnableAttribute : Attribute
    {
        public string NameEN { get; set; }
        public string NameJA { get; set; }
        public string Desc { get; set; }
        public string Details { get; set; }
        public int OpenHand { get; set; } = ScoreBasis.None;
        public int ClosedHand { get; set; } = ScoreBasis.None;
    }

    [StructLayout(LayoutKind.Explicit, Size = (WinCatalog.Capacity + 63) / 64 * 8)]
    public struct Wins : BitMask {
        // public static Wins operator |(Wins a, Wins b)
        // {
        //     return a.OrBits(b);
        // }

        public Wins(params int[] bits)
        {
            this = default;
            for (int i = 0; i < bits.Length; i++)
                this.Add(bits[i]);
        }

        public readonly bool Empty => BitMaskExtensions.BitsEmpty(this);
        public readonly void GetBits(ref Span<int> indices)
        {
            int count = BitMaskExtensions.GetBits_Sparse(this, indices);
            indices = indices[..count];
        }

        public static Wins operator &(Wins a, Wins b)
        {
            return BitMaskExtensions.And(a, b);
        }

        public static Wins operator |(Wins a, int b) => BitMaskExtensions.Or(a, b);
        public static Wins operator |(int a, Wins b) => BitMaskExtensions.Or(b, a);
        public static Wins operator |(Wins a, Wins b) => BitMaskExtensions.Or(a, b);

        public static implicit operator Wins(int w)
        {
            Wins result = default;
            result.Add(w);
            return result;
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is not Wins wins)
                return false;

            return BitMaskExtensions.AreEqual(this, wins);
        }

        public override readonly int GetHashCode()
        {
            // Assuming BitMaskExtensions provides a suitable hash code implementation
            return BitMaskExtensions.GetHashCode(this);
        }

        public override readonly string ToString()
        {
            Span<int> indices = stackalloc int[WinCatalog.Capacity];
            GetBits(ref indices);

            if (indices.Length == 0)
                return "<None>";

            string result = string.Empty;
            for (int i = 0; i < indices.Length; i++)
            {
                if (i > 0)
                    result += ", ";
                result += WinCatalog._entries[indices[i]].NameEN;
            }

            return result;
        }
    }

    /// <summary>
    /// A catalog of all possible minipoints and yaku wins that can be scored for a hand. All entries are defined as literal integers (cont) with a WinnableAttribute. The literal value is used as the index of described win, and the attribute defines the properties of the win, such as name, description, and points awards.
    /// The first time this class is accessed (thus the static constructor is called), and exception will be thrown if any defined wins have overlapping indices, or if the index is out of range.
    /// This class contains readonly static fields and methods for retrieving the score or winnables from a Wins mask.
    /// </summary>
    public static partial class WinCatalog
    {
        public static Wins None => default;
        public const int Capacity = 128;

        public readonly static WinnableAttribute[] _entries;
        public readonly static Wins open_mask;
        public readonly static ScoreBasis[] open_basis;
        public readonly static Wins closed_mask;
        public readonly static ScoreBasis[] closed_basis;

        static WinCatalog()
        {
            var indices = typeof(WinCatalog).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            _entries = new WinnableAttribute[Capacity];
            open_mask = new Wins();
            open_basis = new ScoreBasis[Capacity];
            closed_mask = new Wins();
            closed_basis = new ScoreBasis[Capacity];

            for (int windex = 0; windex < indices.Length; windex++)
            {
                var field = indices[windex];
                if (!field.IsLiteral || field.FieldType != typeof(int))
                    continue;

                var attr = field.GetCustomAttribute<WinnableAttribute>();
                if (attr == null)
                    continue;

                var index = (int)field.GetValue(null);

                if (index < 0 || index >= Capacity)
                    Trace.Fail($"Winnable {field.Name} at index {index} is out of range [0, {Capacity - 1}]. Increase capacity or change to a valid index.");
                if (_entries[index] != null)
                    Trace.Fail($"Winnable {field.Name} at index {index} clashes with already defined win \"{_entries[index].NameEN}\". All winnables must use unique indices in the range [0, {Capacity - 1}].");

                _entries[index] = attr;

                if (attr.OpenHand >= 0) {
                    open_mask.Add(index);
                    open_basis[index] = (ScoreBasis)attr.OpenHand;
                }
                if (attr.ClosedHand >= 0) {
                    closed_mask.Add(index);
                    closed_basis[index] = (ScoreBasis)attr.ClosedHand;
                }
            }
        }

        public static ScoreBasis ScoreOpenWins(ref Wins wins)
        {
            wins.Mask(open_mask);
            if (wins.Empty)
                return default;

            Span<int> indices = stackalloc int[Capacity];
            wins.GetBits(ref indices);

            ScoreBasis result = default;
            for (int i = 0; i < indices.Length; i++)
                result += open_basis[indices[i]];

            return result;
        }

        public static ScoreBasis ScoreClosedWins(ref Wins wins)
        {
            wins.Mask(closed_mask);
            if (wins.Empty)
                return default;

            Span<int> indices = stackalloc int[Capacity];
            wins.GetBits(ref indices);

            ScoreBasis result = default;
            for (int i = 0; i < indices.Length; i++)
                result += closed_basis[indices[i]];

            return result;
        }

        public static int GetWinAttributes(Span<WinnableAttribute> buffer, in Wins wins)
        {
            if (wins.Empty)
                return 0;

            int count = 0;
            Span<int> indices = stackalloc int[Capacity];
            wins.GetBits(ref indices);

            for (int i = 0; i < indices.Length; i++, count++)
            {
            #if DEBUG
                if (indices[i] >= 0 && indices[i] < _entries.Length)
                {
                    Trace.Fail($"Win index {indices[i]} from wins mask is out of range [0, {_entries.Length - 1}].");
                    count--;
                    continue;
                }
            #endif

                if (count < buffer.Length)
                    buffer[count] = _entries[indices[i]];
            }

            return count;
        }
    }
}