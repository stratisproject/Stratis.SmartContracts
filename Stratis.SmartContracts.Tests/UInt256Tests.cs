using System;
using System.Numerics;
using Xunit;

namespace Stratis.SmartContracts.Tests
{
    public class UInt256Tests
    {
        [Fact]
        public void CanConvertToFromUlong()
        {
            UInt256 x = 50;
            Assert.Equal((ulong)50, (ulong)x);
        }

        [Fact]
        public void Uint256FromTooLargeUlongThrowsError()
        {
            UInt256 x = UInt256.Parse("0x10000000000000000");
            Assert.Throws<OverflowException>(() => (ulong)x);
        }

        [Fact]
        public void AddingThrowsOverflowIfResultTooBig()
        {
            UInt256 v1 = UInt256.Parse("0xffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            UInt256 v2 = UInt256.Parse("0x1");
            Assert.Throws<OverflowException>(() => v1 + v2);
        }

        [Fact]
        public void SubtractingThrowsErrorIfResultIsNegative()
        {
            UInt256 v1 = UInt256.Parse("0xffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Assert.Throws<OverflowException>(() => UInt256.Zero - v1);
        }

        [Fact]
        public void MultiplyingThrowsErrorIfResultTooBig()
        {
            UInt256 v1 = UInt256.Parse("0x100000000000000000000000000000000");
            UInt256 v2 = UInt256.Parse("0x100000000000000000000000000000000");
            Assert.Throws<OverflowException>(() => v1 * v2);
        }

        [Fact]
        public void DividingByZeroThrowsError()
        {
            UInt256 v1 = UInt256.Parse("0x100000000000000000000000000000000");
            UInt256 v2 = UInt256.Zero;
            Assert.Throws<DivideByZeroException>(() => v1 / v2);
        }

        [Fact]
        public void ModByZeroThrowsError()
        {
            UInt256 v1 = UInt256.Parse("0x100000000000000000000000000000000");
            UInt256 v2 = UInt256.Zero;
            Assert.Throws<DivideByZeroException>(() => v1 % v2);
        }

        [Fact]
        public void CanConvertToFromBytes()
        {
            UInt256 x = UInt256.Parse("0xffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Assert.Equal(x, new UInt256(x.ToBytes()));
        }

        [Fact]
        public void CanAdd()
        {
            UInt256 v1 = new UInt256("0x325");
            UInt256 v2 = new UInt256("0xf0f");
            UInt256 v3 = v1 + v2;

            Assert.Equal(new UInt256("0x1234"), v3);
        }

        [Fact]
        public void CanSubtract()
        {
            UInt256 v1 = new UInt256("0x1234");
            UInt256 v2 = new UInt256("0x325");
            UInt256 v3 = v1 - v2;

            Assert.Equal(new UInt256("0xf0f"), v3);
        }

        [Fact]
        public void CanMultiply()
        {
            UInt256 v1 = new UInt256("0x1234");
            UInt256 v2 = new UInt256("0x5678");
            UInt256 v3 = v1 * v2;

            Assert.Equal(new UInt256("0x6260060"), v3);
        }

        [Fact]
        public void CanDivide()
        {
            UInt256 v1 = new UInt256("0x6260060");
            UInt256 v2 = new UInt256("0x5678");
            UInt256 v3 = v1 / v2;

            Assert.Equal(new UInt256("0x1234"), v3);
        }

        [Fact]
        public void CanParseLargeNumbers()
        {
            UInt256 v1 = UInt256.Parse("0xfffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141");
            UInt256 v2 = UInt256.Parse("0xebaaedce6af48a03bbfd25e8cd0364141");
            UInt256 v3 = v1 - v2;

            Assert.Equal(new UInt256("0xfffffffffffffffffffffffffffffff000000000000000000000000000000000"), v3);
        }

        [Fact]
        public void TooBigUInt128CastThrowsOverflow()
        {
            UInt256 v1 = UInt256.Parse("0xfffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141");
            UInt256 v2 = UInt256.Parse("0xbaaedce6af48a03bbfd25e8cd0364141");

            Assert.Throws<OverflowException>(() => (UInt128)v1);
            var dummy = (UInt128)v2;
        }

        [Fact]
        public void CanCast()
        {
            UInt256 v1 = UInt256.Parse("0x12345678");

            Assert.Equal<uint>(0x12345678, (uint)v1);
            Assert.Equal<int>(0x12345678, (int)v1);
            Assert.Equal<ulong>(0x12345678, (ulong)v1);
            Assert.Equal<long>(0x12345678, (long)v1);
            Assert.Equal<UInt128>(0x12345678, (UInt128)v1);
            Assert.Equal<UInt256>(0x12345678, v1);
        }

        [Fact]
        public void CastTooBigNumberCausesOverflow()
        {
            Assert.Throws<OverflowException>(() => (int)UInt256.Parse("0x12345678123456789"));
        }
    }
}
