using System;
using System.Numerics;

namespace Stratis.SmartContracts
{
    internal struct UIntBase : IComparable
    {
        private int width;
        private BigInteger value;

        public UIntBase(int width)
        {
            if ((width & 3) != 0)
                throw new ArgumentException($"The '{nameof(width)}' must be a multiple of 4.");

            this.width = width;
        }

        public UIntBase(int width, BigInteger value) : this(width)
        {
            SetValue(value);
        }

        public UIntBase(int width, UIntBase value) : this(width)
        {
            SetValue(value.value);
        }

        public UIntBase(int width, ulong b) : this(width)
        {
            SetValue(new BigInteger(b));
        }

        public UIntBase(int width, byte[] vch, bool lendian = true) : this(width)
        {
            if (vch.Length > this.width)
                throw new FormatException($"The byte array should be {this.width} bytes or less.");

            SetValue(new BigInteger(vch, true, !lendian));
        }

        public UIntBase(int width, string str) : this(width)
        {
            if (str.StartsWith("0x"))
                SetValue(BigInteger.Parse("0" + str.Substring(2), System.Globalization.NumberStyles.HexNumber));
            else
                SetValue(BigInteger.Parse(str));
        }

        public UIntBase(int width, uint[] array) : this(width)
        {
            int length = this.width / 4;

            if (array.Length != length)
                throw new FormatException($"The array length should be {length}.");

            byte[] bytes = new byte[this.width];

            for (int i = 0; i < length; i++)
                BitConverter.GetBytes(array[i]).CopyTo(bytes, i * 4);

            SetValue(new BigInteger(bytes, true));
        }

        private bool TooBig(byte[] bytes)
        {
            if (bytes.Length <= this.width)
                return false;
            if (bytes.Length == (this.width + 1) && bytes[this.width] == 0)
                return false;
            return true;
        }

        private void SetValue(BigInteger value)
        {
            if (value.Sign < 0)
                throw new OverflowException("Only positive or zero values are allowed.");

            if (TooBig(value.ToByteArray()))
                throw new OverflowException();

            this.value = value;
        }

        public BigInteger GetValue()
        {
            return this.value;
        }

        private uint[] ToUIntArray()
        {
            var bytes = this.ToBytes();
            int length = this.width / 4;
            uint[] pn = new uint[length];
            for (int i = 0; i < length; i++)
                pn[i] = BitConverter.ToUInt32(bytes, i * 4);

            return pn;
        }

        public byte[] ToBytes(bool lendian = true)
        {
            var arr1 = this.value.ToByteArray();
            var arr = new byte[this.width];
            Array.Copy(arr1, arr, Math.Min(arr1.Length, arr.Length));

            if (!lendian)
                Array.Reverse(arr);

            return arr;
        }

        internal BigInteger ShiftRight(int shift) => this.value >> shift;

        internal BigInteger ShiftLeft(int shift) => this.value << shift;

        internal BigInteger Add(BigInteger value2) => this.value + value2;

        internal BigInteger Subtract(BigInteger value2)
        {
            if (this.value.CompareTo(value2) < 0)
                throw new OverflowException("Result cannot be negative.");

            return this.value - value2;
        }

        internal BigInteger Multiply(BigInteger value2) => this.value * value2;

        internal BigInteger Divide(BigInteger value2) => this.value / value2;

        internal BigInteger Mod(BigInteger value2) => this.value % value2;

        public int CompareTo(object b)
        {
            return this.value.CompareTo(((UIntBase)b).value);
        }

        public static int Comparison(UIntBase a, UIntBase b)
        {
            return a.CompareTo(b);
        }

        public override int GetHashCode()
        {
            var pn = ToUIntArray();
            uint hash = 0;
            for (int i = 0; i < pn.Length; i++)
                hash ^= pn[i];
            return (int)hash;
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public string ToHex()
        {
            return ByteArrayToString(ToBytes(false)).ToLower();
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
