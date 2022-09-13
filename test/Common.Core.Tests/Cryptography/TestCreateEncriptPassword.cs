using Common.Core.Cryptography;
using System.Security.Cryptography;

namespace Common.Core.Tests.Cryptography
{
    public class TestCreateEncriptPassword
    {
        [Theory]
        [InlineData("Hello")]
        public void CreateEncriptPassword(string text)
        {
            var password = new byte[32];
            RandomNumberGenerator.Create().GetBytes(password);

            var encriptText = new AesProvider(password).Encrypt(text);

            var rfc = new Rfc2898DeriveBytes("11", 256, 10000, HashAlgorithmName.SHA256);
            var master = Convert.ToBase64String(rfc.GetBytes(16));

            var rsa = new RsaOaepAndPkcs8Provider(master);
            password = rsa.Encrypt(password, out byte[] privateKey);

            rsa = new RsaOaepAndPkcs8Provider(master);
            password = rsa.Decrypt(password, privateKey);

            var result = new AesProvider(password).Decrypt(encriptText) ?? "";

            Assert.Equal(expected: text, actual: result);
        }
    }
}
