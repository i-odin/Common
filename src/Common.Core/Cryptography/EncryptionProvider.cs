using System.Security.Cryptography;
using System.Text;

namespace Common.Core.Cryptography
{
    public interface IEncryptionProvider
    {
        string Encrypt(string source);

        string Decrypt(string source);
    }

    public class AesProvider : IEncryptionProvider
    {
        private readonly AesDefault _encryption;

        public AesProvider(string key)
        {
            _encryption = new AesDefault(new AesDefaultFactory(Encoding.UTF8.GetBytes(key)));
        }

        public string Decrypt(string source) =>
            Encoding.UTF8.GetString(_encryption.Decrypt(Convert.FromBase64String(source)));
        

        public string Encrypt(string source) =>
            Convert.ToBase64String(_encryption.Encrypt(Encoding.UTF8.GetBytes(source)));
        
    }

    public interface ISymmetricAlgorithmFactory<T> where T : SymmetricAlgorithm
    {
        T Create(); 
    }

    public interface IAesAlgorithmFactory : ISymmetricAlgorithmFactory<Aes>
    {
        byte[] Join(byte[] source, byte[] iv);
        byte[] Split(byte[] source);
    }

    public class AesDefaultFactory : IAesAlgorithmFactory
    {
        private Aes _aes;
        private readonly byte[] _key;
        public AesDefaultFactory(byte[] key) { _key = key; }

        public Aes Create()
        {
            _aes = Aes.Create();
            _aes.Key = _key;
            return _aes;
        }

        public byte[] Join(byte[] source, byte[] iv)
        {
            var result = new byte[source.Length + iv.Length];
            for (int i = 0; i < iv.Length; i++)
                result[i] = iv[i];

            for (int i = iv.Length, j = 0; j < source.Length; i++, j++)
                result[i] = source[j];

            return result;
        }

        public byte[] Split(byte[] source)
        {
            var iv = new byte[_aes.IV.Length];
            for (int i = 0; i < _aes.IV.Length; i++)
                iv[i] = source[i];

            _aes.IV = iv;

            var result = new byte[source.Length - _aes.IV.Length];
            for (int i = _aes.IV.Length, j = 0; i < source.Length; i++, j++)
                result[j] = source[i];

            return result;
        }
    }

    public class AesDefault
    {
        private readonly IAesAlgorithmFactory _algorithm;
        public AesDefault(IAesAlgorithmFactory algorithm)
        {
            _algorithm = algorithm;
        }

        public byte[] Encrypt(byte[] source)
        {
            using (var aes = _algorithm.Create())
            {
                using (var stream = new MemoryStream())
                {
                    using (var crypto = new CryptoStream(stream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                        crypto.Write(source, 0, source.Length);

                    return _algorithm.Join(stream.ToArray(), aes.IV);
                }
            }
        }

        public byte[] Decrypt(byte[] source)
        {
            var result = new List<byte>(source.Length);
            using (var aes = _algorithm.Create())
            {
                using (var stream = new MemoryStream(_algorithm.Split(source)))
                {
                    using (var crypto = new CryptoStream(stream, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read))
                    {
                        int data;
                        while ((data = crypto.ReadByte()) != -1)
                            result.Add((byte)data);
                    }
                }
            }

            return result.ToArray();
        }
    }
}