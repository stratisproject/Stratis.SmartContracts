namespace Stratis.SmartContracts
{
    public interface IEcRecoverProvider
    {
        Address GetSigner(byte[] message, byte[] signature);
        Address[] VerifySignatures(byte[] signatures, byte[] message, Address[] addresses = null);
    }
}
