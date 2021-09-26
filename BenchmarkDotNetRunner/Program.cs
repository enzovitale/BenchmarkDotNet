using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

using BenchmarkDotNetSamples.Samples;


namespace BenchmarkDotNetSamples.Runner
{
    public class MethodWrapper
    {
        [Benchmark(Baseline = true, Description = "my Baseline")] public void BaselineBenchmark() => new IntroBasic().Sleep();
        [Benchmark(Description = "my Other")] public void OtherBenchmark() => new IntroBasic().SleepWithDescription();
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region A configuration for our benchmarks
            var myConfig = ManualConfig.CreateEmpty()

                // Adding first job
                .AddJob(Job.Default
                    .WithRuntime(CoreRuntime.Core50) // .NET Framework 4.7.2
                    .WithPlatform(Platform.X64) // Run as x64 application
                    .WithJit(Jit.RyuJit) // Use LegacyJIT instead of the default RyuJIT
                    .WithGcServer(true) // Use Server GC
                )

                // Adding second job
                .AddJob(Job.Default
                    .AsBaseline() // It will be marked as baseline
                    .WithRuntime(CoreRuntime.Core50) // .NET Framework 4.7.2
                    .WithPlatform(Platform.X64) // Run as x64 application
                    .WithJit(Jit.RyuJit) // Use LegacyJIT instead of the default RyuJIT
                    .WithGcServer(true) // Use Server GC
                    //.WithEnvironmentVariable("Key", "Value") // Setting an environment variable
                    //.WithWarmupCount(0) // Disable warm-up stage
                );

            foreach (var l in DefaultConfig.Instance.GetLoggers())
                myConfig.AddLogger(l);
            #endregion

            _ = BenchmarkRunner.Run<MethodWrapper>();
        }
    }
}
