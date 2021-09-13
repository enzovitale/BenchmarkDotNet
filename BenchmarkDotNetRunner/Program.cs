using BenchmarkDotNet.Running;

using BenchmarkDotNetSamples.Samples;


namespace BenchmarkDotNetSamples.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<IntroBasic>();
        }
    }
}
