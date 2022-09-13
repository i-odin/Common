using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;

namespace Common.Core.Cryptography
{
    public class RsaOaepDefault
    {
        public virtual byte[] Encrypt(byte[] source, out byte[] privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                privateKey = ExportPrivateKey(rsa);
                return rsa.Encrypt(source, true);
            }
        }

        public virtual byte[] Decrypt(byte[] source, byte[] privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                ImportPrivateKey(rsa, privateKey);
                return rsa.Decrypt(source, true);
            }
        }

        protected virtual byte[] ExportPrivateKey(RSACryptoServiceProvider rsa) => 
            rsa.ExportRSAPrivateKey();

        protected virtual void ImportPrivateKey(RSACryptoServiceProvider rsa, byte[] privateKey) =>
            rsa.ImportRSAPrivateKey(privateKey, out int _);
    }

    public class RsaOaepAndPkcs8 : RsaOaepDefault
    {
        readonly string _master;
        protected RsaOaepAndPkcs8() {}
        public RsaOaepAndPkcs8(string master) => _master = master;

        protected override byte[] ExportPrivateKey(RSACryptoServiceProvider rsa) => 
            Pkcs8PrivateKeyInfo.Create(rsa).Encrypt(_master, new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10000));

        protected override void ImportPrivateKey(RSACryptoServiceProvider rsa, byte[] privateKey) => 
            rsa.ImportEncryptedPkcs8PrivateKey(_master, privateKey, out int _);
    }
}