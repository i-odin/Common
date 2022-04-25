using Common.Core.Extensions;

namespace Common.Core.Benchmarks.Extensions
{
    [RankColumn]
    [MemoryDiagnoser]
    public  class PathExtensionBenchmarkGetFileName
    {
        private const int _count = 10_000_000;
        private List<string> strings = new List<string>();
        public PathExtensionBenchmarkGetFileName()
        {
            strings = Enumerable.Range(0, _count).Select(x=> $"C:\\qwe\\qwe\\xzvzxv\\afasfasf\\asasfasf\\asfsafasf\\asfasfasf\\asfsafasf\\{Guid.NewGuid()}.sql").ToList();
        }

        [Benchmark]
        public string GetFileName()
        {
            foreach (var item in strings)
            {
                return Path.GetFileName(item);
            }
            return string.Empty;
        }

        [Benchmark]
        public ReadOnlySpan<char> GetFileNameAsSpan()
        {
            foreach (var item in strings)
            {
                return Path.GetFileName(item.AsSpan());
            }
            return ReadOnlySpan<char>.Empty;
            
        }

        [Benchmark]
        public ReadOnlySpan<char> GetFileNameExtension()
        {
            foreach (var item in strings)
            {
                return PathExtension.GetFileName(item);
            }
            return ReadOnlySpan<char>.Empty;
            
        }
    }
}