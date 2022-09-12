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
            var encrypt = rsa.Encrypt(Encoding.UTF8.GetBytes(text), out byte[] privateKey);

            rsa = new RsaOaepDefault();
            var result = Encoding.UTF8.GetString(rsa.Decrypt(encrypt, privateKey));

            Assert.Equal(expected: text, actual: result);
        }

        [Theory]
        [InlineData("Hello")]
        [InlineData("&!$*!@(%@!(")]
        public void RsaOaepProvider_EncryptAndDecrypt_ReturnTrue(string text)
        {
            var provider = new RsaOaepProvider();
            var encrypt = provider.Encrypt(text, out string privateKey);

            provider = new RsaOaepProvider();
            var result = provider.Decrypt(encrypt, privateKey);

            Assert.Equal(expected: text, actual: result);
        }
    }
}