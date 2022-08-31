using System.Security.Cryptography;

namespace Common.Core.Cryptography
{
    public class RsaOaepDefault
    {
        public byte[]? PrivateKey { get; set; }

        public byte[] Encrypt(byte[] source)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                PrivateKey = rsa.ExportRSAPrivateKey();
                return rsa.Encrypt(source, true);
            }
        }

        public byte[] Decrypt(byte[] source)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPrivateKey(PrivateKey, out int _);
                return rsa.Decrypt(source, true);
            }
        }
    }
}