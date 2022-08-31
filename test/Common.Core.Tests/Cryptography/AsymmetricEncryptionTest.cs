using Common.Core.Cryptography;
using System.Text;

namespace Common.Core.Tests.Cryptography
{
    public class AsymmetricEncryptionTest
    {
        [Theory]
        [InlineData("Hello")]
        [InlineData("&!$*!@(%@!(")]
        public void RsaOaepDefault_EncryptAndDecrypt_ReturnTrue(string text)
        {
            var rsa = new RsaOaepDefault();
            var encrypt = rsa.Encrypt(Encoding.UTF8.GetBytes(text));
            var privateKey = rsa.PrivateKey;

            rsa = new RsaOaepDefault();
            rsa.PrivateKey = privateKey;
            var result = Encoding.UTF8.GetString(rsa.Decrypt(encrypt));

            Assert.Equal(expected: text, actual: result);
        }

        [Theory]
        [InlineData("Hello")]
        [InlineData("&!$*!@(%@!(")]
        public void RsaOaepProvider_EncryptAndDecrypt_ReturnTrue(string text)
        {
            var provider = new RsaOaepProvider();

            var encrypt = provider.Encrypt(text);
            var privateKey = provider.PrivateKey;

            provider = new RsaOaepProvider();
            provider.PrivateKey = privateKey;
            var result = provider.Decrypt(encrypt);

            Assert.Equal(expected: text, actual: result);
        }
    }
}