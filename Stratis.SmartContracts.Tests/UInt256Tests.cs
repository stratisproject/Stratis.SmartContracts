using System;
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
            UInt256 x = UInt256.Parse("010000000000000000");
            Assert.Throws(typeof(OverflowException), () => (ulong)x);
        }

        [Fact]
        public void CanConvertToFromBytes()
        {
            UInt256 x = UInt256.Parse("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Assert.Equal(x, new UInt256(x.ToBytes()));
        }

        [Fact]
        public void CanAdd()
        {
            UInt256 v1 = new UInt256("0000000000000000000000000000000000000000000000000000000000000325");
            UInt256 v2 = new UInt256("0000000000000000000000000000000000000000000000000000000000000F0F");
            UInt256 v3 = v1 + v2;

            Assert.Equal(new UInt256("0000000000000000000000000000000000000000000000000000000000001234"), v3);
        }

        [Fact]
        public void CanSubtract()
        {
            UInt256 v1 = new UInt256("0000000000000000000000000000000000000000000000000000000000001234");
            UInt256 v2 = new UInt256("0000000000000000000000000000000000000000000000000000000000000325");
            UInt256 v3 = v1 - v2;

            Assert.Equal(new UInt256("0000000000000000000000000000000000000000000000000000000000000F0F"), v3);
        }

        [Fact]
        public void CanMultiply()
        {
            UInt256 v1 = new UInt256("0000000000000000000000000000000000000000000000000000000000001234");
            UInt256 v2 = new UInt256("0000000000000000000000000000000000000000000000000000000000005678");
            UInt256 v3 = v1 * v2;

            Assert.Equal(new UInt256("0000000000000000000000000000000000000000000000000000000006260060"), v3);
        }

        [Fact]
        public void CanDivide()
        {
            UInt256 v1 = new UInt256("0000000000000000000000000000000000000000000000000000000006260060");
            UInt256 v2 = new UInt256("0000000000000000000000000000000000000000000000000000000000005678");
            UInt256 v3 = v1 / v2;

            Assert.Equal(new UInt256("0000000000000000000000000000000000000000000000000000000000001234"), v3);
        }

        [Fact]
        public void CanParseLargeNumbers()
        {
            UInt256 v1 = UInt256.Parse("fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141");
            UInt256 v2 = UInt256.Parse("0000000000000000000000000000000ebaaedce6af48a03bbfd25e8cd0364141");
            UInt256 v3 = v1 - v2;

            Assert.Equal(new UInt256("fffffffffffffffffffffffffffffff000000000000000000000000000000000"), v3);
        }
    }
}
