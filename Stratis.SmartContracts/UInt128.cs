using System;
using System.Numerics;

namespace Stratis.SmartContracts
{
    public struct UInt128
    {
        const int WIDTH = 16;

        private UIntBase value;

        public static UInt256 Zero = 0;
        public static UInt256 MinValue = 0;
        public static UInt256 MaxValue = new UInt256((BigInteger.One << (WIDTH * 8)) - 1);

        public UInt128(BigInteger value)
        {
            this.value = new UIntBase(WIDTH, value);
        }

        public UInt128(UInt256 value)
        {
            this.value = new UIntBase(WIDTH, value);
        }

        public UInt128(string hex)
        {
            this.value = new UIntBase(WIDTH, hex);
        }

        public static UInt128 Parse(string hex)
        {
            return new UInt128(hex);
        }

        public UInt128(ulong b)
        {
            this.value = new UIntBase(WIDTH, b);
        }

        public UInt128(byte[] vch, bool lendian = true)
        {
            this.value = new UIntBase(WIDTH, vch, lendian);
        }

        public static UInt128 operator >>(UInt128 a, int shift)
        {
            return new UInt128(a.value.ShiftRight(shift));
        }

        public static UInt128 operator <<(UInt128 a, int shift)
        {
            return new UInt128(a.value.ShiftLeft(shift));
        }

        public static UInt128 operator -(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Subtract(b.value.GetValue()));
        }

        public static UInt128 operator +(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Add(b.value.GetValue()));
        }

        public static UInt128 operator *(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Multiply(b.value.GetValue()));
        }

        public static UInt128 operator /(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Divide(b.value.GetValue()));
        }

        public static UInt128 operator %(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Mod(b.value.GetValue()));
        }

        public UInt128(byte[] vch) : this(vch, true)
        {
        }

        public static bool operator <(UInt128 a, UInt128 b)
        {
            return UIntBase.Comparison(a.value, b.value) < 0;
        }

        public static bool operator >(UInt128 a, UInt128 b)
        {
            return UIntBase.Comparison(a.value, b.value) > 0;
        }

        public static bool operator <=(UInt128 a, UInt128 b)
        {
            return UIntBase.Comparison(a.value, b.value) <= 0;
        }

        public static bool operator >=(UInt128 a, UInt128 b)
        {
            return UIntBase.Comparison(a.value, b.value) >= 0;
        }

        public static bool operator ==(UInt128 a, UInt128 b)
        {
            return UIntBase.Comparison(a.value, b.value) == 0;
        }

        public static bool operator !=(UInt128 a, UInt128 b)
        {
            return !(a == b);
        }

        public static implicit operator UInt128(ulong value)
        {
            return new UInt128(value);
        }

        public static implicit operator UInt128(long value)
        {
            if (value < 0)
                throw new ArgumentException("Only positive or zero values are allowed.", nameof(value));

            return new UInt128((ulong)value);
        }

        public static implicit operator UInt128(int value)
        {
            if (value < 0)
                throw new ArgumentException("Only positive or zero values are allowed.", nameof(value));

            return new UInt128((ulong)value);
        }

        public static implicit operator ulong(UInt128 value)
        {
            return (ulong)value.value.GetValue();
        }

        public byte[] ToBytes()
        {
            return this.value.ToBytes();
        }

        public int CompareTo(object b)
        {
            return this.value.CompareTo(((UInt128)b).value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }
    }
}