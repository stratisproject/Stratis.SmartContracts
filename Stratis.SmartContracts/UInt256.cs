using System.Numerics;

namespace Stratis.SmartContracts
{
    public class UInt256 : UIntBase
    {
        const int WIDTH = 32;

        public UInt256() : base(WIDTH)
        {
        }

        public UInt256(BigInteger value) : base(WIDTH, value)
        {
        }

        public UInt256(UInt256 value) : this(value.value)
        {
        }

        public UInt256(string hex) : base(WIDTH, hex)
        {
        }

        public static UInt256 Parse(string hex)
        {
            return new UInt256(hex);
        }

        public UInt256(ulong b) : base(WIDTH, b)
        {
        }

        public UInt256(byte[] vch, bool lendian = true) : base(WIDTH, vch, lendian)
        {
        }

        public static UInt256 operator >>(UInt256 a, int shift)
        {
            return new UInt256(a.ShiftRight(shift));
        }

        public static UInt256 operator <<(UInt256 a, int shift)
        {
            return new UInt256(a.ShiftLeft(shift));
        }

        public static UInt256 operator -(UInt256 a, UInt256 b)
        {
            return new UInt256(a.Subtract(b.value));
        }

        public static UInt256 operator +(UInt256 a, UInt256 b)
        {
            return new UInt256(a.Add(b.value));
        }

        public static UInt256 operator *(UInt256 a, UInt256 b)
        {
            return new UInt256(a.Multiply(b.value));
        }

        public static UInt256 operator /(UInt256 a, UInt256 b)
        {
            return new UInt256(a.Divide(b.value));
        }

        public static UInt256 operator %(UInt256 a, UInt256 b)
        {
            return new UInt256(a.Mod(b.value));
        }

        public UInt256(byte[] vch) : this(vch, true)
        {
        }

        public static bool operator <(UInt256 a, UInt256 b)
        {
            return Comparison(a, b) < 0;
        }

        public static bool operator >(UInt256 a, UInt256 b)
        {
            return Comparison(a, b) > 0;
        }

        public static bool operator <=(UInt256 a, UInt256 b)
        {
            return Comparison(a, b) <= 0;
        }

        public static bool operator >=(UInt256 a, UInt256 b)
        {
            return Comparison(a, b) >= 0;
        }

        public static bool operator ==(UInt256 a, UInt256 b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) != ((object)b == null))
                return false;

            return Comparison(a, b) == 0;
        }

        public static bool operator !=(UInt256 a, UInt256 b)
        {
            return !(a == b);
        }

        public static bool operator ==(UInt256 a, ulong b)
        {
            return (a == new UInt256(b));
        }

        public static bool operator !=(UInt256 a, ulong b)
        {
            return !(a == new UInt256(b));
        }

        public static implicit operator UInt256(ulong value)
        {
            return new UInt256(value);
        }
    }
}