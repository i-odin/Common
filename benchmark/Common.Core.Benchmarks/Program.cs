using BenchmarkDotNet.Running;
using Common.Core.Benchmarks.SqlBuilder.Ms;

//BenchmarkRunner.Run<StringExtensionBenchmarkIsEmpty>();
//BenchmarkRunner.Run<PathExtensionBenchmarkGetFileName>();
BenchmarkRunner.Run<SqlBuilderBenchmarkUpdate>();