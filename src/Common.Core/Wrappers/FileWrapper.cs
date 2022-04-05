namespace Common.Core.Wrappers;

public interface IFileWrapper
{
    string ReadAllText(string path);
    void WriteAllText(string path, string content);
}

public class FileWrapper : IFileWrapper
{
    public string ReadAllText(string path) => File.ReadAllText(path);
    public void WriteAllText(string path, string content) => File.WriteAllText(path, content);
}