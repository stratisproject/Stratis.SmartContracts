namespace Stratis.SmartContracts
{
    public sealed class Message : IMessage
    {
        /// <inheritdoc/>
        public Address ContractAddress { get; }

        /// <inheritdoc/>
        public Address Sender { get; }

        /// <inheritdoc/>
        public UInt256 Value { get; }

        public Message(Address contractAddress, Address sender, UInt256 value)
        {
            this.ContractAddress = contractAddress;
            this.Sender = sender;
            this.Value = value;
        }
    }
}