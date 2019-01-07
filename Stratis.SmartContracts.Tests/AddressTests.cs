using System;
using System.Linq;
using Stratis.SmartContracts;
using Xunit;

namespace Stratis.SmartContracts.Tests
{
    public class AddressTests
    {
        private static readonly byte[] address0Bytes = new byte[20]
        {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        private static readonly byte[] address1Bytes = new byte[20]
        {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01
        };

        private static readonly byte[] address2Bytes = new byte[20]
        {
            0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x01, 0x20, 0x03, 0x30,
            0x04, 0x40, 0x05, 0x50, 0x06, 0x60, 0x07, 0x70, 0x08, 0x80
        };

        private static Address address0 = address0Bytes.ToAddress();
        private static Address address1 = address1Bytes.ToAddress();
        private static Address address2 = address2Bytes.ToAddress();

        [Fact]
        public void Create_Address_Success()
        {
            Assert.True(address0.ToBytes().SequenceEqual(address0Bytes));
            Assert.True(address1.ToBytes().SequenceEqual(address1Bytes));
            Assert.True(address2.ToBytes().SequenceEqual(address2Bytes));
        }

        [Fact]
        public void Address_ToString()
        {
            var addressString = address2Bytes.ToHexString();

            Assert.Equal(addressString, address2.ToString());
        }

        [Fact]
        public void Address_Equality_Equals_Different()
        {
            Assert.False(address0.Equals(address1));
        }

        [Fact]
        public void Address_Equality_Equals_Operator_Different()
        {
            Assert.False(address0 == address1);
        }

        [Fact]
        public void Address_Equality_Equals_Same()
        {
            var address2 = new Address(address1);

            Assert.True(address1.Equals(address2));
        }

        [Fact]
        public void Address_Equality_Equals_Operator_Same()
        {
            var address2 = new Address(address1);

            Assert.True(address1 == address2);
        }

        [Fact]
        public void Address_Equality_Equals_Same_Instance()
        {
            Assert.True(address0.Equals(address0));
        }

        [Fact]
        public void Address_Equality_Equals_Operator_Same_Instance()
        {
            Assert.True(address0 == address0);
        }

        [Fact]
        public void Address_Equality_Equals_Null()
        {
            Assert.False(address0.Equals(null));
        }

        [Fact]
        public void Address_Equality_Equals_Operator_Null()
        {
            Assert.False(address0 == null);
        }
    }

    public static class AddressHelpers
    {
        public static Address ToAddress(this byte[] bytes)
        {
            var pn0 = BitConverter.ToUInt32(bytes, 0);
            var pn1 = BitConverter.ToUInt32(bytes, 4);
            var pn2 = BitConverter.ToUInt32(bytes, 8);
            var pn3 = BitConverter.ToUInt32(bytes, 12);
            var pn4 = BitConverter.ToUInt32(bytes, 16);

            return new Address(pn0, pn1, pn2, pn3, pn4);
        }

        public static string ToHexString(this byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}