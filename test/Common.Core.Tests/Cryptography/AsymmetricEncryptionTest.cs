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

        [Theory]
        [InlineData("Hello")]
        [InlineData("&!$*!@(%@!(")]
        public void RsaOaepAndPkcs8_EncryptAndDecrypt_ReturnTrue(string text)
        {
            var rsa = new RsaOaepAndPkcs8("1111111111111111");
            var encrypt = rsa.Encrypt(Encoding.UTF8.GetBytes(text), out byte[] privateKey);

            rsa = new RsaOaepAndPkcs8("1111111111111111");
            var result = Encoding.UTF8.GetString(rsa.Decrypt(encrypt, privateKey));

            Assert.Equal(expected: text, actual: result);
        }

        [Theory]
        [InlineData("Hello")]
        [InlineData("&!$*!@(%@!(")]
        public void RsaOaepAndPkcs8Provider_EncryptAndDecrypt_ReturnTrue(string text)
        {
            var provider = new RsaOaepAndPkcs8Provider("1111111111111111");
            var encrypt = provider.Encrypt(text, out string privateKey);

            provider = new RsaOaepAndPkcs8Provider("1111111111111111");
            var result = provider.Decrypt(encrypt, privateKey);

            Assert.Equal(expected: text, actual: result);
        }
    }
}