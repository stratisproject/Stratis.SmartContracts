using System;
using System.Numerics;

namespace Stratis.SmartContracts
{
    public struct UInt256 : IComparable
    {
        const int WIDTH = 32;

        private UIntBase value;

        public static UInt256 Zero => 0;
        public static UInt256 MinValue => 0;
        public static UInt256 MaxValue => new UInt256((BigInteger.One << (WIDTH * 8)) - 1);        

        public UInt256(string hex)
        {
            this.value = new UIntBase(WIDTH, hex);
        }

        public static UInt256 Parse(string str)
        {
            return new UInt256(str);
        }

        private UInt256(BigInteger value)
        {
            this.value = new UIntBase(WIDTH, value);
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

        /// <summary>
        /// Subtracts the first number from the second.
        /// </summary>
        /// <param name="a">Number to subtract from.</param>
        /// <param name="b">Number to subtract.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="OverflowException">Always thrown if the result is negative regardless of checked context.</exception>
        public static UInt256 operator -(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Subtract(b.value.GetValue()));
        }

        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="a">Number to add.</param>
        /// <param name="b">Number to add.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="OverflowException">Always thrown if the result is greater than <see cref="MaxValue"/>.</exception>
        public static UInt256 operator +(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Add(b.value.GetValue()));
        }

        /// <summary>
        /// Multiplies two numbers.
        /// </summary>
        /// <param name="a">Number to multiply.</param>
        /// <param name="b">Number to multiply.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="OverflowException">Always thrown if the result is greater than <see cref="MaxValue"/>.</exception>
        public static UInt256 operator *(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Multiply(b.value.GetValue()));
        }

        /// <summary>
        /// Divides the first number by the second.
        /// </summary>
        /// <param name="a">Number to divide.</param>
        /// <param name="b">Number to divide by.</param>
        /// <returns>The result as a zero or positive number.</returns>
        /// <exception cref="DivideByZeroException">Always thrown if <paramref name="b"/> is zero.</exception>
        public static UInt256 operator /(UInt256 a, UInt256 b)
        {
            return new UInt256(a.value.Divide(b.value.GetValue()));
        }

        /// <summary>
        /// Computes the remainder of the division of the first number by the second - i.e. a mod b.
        /// </summary>
        /// <param name="a">Number to divide.</param>
        /// <param name="b">Number to divide by.</param>
        /// <returns>The remainder as a zero or positive number.</returns>
        /// <exception cref="DivideByZeroException">Always thrown if <paramref name="b"/> is zero.</exception>
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
            return new UInt256(value);
        }
  
        public static implicit operator UInt256(int value)
        {
            return new UInt256(value);
        }

        public static implicit operator UInt256(uint value)
        {
            return new UInt256(value);
        }

        public static implicit operator UInt256(UInt128 value)
        {
            var bytes = new byte[32];
            value.ToBytes().CopyTo(bytes, 0);
            return new UInt256(bytes);
        }

        public static explicit operator int(UInt256 value)
        {
            return (int)value.value.GetValue();
        }

        public static explicit operator uint(UInt256 value)
        {
            return (uint)value.value.GetValue();
        }

        public static explicit operator long(UInt256 value)
        {
            return (long)value.value.GetValue();
        }

        public static explicit operator ulong(UInt256 value)
        {
            return (ulong)value.value.GetValue();
        }

        public static explicit operator UInt128(UInt256 value)
        {
            var bytes = value.ToBytes();
            var firstHalf = new byte[16];
            var secondHalf = new byte[16];
            Array.Copy(bytes, 0, firstHalf, 0, 16);
            Array.Copy(bytes, 16, secondHalf, 0, 16);
            if (new UInt128(secondHalf) != UInt128.Zero)
                throw new OverflowException();

            return new UInt128(firstHalf);
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

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}