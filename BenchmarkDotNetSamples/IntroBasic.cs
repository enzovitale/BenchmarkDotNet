using System.Threading;


namespace BenchmarkDotNetSamples.Samples
{
    public class IntroBasic
    {
        public void Sleep() => Thread.Sleep(30);

        public void SleepWithDescription() => Thread.Sleep(30);
    }
}