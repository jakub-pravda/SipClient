using System;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace SdpSeriallizer.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Summary summary = BenchmarkRunner.Run<BenchmarkSettings>();
        }
    }
}