using Common.Core.Extensions;

namespace Common.Core.Benchmarks.Extensions;

[RankColumn]
[MemoryDiagnoser]
public class StringExtensionBenchmarkIsEmpty
{
    [Params(null, "", " ", "\n", "\t")]
    public string Params;

    [Benchmark]
    public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(Params);
    
    [Benchmark]
    public bool IsNullOrWhiteSpaceAndIsNullAndIsEmpty() => Params.IsEmpty();

    [Benchmark]
    public bool IsNullOrEmptyAndIsNullOrWhiteSpace() => string.IsNullOrEmpty(Params) || string.IsNullOrWhiteSpace(Params); 
}