namespace Stratis.SmartContracts
{
    public interface IEcRecoverProvider
    {
        Address GetSigner(byte[] message, byte[] signature);
        Address[] VerifySignatures(byte[] message, byte[] signatures, Address[] addresses);
    }
}
