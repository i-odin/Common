using Common.Core.Extensions;

namespace Common.Core.Benchmarks.Extensions;

[RankColumn]
[MemoryDiagnoser]
public class StringExtensionBenchmark
{
    [Params(null, "", " ", "\n", "\t")]
    public string Params;

    [Benchmark]
    public bool IsNullOrWhiteSpace() => Params.IsEmpty();

    [Benchmark]
    public bool IsNullOrEmptyAndTrim() => string.IsNullOrEmpty(Params) || Params.Trim().Length == 0;
}