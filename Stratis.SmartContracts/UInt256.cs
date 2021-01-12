using System.Numerics;

namespace Stratis.SmartContracts
{
    public class uint256 : BigIntegerBase
    {
        const int WIDTH = 32;

        public uint256() : base(WIDTH)
        {
        }

        public uint256(BigInteger value) : base(WIDTH, value)
        {
        }

        public uint256(uint256 value) : this(value.value)
        {
        }

        public uint256(string hex) : base(WIDTH, hex)
        {
        }

        public static uint256 Parse(string hex)
        {
            return new uint256(hex);
        }

        public uint256(ulong b) : base(b)
        {
        }

        public uint256(byte[] vch, bool lendian = true) : base(WIDTH, vch, lendian)
        {
        }

        public static uint256 operator >>(uint256 a, int shift)
        {
            return new uint256(a.ShiftRight(shift));
        }

        public static uint256 operator <<(uint256 a, int shift)
        {
            return new uint256(a.ShiftLeft(shift));
        }

        public static uint256 operator -(uint256 a, uint256 b)
        {
            return new uint256(a.Subtract(b.value));
        }

        public static uint256 operator +(uint256 a, uint256 b)
        {
            return new uint256(a.Add(b.value));
        }

        public static uint256 operator *(uint256 a, uint256 b)
        {
            return new uint256(a.Multiply(b.value));
        }

        public static uint256 operator /(uint256 a, uint256 b)
        {
            return new uint256(a.Divide(b.value));
        }

        public static uint256 operator %(uint256 a, uint256 b)
        {
            return new uint256(a.Mod(b.value));
        }

        public uint256(byte[] vch) : this(vch, true)
        {
        }

        public static bool operator <(uint256 a, uint256 b)
        {
            return Comparison(a, b) < 0;
        }

        public static bool operator >(uint256 a, uint256 b)
        {
            return Comparison(a, b) > 0;
        }

        public static bool operator <=(uint256 a, uint256 b)
        {
            return Comparison(a, b) <= 0;
        }

        public static bool operator >=(uint256 a, uint256 b)
        {
            return Comparison(a, b) >= 0;
        }

        public static bool operator ==(uint256 a, uint256 b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) != ((object)b == null))
                return false;

            return Comparison(a, b) == 0;
        }

        public static bool operator !=(uint256 a, uint256 b)
        {
            return !(a == b);
        }

        public static bool operator ==(uint256 a, ulong b)
        {
            return (a == new uint256(b));
        }

        public static bool operator !=(uint256 a, ulong b)
        {
            return !(a == new uint256(b));
        }

        public static implicit operator uint256(ulong value)
        {
            return new uint256(value);
        }
    }
}