using System.Security.Cryptography;

namespace Common.Core.Cryptography;

public class AlgorithmFactory
{
    public static IDataAlgorithm Create(IDataEncrypt source, byte[] key)
    {
        return source.DataType switch
        {
            DataType.EAS => new AesAlgorithm(key),
            _ => throw new NotSupportedException(),
        };
    }
}

public interface IDataAlgorithm : IDisposable
{
    byte[] Decrypt(byte[] data);
    byte[] Encrypt(byte[] data);
}

public class AesAlgorithm : IDataAlgorithm
{
    private readonly Aes _aes;
    public AesAlgorithm(byte[] key)
    {
        _aes = Aes.Create();
        _aes.Key = key;
    }

    public byte[] Decrypt(byte[] data)
    {
        var result = new List<byte>(data.Length);
        using (var stream = new MemoryStream(data))
        {
            using (var crypto = new CryptoStream(stream, _aes.CreateDecryptor(_aes.Key, _aes.IV), CryptoStreamMode.Read))
            {
                int input;
                while ((input = crypto.ReadByte()) != -1)
                    result.Add((byte)input);
            }
        }
        
        return result.ToArray();
    }

    public void Dispose()
    {
        _aes?.Dispose();
    }

    public byte[] Encrypt(byte[] data)
    {
        using (var stream = new MemoryStream())
        {
            using (var crypto = new CryptoStream(stream, _aes.CreateEncryptor(_aes.Key, _aes.IV), CryptoStreamMode.Write))
                crypto.Write(data, 0, data.Length);

            _aes.Dispose();
            return stream.ToArray();
        }
    }
}

public enum DataType { EAS = 1 }
public enum EncType { RSA_OAEP = 1 }
public enum PrivType {  }

public interface IDataEncrypt
{
    public string Data { get; set; }
    public DataType DataType { get; }
}

public interface IKeyEncrypt
{
    public byte[] EncKey { get; set; }
    public EncType EncType { get; }
}

public interface IPrivKeyEncrypt
{
    public string PrivKey { get; set; }
    public PrivType PrivType { get; }
}

public interface IEncrypt : IKeyEncrypt, IPrivKeyEncrypt, IDataEncrypt
{
}

public interface IEncryptionService
{
    void Decrypt(IEncrypt input, string unlockKey);
    void Encrypt(IEncrypt input, string unlockKey);
}

public class EncryptionService : IEncryptionService
{
    public void Decrypt(IEncrypt input, string unlockKey)
    {
        throw new NotImplementedException();
    }

    public void Encrypt(IEncrypt source, string unlockKey)
    {
        //https://habr.com/ru/companies/yandex/articles/344382/
        // unlockKey - это хеш

        //1. Генерируем пароль EncKey
        //2. Берем симетрически алгоритм EncType
        //3. Симетрическим алгоритмом шифруем data
        //4. Берем асиметрический алгоритм 
        //5. шифруем EncKey*

        using (var dataAlg = AlgorithmFactory.Create(source, source.EncKey))
        {

        }

        throw new NotImplementedException();
    }
}
