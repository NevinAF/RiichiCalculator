using System.Runtime.InteropServices;

namespace RiichiCalculator
{
    // [DebuggerTypeProxy(typeof(ScoreBasisDebugView))]
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct ScoreBasis {
        public const int None = 0;
        public const int TwoFu = 2;
        public const int FourFu = 4;
        public const int EightFu = 8;
        public const int SixteenFu = 16;
        public const int TwentyFu = 20;
        public const int TwentyTwoFu = 22;
        public const int TwentyFiveFu = 25;
        public const int ThirtyFu = 30;
        public const int ThirtyTwoFu = 32;
        public const int OneHan = 1 << 16;
        public const int TwoHan = 2 << 16;
        public const int ThreeHan = 3 << 16;
        public const int FourHan = 4 << 16;
        public const int FiveHan = 5 << 16;
        public const int SixHan = 6 << 16;
        public const int Mangan = FiveHan;
        public const int OneYakuman = 1 << 24;

        [FieldOffset(0)]
        private int _value;

        [FieldOffset(0)]
        public ushort Fu;
        [FieldOffset(2)]
        public byte Han;
        [FieldOffset(3)]
        public byte Yakuman;

        public readonly int Points {
            get {
                if (_value == 0)
                    return 0;


                if (Yakuman > 0)
                    return 8000 * Yakuman;

                switch (Han)
                {
                    case 5: return 2000;
                    case 6:
                    case 7: return 3000;
                    case 8:
                    case 9:
                    case 10: return 4000;
                    case 11:
                    case 12: return 6000;
                    default:
                        if (Han >= 13)
                            return 8000;
                        break;
                }

                int result = Fu != 25
                    ? (Fu + 9) / 10 * 10 // Round up to the nearest ten.
                    : 25; // 25 fu from seven pairs is always 25 points.

                result *= 1 << (Han + 2); // fu ^ (2+han)
                return result >= 1920
                    ? 2000 // Cap at 2000 points (mangan).
                    : (result + 99) / 100 * 100; // Round up to the nearest hundred.
            }
        }

        public static explicit operator ScoreBasis(int value)
        {
            return new ScoreBasis { _value = value };
        }

        public static bool operator ==(ScoreBasis a, ScoreBasis b)
        {
            return a._value == b._value;
        }

        public static bool operator !=(ScoreBasis a, ScoreBasis b)
        {
            return a._value != b._value;
        }

        public override readonly int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is not ScoreBasis basis)
                return false;

            return _value == basis._value;
        }

        public static ScoreBasis operator +(ScoreBasis a, ScoreBasis b)
        {
            return new ScoreBasis { _value = a._value + b._value };
        }

        public static bool operator <(ScoreBasis a, ScoreBasis b)
        {
            return a.Points < b.Points;
        }

        public static bool operator >(ScoreBasis a, ScoreBasis b)
        {
            return a.Points > b.Points;
        }

        public static bool operator <=(ScoreBasis a, ScoreBasis b)
        {
            return a.Points <= b.Points;
        }

        public static bool operator >=(ScoreBasis a, ScoreBasis b)
        {
            return a.Points >= b.Points;
        }

        public override readonly string ToString()
        {
            return $"{Yakuman}yak {Han}han {Fu}fu ({Points}pts)";
        }
    }
}