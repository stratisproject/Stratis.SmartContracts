using System;
using Xunit;

namespace Stratis.SmartContracts.Tests
{
    public class UInt256Tests
    {
        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanConvertToFromUlong()
        {
            uint256 x = 50;
            Assert.Equal((ulong)50, (ulong)x);
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void Uint256FromTooLargeUlongThrowsError()
        {
            uint256 x = uint256.Parse("010000000000000000");
            Assert.Throws(typeof(OverflowException), () => (ulong)x);
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanConvertToFromBytes()
        {
            uint256 x = uint256.Parse("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Assert.Equal(x, new uint256(x.ToBytes()));
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanAdd()
        {
            uint256 v1 = new uint256("0000000000000000000000000000000000000000000000000000000000000325");
            uint256 v2 = new uint256("0000000000000000000000000000000000000000000000000000000000000F0F");
            uint256 v3 = v1 + v2;

            Assert.Equal(new uint256("0000000000000000000000000000000000000000000000000000000000001234"), v3);
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanSubtract()
        {
            uint256 v1 = new uint256("0000000000000000000000000000000000000000000000000000000000001234");
            uint256 v2 = new uint256("0000000000000000000000000000000000000000000000000000000000000325");
            uint256 v3 = v1 - v2;

            Assert.Equal(new uint256("0000000000000000000000000000000000000000000000000000000000000F0F"), v3);
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanMultiply()
        {
            uint256 v1 = new uint256("0000000000000000000000000000000000000000000000000000000000001234");
            uint256 v2 = new uint256("0000000000000000000000000000000000000000000000000000000000005678");
            uint256 v3 = v1 * v2;

            Assert.Equal(new uint256("0000000000000000000000000000000000000000000000000000000006260060"), v3);
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanDivide()
        {
            uint256 v1 = new uint256("0000000000000000000000000000000000000000000000000000000006260060");
            uint256 v2 = new uint256("0000000000000000000000000000000000000000000000000000000000005678");
            uint256 v3 = v1 / v2;

            Assert.Equal(new uint256("0000000000000000000000000000000000000000000000000000000000001234"), v3);
        }

        [Fact]
        [Trait("UnitTest", "UnitTest")]
        public void CanParseLargeNumbers()
        {
            uint256 v1 = uint256.Parse("fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141");
            uint256 v2 = uint256.Parse("0000000000000000000000000000000ebaaedce6af48a03bbfd25e8cd0364141");
            uint256 v3 = v1 - v2;

            Assert.Equal(new uint256("fffffffffffffffffffffffffffffff000000000000000000000000000000000"), v3);
        }
    }
}
