using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Core.Json
{
    public interface IReadWriteJson<T> : IReadJson<T>, IWriteJson<T> where T : class { }

    public interface IReadJson<T> where T : class
    {
        string Path => "Data.json";

        async Task<IList<T>> ReadAsync()
        {
            var text = await File.ReadAllTextAsync(Path);
            return JsonSerializer.Deserialize<IList<T>>(text);
        }
    }

    public interface IWriteJson<T> where T : class
    {
        string Path => "Data.json";

        async Task WriteAsync(IList<T> list)
        {
            await File.WriteAllTextAsync(Path, JsonSerializer.Serialize(list));
        }
    }
}
