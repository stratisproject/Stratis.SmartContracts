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
    }
}
