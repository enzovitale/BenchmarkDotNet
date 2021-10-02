using System.Linq;

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
                    .WithRuntime(CoreRuntime.Core50)
                    .WithPlatform(Platform.X64)
                    .WithJit(Jit.RyuJit)
                    .WithGcServer(true)
                )

                // Adding second job
                .AddJob(Job.Default
                    .AsBaseline() // It will be marked as baseline
                    .WithRuntime(ClrRuntime.Net472)
                    .WithPlatform(Platform.X64)
                    .WithJit(Jit.RyuJit)
                    .WithGcServer(true)
                    //.WithEnvironmentVariable("Key", "Value") // Setting an environment variable
                    //.WithWarmupCount(0) // Disable warm-up stage
                );


            var defaultConfig = DefaultConfig.Instance;

            myConfig.AddColumnProvider(defaultConfig.GetColumnProviders().ToArray());
            myConfig.AddExporter(defaultConfig.GetExporters().ToArray());
            myConfig.AddDiagnoser(defaultConfig.GetDiagnosers().ToArray());
            myConfig.AddAnalyser(defaultConfig.GetAnalysers().ToArray());
            //myConfig.AddJob(defaultConfig.GetJobs().ToArray());
            myConfig.AddValidator(defaultConfig.GetValidators().ToArray());
            //myConfig.UnionRule = ConfigUnionRule.AlwaysUseGlobal; // Overriding the default
            myConfig.AddLogger(defaultConfig.GetLoggers().ToArray());
            #endregion

            _ = BenchmarkRunner.Run<MethodWrapper>(myConfig);
        }
    }
}
