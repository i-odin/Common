using System.Text;

namespace Common.Core.Cryptography
{
    public interface IAsymmetricEncryptionProvider
    {
        byte[] PrivateKey { get; set; }
        string Encrypt(string source);
        string Decrypt(string source);
    }

    public class RsaOaepProvider : IAsymmetricEncryptionProvider
    {
        private readonly RsaOaepDefault _encryption = new RsaOaepDefault();
        public RsaOaepProvider() { }
        public RsaOaepProvider(byte[] privateKey) => PrivateKey = privateKey;

        public byte[] PrivateKey { get => _encryption.PrivateKey; set => _encryption.PrivateKey = value; }

        public string Decrypt(string source) =>
            Encoding.UTF8.GetString(_encryption.Decrypt(Convert.FromBase64String(source)));

        public string Encrypt(string source) =>
            Convert.ToBase64String(_encryption.Encrypt(Encoding.UTF8.GetBytes(source)));

        public byte[] Decrypt(byte[] source) =>_encryption.Decrypt(source);

        public byte[] Encrypt(byte[] source) => _encryption.Encrypt(source);
    }
}