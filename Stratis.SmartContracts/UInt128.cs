using System.Numerics;

namespace Stratis.SmartContracts
{
    public class UInt128 : UIntBase
    {
        const int WIDTH = 16;

        public UInt128() : base(WIDTH)
        {
        }

        public UInt128(BigInteger value) : base(WIDTH, value)
        {
        }

        public UInt128(UInt128 value) : this(value.value)
        {
        }

        public UInt128(string hex) : base(WIDTH, hex)
        {
        }

        public static UInt128 Parse(string hex)
        {
            return new UInt128(hex);
        }

        public UInt128(ulong b) : base(WIDTH, b)
        {
        }

        public UInt128(byte[] vch, bool lendian = true) : base(WIDTH, vch, lendian)
        {
        }

        public static UInt128 operator >>(UInt128 a, int shift)
        {
            return new UInt128(a.ShiftRight(shift));
        }

        public static UInt128 operator <<(UInt128 a, int shift)
        {
            return new UInt128(a.ShiftLeft(shift));
        }

        public static UInt128 operator -(UInt128 a, UInt128 b)
        {
            return new UInt128(a.Subtract(b.value));
        }

        public static UInt128 operator +(UInt128 a, UInt128 b)
        {
            return new UInt128(a.Add(b.value));
        }

        public static UInt128 operator *(UInt128 a, UInt128 b)
        {
            return new UInt128(a.Multiply(b.value));
        }

        public static UInt128 operator /(UInt128 a, UInt128 b)
        {
            return new UInt128(a.Divide(b.value));
        }

        public static UInt128 operator %(UInt128 a, UInt128 b)
        {
            return new UInt128(a.Mod(b.value));
        }

        public UInt128(byte[] vch) : this(vch, true)
        {
        }

        public static bool operator <(UInt128 a, UInt128 b)
        {
            return Comparison(a, b) < 0;
        }

        public static bool operator >(UInt128 a, UInt128 b)
        {
            return Comparison(a, b) > 0;
        }

        public static bool operator <=(UInt128 a, UInt128 b)
        {
            return Comparison(a, b) <= 0;
        }

        public static bool operator >=(UInt128 a, UInt128 b)
        {
            return Comparison(a, b) >= 0;
        }

        public static bool operator ==(UInt128 a, UInt128 b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) != ((object)b == null))
                return false;

            return Comparison(a, b) == 0;
        }

        public static bool operator !=(UInt128 a, UInt128 b)
        {
            return !(a == b);
        }

        public static bool operator ==(UInt128 a, ulong b)
        {
            return (a == new UInt128(b));
        }

        public static bool operator !=(UInt128 a, ulong b)
        {
            return !(a == new UInt128(b));
        }

        public static implicit operator UInt128(ulong value)
        {
            return new UInt128(value);
        }

        public static implicit operator ulong(UInt128 value)
        {
            return (ulong)value.value;
        }
    }
}