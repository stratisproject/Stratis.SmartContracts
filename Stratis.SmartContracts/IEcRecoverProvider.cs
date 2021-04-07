namespace Stratis.SmartContracts
{
    public interface IEcRecoverProvider
    {
        Address GetSigner(byte[] message, byte[] signature);
        Address[] VerifySignatures(string message, byte[] signatures, Address[] addresses);
    }
}
