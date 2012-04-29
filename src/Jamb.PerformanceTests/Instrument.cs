using System;
using System.Diagnostics;

namespace Jamb.PerformanceTests
{
    public class Instrument
    {
        public static long Do(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();

            action();

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}