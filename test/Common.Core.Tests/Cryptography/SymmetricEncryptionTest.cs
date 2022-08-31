using Common.Core.Cryptography;
using System.Text;

namespace Common.Core.Tests.Cryptography
{
    public class SymmetricEncryptionTest
    {
        [Theory]
        [InlineData("Hello", "01234567890123456789012345678901")]
        [InlineData("&!$*!@(%@!(", "01234567890123456789012345678901")]
        [InlineData("&!$*!@(%@!(", "%.234567890123456789012345678901")]
        public void AesDefault_EncryptAndDecrypt_ReturnTrue(string text, string password)
        {
            var aes = new AesDefault(new AesDefaultFactory(Encoding.UTF8.GetBytes(password)));

            var encrypt = aes.Encrypt(Encoding.UTF8.GetBytes(text));

            aes = new AesDefault(new AesDefaultFactory(Encoding.UTF8.GetBytes(password)));

            var result = Encoding.UTF8.GetString(aes.Decrypt(encrypt));

            Assert.Equal(expected: text, actual: result);
        }

        [Theory]
        [InlineData("Hello", "01234567890123456789012345678901")]
        [InlineData("&!$*!@(%@!(", "01234567890123456789012345678901")]
        [InlineData("&!$*!@(%@!(", "%.234567890123456789012345678901")]
        public void AesProvider_EncryptAndDecrypt_ReturnTrue(string text, string password)
        {
            var provider = new AesProvider(password);

            var encrypt = provider.Encrypt(text);

            provider = new AesProvider(password);

            var result = provider.Decrypt(encrypt);

            Assert.Equal(expected: text, actual: result);
        }
    }
}
