namespace Stratis.SmartContracts
{
    public interface IEcRecoverProvider
    {
        Address GetSigner(byte[] message, byte[] signature);
    }
}
