using BenchmarkDotNet.Running;
using Common.Core.Benchmarks.Extensions;

namespace Common.Core.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringExtensionBenchmark>();
        }
    }
}