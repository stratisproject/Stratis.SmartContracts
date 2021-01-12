using System;
using System.Numerics;

namespace NBitcoin
{
    public abstract class BigIntegerBase : IComparable
    {
        protected BigInteger value;
        protected int width;

        public BigIntegerBase(BigIntegerBase value) : this(value.width, value.value)
        {
        }

        public BigIntegerBase(int width)
        {
            if ((width & 3) != 0)
                throw new ArgumentException($"The '{nameof(width)}' must be a multiple of 4.");
            this.width = width;
        }

        public BigIntegerBase(int width, ulong b) : this(width)
        {
            SetValue(new BigInteger(b));
        }

                public BigIntegerBase(int width, string hex) : this(width, HexBytes(hex))
        {
        }

        public BigIntegerBase(int width, BigIntegerBase value) : this(width, value.value)
        {
        }

        public BigIntegerBase(int width, BigInteger value) : this(width)
        {
            SetValue(value);
        }

        public BigIntegerBase(int width, byte[] vch, bool lendian = true) : this(width)
        {
            if (vch.Length != this.width)
                throw new FormatException($"The byte array should be {this.width} bytes long.");

            SetValue(new BigInteger(vch, true, !lendian));
        }

        public BigIntegerBase(int width, uint[] array) : this(width)
        {
            int length = this.width / 4;

            if (array.Length != length)
                throw new FormatException($"The array length should be {length}.");

            byte[] bytes = new byte[this.width];

            for (int i = 0; i < length; i++)
                BitConverter.GetBytes(array[i]).CopyTo(bytes, i * 4);

            SetValue(new BigInteger(bytes, true));
        }


        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private static byte[] HexBytes(string str)
        {
            str = str.ToLower().Trim();

            if (str.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                str = str.Substring(2);

            if ((str.Length & 1) != 0)
                throw new ArgumentException("The string length must be a multiple of 2.");

            return StringToByteArray(str);
        }

        private bool TooBig(byte[] bytes)
        {
            if (bytes.Length <= this.width)
                return false;
            if (bytes.Length == (this.width + 1) && bytes[this.width] == 0)
                return false;
            return true;
        }

        private void SetValue(BigInteger newValue)
        {
            if (TooBig(newValue.ToByteArray()))
                throw new OverflowException();

            this.value = newValue;
        }

        public uint[] ToUIntArray()
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

        protected BigInteger ShiftRight(int shift) => this.value >> shift;

        protected BigInteger ShiftLeft(int shift) => this.value << shift;

        protected BigInteger Add(BigInteger value2) => this.value + value2;

        protected BigInteger Subtract(BigInteger value2)
        {
            if (this.value.CompareTo(value2) < 0)
                throw new ArithmeticException("Number cannot be negative.");

            return this.value - value2;
        }

        protected BigInteger Multiply(BigInteger value2) => this.value * value2;

        protected BigInteger Divide(BigInteger value2) => this.value / value2;

        protected BigInteger Mod(BigInteger value2) => this.value % value2;

        public int CompareTo(object b)
        {
            return this.value.CompareTo(((BigIntegerBase)b).value);
        }

        public static int Comparison(BigIntegerBase a, BigIntegerBase b)
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

        public override string ToString()
        {
            return ByteArrayToString(ToBytes(false));
        }

        public byte GetByte(int n)
        {
            if (n >= this.width)
                throw new ArgumentOutOfRangeException();

            return this.ToBytes()[n];
        }

        public uint GetLow32()
        {
            var pn = ToUIntArray();
            return pn[0];
        }

        public ulong GetULong(int position)
        {
            var pn = ToUIntArray();
            int length = this.width / 8;

            if (position >= length)
                throw new ArgumentOutOfRangeException($"Argument '{nameof(position)}' should be less than {length}.", nameof(position));

            return (ulong)pn[position * 2] + (ulong)((ulong)pn[position * 2 + 1] << 32);
        }
    }
}
