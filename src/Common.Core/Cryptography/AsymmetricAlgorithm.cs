using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;

namespace Common.Core.Cryptography
{
    public class RsaOaepDefault
    {
        public byte[] Encrypt(byte[] source, out byte[] privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                privateKey = rsa.ExportRSAPrivateKey();
                return rsa.Encrypt(source, true);
            }
        }

        public byte[] Decrypt(byte[] source, byte[] privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPrivateKey(privateKey, out int _);
                return rsa.Decrypt(source, true);
            }
        }
    }

    //TODO: Test
    public class RsaOaepAndPkcs8
    {
        private string Master = "1111111111111111";
        public byte[] Encrypt(byte[] source, out byte[] privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                var pkcs = Pkcs8PrivateKeyInfo.Create(rsa);
                var qwe = rsa.ExportRSAPrivateKey();
                var qwe1 = rsa.ExportPkcs8PrivateKey();
                privateKey = pkcs.Encrypt(Master, new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10000));
                return rsa.Encrypt(source, true);
            }
        }

        public byte[] Decrypt(byte[] source, byte[] privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPrivateKey(privateKey, out int _);
                return rsa.Decrypt(source, true);
            }
        }
    }
}