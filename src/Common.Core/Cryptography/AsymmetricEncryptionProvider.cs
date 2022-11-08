using System.Text;

namespace Common.Core.Cryptography
{
    //TODO: Что бы использовать и string и byte можно попробовать сделать обертку над параметрами. Базовый byte - наследник string
    public interface IAsymmetricEncryptionProvider
    {
        string Encrypt(string source, out string privateKey);
        string Decrypt(string source, string privateKey);
    }

    public class RsaOaepProvider : IAsymmetricEncryptionProvider
    {
        protected virtual RsaOaepDefault _encryption { get; } = new RsaOaepDefault();

        public string Decrypt(string source, string privateKey) =>
            Encoding.UTF8.GetString(_encryption.Decrypt(Convert.FromBase64String(source), Convert.FromBase64String(privateKey)));

        public string Encrypt(string source, out string privateKey)
        {
            var result = Convert.ToBase64String(_encryption.Encrypt(Encoding.UTF8.GetBytes(source), out byte[] key));
            privateKey = Convert.ToBase64String(key);
            return result;
        }

        public byte[] Decrypt(byte[] source, byte[] privateKey) =>_encryption.Decrypt(source, privateKey);

        public byte[] Encrypt(byte[] source, out byte[] privateKey) => _encryption.Encrypt(source, out privateKey);
    }

    public class RsaOaepAndPkcs8Provider : RsaOaepProvider
    {
        readonly string _master;
        protected override RsaOaepDefault _encryption => new RsaOaepAndPkcs8(_master);
        public RsaOaepAndPkcs8Provider(string master) => _master = master;
    }
}