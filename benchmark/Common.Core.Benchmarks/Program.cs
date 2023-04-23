using BenchmarkDotNet.Running;
using Common.Core.Benchmarks.Extensions;
using Common.Core.Benchmarks.SqlBuilder.MsSqlBuilder;

//BenchmarkRunner.Run<StringExtensionBenchmarkIsEmpty>();
//BenchmarkRunner.Run<PathExtensionBenchmarkGetFileName>();
BenchmarkRunner.Run<SqlBuilderBenchmarkUpdate>();