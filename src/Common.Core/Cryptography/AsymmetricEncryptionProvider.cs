
namespace Common.Core.Cryptography
{
    public interface IAsymmetricEncryptionProvider
    {
        string Encrypt(string source, out string key);

        string Decrypt(string source, string key);
    }
}
