using System.Text;

namespace Common.Core.Cryptography
{
    public interface ISymmetricEncryptionProvider
    {
        string Encrypt(string source);

        string Decrypt(string source);
    }

    public class AesProvider : ISymmetricEncryptionProvider
    {
        private readonly AesDefault _encryption;

        public AesProvider(byte[] key)
        {
            _encryption = new AesDefault(new AesDefaultFactory(key));
        }
        public AesProvider(string key) : this(Encoding.UTF8.GetBytes(key)) {}

        public string Decrypt(string source) =>
            Encoding.UTF8.GetString(_encryption.Decrypt(Convert.FromBase64String(source)));
        
        public string Encrypt(string source) =>
            Convert.ToBase64String(_encryption.Encrypt(Encoding.UTF8.GetBytes(source)));
        public byte[] Decrypt(byte[] source) => _encryption.Decrypt(source);

        public byte[] Encrypt(byte[] source) => _encryption.Encrypt(source);
    }
}