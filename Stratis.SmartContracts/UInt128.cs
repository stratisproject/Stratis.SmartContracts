﻿using System.Numerics;

namespace NBitcoin
{
    public abstract class BigInteger128 : BigIntegerBase
    {
        const int WIDTH = 16;

        public BigInteger128() : base(WIDTH)
        {
        }

        public BigInteger128(ulong b) : base(WIDTH, b)
        {
        }

        public BigInteger128(string hex) : base(WIDTH, hex)
        {
        }

        public BigInteger128(BigInteger value) : base(WIDTH, value)
        {
        }

        public BigInteger128(byte[] vch, bool lendian = true) : base(WIDTH, vch, lendian)
        {
        }

        public BigInteger128(uint[] array) : base(WIDTH, array)
        {
        }
    }

    public class uint128 : BigInteger128
    {
        public uint128() : base()
        {
        }

        public uint128(BigInteger value) : base(value)
        {
        }

        public uint128(uint128 value) : this(value.value)
        {
        }

        public uint128(string hex) : base(hex)
        {
        }

        public static uint128 Parse(string hex)
        {
            return new uint128(hex);
        }

        public uint128(ulong b) : base(b)
        {
        }

        public uint128(byte[] vch, bool lendian = true) : base(vch, lendian)
        {
        }

        public static uint128 operator >>(uint128 a, int shift)
        {
            return new uint128(a.ShiftRight(shift));
        }

        public static uint128 operator <<(uint128 a, int shift)
        {
            return new uint128(a.ShiftLeft(shift));
        }

        public static uint128 operator -(uint128 a, uint128 b)
        {
            return new uint128(a.Subtract(b.value));
        }

        public static uint128 operator +(uint128 a, uint128 b)
        {
            return new uint128(a.Add(b.value));
        }

        public static uint128 operator *(uint128 a, uint128 b)
        {
            return new uint128(a.Multiply(b.value));
        }

        public static uint128 operator /(uint128 a, uint128 b)
        {
            return new uint128(a.Divide(b.value));
        }

        public static uint128 operator %(uint128 a, uint128 b)
        {
            return new uint128(a.Mod(b.value));
        }

        public uint128(byte[] vch) : this(vch, true)
        {
        }

        public static bool operator <(uint128 a, uint128 b)
        {
            return Comparison(a, b) < 0;
        }

        public static bool operator >(uint128 a, uint128 b)
        {
            return Comparison(a, b) > 0;
        }

        public static bool operator <=(uint128 a, uint128 b)
        {
            return Comparison(a, b) <= 0;
        }

        public static bool operator >=(uint128 a, uint128 b)
        {
            return Comparison(a, b) >= 0;
        }

        public static bool operator ==(uint128 a, uint128 b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) != ((object)b == null))
                return false;

            return Comparison(a, b) == 0;
        }

        public static bool operator !=(uint128 a, uint128 b)
        {
            return !(a == b);
        }

        public static bool operator ==(uint128 a, ulong b)
        {
            return (a == new uint128(b));
        }

        public static bool operator !=(uint128 a, ulong b)
        {
            return !(a == new uint128(b));
        }

        public static implicit operator uint128(ulong value)
        {
            return new uint128(value);
        }
    }
}