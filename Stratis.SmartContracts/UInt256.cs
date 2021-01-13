using System;
using System.Numerics;

namespace Stratis.SmartContracts
{
    public struct UInt256 : IComparable
    {
        const int WIDTH = 32;

        private UIntBase value;

        public static UInt256 Zero = 0;
        public static UInt256 MinValue = 0;
        public static UInt256 MaxValue = new UInt256((BigInteger.One << (WIDTH * 8)) - 1);

        public UInt256(BigInteger value)
        {
            this.value = new UIntBase(WIDTH, value);
        }

        public UInt256(UInt256 value)
        {
            this.value = new UIntBase(WIDTH, value);
        }

        public UInt256(string hex)
        {
            this.value = new UIntBase(WIDTH, hex);
        }

        public static UInt256 Parse(string hex)
        {
            return new UInt256(hex);
        }

        public UInt256(ulong b)
        {
            this.value = new UIntBase(WIDTH, b);
        }

        public UInt256(byte[] vch, bool lendian = true)
        {
            this.value = new UIntBase(WIDTH, vch, lendian);
        }

        public static UInt256 operator >>(UInt256 a, int shift)
        {
            return new UInt256(a.value.ShiftRight(shift));
        }

        public static UInt256 operator <<(UInt256 a, int shift)
        {
            return new UInt256(a.value.ShiftLeft(shift));
        }

        public static UInt256 operator -(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Subtract(b.value.GetValue()));
        }

        public static UInt256 operator +(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Add(b.value.GetValue()));
        }

        public static UInt256 operator *(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Multiply(b.value.GetValue()));
        }

        public static UInt256 operator /(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Divide(b.value.GetValue()));
        }

        public static UInt256 operator %(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Mod(b.value.GetValue()));
        }

        public UInt256(byte[] vch) : this(vch, true)
        {
        }

        public static bool operator <(UInt256 a, UInt256 b)
        {
            return UIntBase.Comparison(a.value, b.value) < 0;
        }

        public static bool operator >(UInt256 a, UInt256 b)
        {
            return UIntBase.Comparison(a.value, b.value) > 0;
        }

        public static bool operator <=(UInt256 a, UInt256 b)
        {
            return UIntBase.Comparison(a.value, b.value) <= 0;
        }

        public static bool operator >=(UInt256 a, UInt256 b)
        {
            return UIntBase.Comparison(a.value, b.value) >= 0;
        }

        public static bool operator ==(UInt256 a, UInt256 b)
        {
            return UIntBase.Comparison(a.value, b.value) == 0;
        }

        public static bool operator !=(UInt256 a, UInt256 b)
        {
            return !(a == b);
        }

        public static implicit operator UInt256(ulong value)
        {
            return new UInt256(value);
        }

        public static implicit operator UInt256(long value)
        {
            if (value < 0)
                throw new ArgumentException("Only positive or zero values are allowed.", nameof(value));

            return new UInt256((ulong)value);
        }
        
        public static implicit operator UInt256(int value)
        {
            if (value < 0)
                throw new ArgumentException("Only positive or zero values are allowed.", nameof(value));

            return new UInt256((ulong)value);
        }
        
        public static implicit operator ulong(UInt256 value)
        {
            return (ulong)value.value.GetValue();
        }

        public byte[] ToBytes()
        {
            return this.value.ToBytes();
        }

        public int CompareTo(object b)
        {
            return this.value.CompareTo(((UInt256)b).value);
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