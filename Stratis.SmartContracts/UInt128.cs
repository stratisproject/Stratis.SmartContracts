using System;
using System.Numerics;

namespace Stratis.SmartContracts
{
    public struct UInt128 : IComparable
    {
        const int WIDTH = 16;

        private UIntBase value;

        public static UInt128 Zero => 0;
        public static UInt128 MinValue => 0;
        public static UInt128 MaxValue => new UInt128((BigInteger.One << (WIDTH * 8)) - 1);

        public UInt128(string hex)
        {
            this.value = new UIntBase(WIDTH, hex);
        }

        public static UInt128 Parse(string str)
        {
            return new UInt128(str);
        }

        private UInt128(BigInteger value)
        {
            this.value = new UIntBase(WIDTH, value);
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

        /// <summary>
        /// Subtracts the first number from the second.
        /// </summary>
        /// <param name="a">Number to subtract from.</param>
        /// <param name="b">Number to subtract.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="OverflowException">Always thrown if the result is negative regardless of checked context.</exception>
        public static UInt128 operator -(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Subtract(b.value.GetValue()));
        }

        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="a">Number to add.</param>
        /// <param name="b">Number to add.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="OverflowException">Always thrown if the result is greater than <see cref="MaxValue"/>.</exception>
        public static UInt128 operator +(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Add(b.value.GetValue()));
        }

        /// <summary>
        /// Multiplies two numbers.
        /// </summary>
        /// <param name="a">Number to multiply.</param>
        /// <param name="b">Number to multiply.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="OverflowException">Always thrown if the result is greater than <see cref="MaxValue"/>.</exception>
        public static UInt128 operator *(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Multiply(b.value.GetValue()));
        }

        /// <summary>
        /// Divides the first number by the second.
        /// </summary>
        /// <param name="a">Number to divide.</param>
        /// <param name="b">Number to divide by.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="DivideByZeroException">Always thrown if <paramref name="b"/> is zero.</exception>
        public static UInt128 operator /(UInt128 a, UInt128 b)
        {
            return new UInt128(a.value.Divide(b.value.GetValue()));
        }

        /// <summary>
        /// Computes the remainder of the division of the first number by the second - i.e. a mod b.
        /// </summary>
        /// <param name="a">Number to divide.</param>
        /// <param name="b">Number to divide by.</param>
        /// <returns>The remainder as a zero or positive number.</returns>
        /// <exception cref="DivideByZeroException">Always thrown if <paramref name="b"/> is zero.</exception>
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
            return new UInt128(value);
        }

        public static implicit operator UInt128(int value)
        {
            return new UInt128(value);
        }

        public static implicit operator UInt128(uint value)
        {
            return new UInt128(value);
        }

        public static explicit operator int(UInt128 value)
        {
            return (int)value.value.GetValue();
        }

        public static explicit operator uint(UInt128 value)
        {
            return (uint)value.value.GetValue();
        }

        public static explicit operator long(UInt128 value)
        {
            return (long)value.value.GetValue();
        }

        public static explicit operator ulong(UInt128 value)
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

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}