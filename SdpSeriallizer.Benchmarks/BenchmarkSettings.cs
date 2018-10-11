using BenchmarkDotNet.Attributes;
using Javor.SdpSerializer;
using Javor.SdpSerializer.Extensions;
using Javor.SdpSerializer.Helpers;
using Javor.SdpSerializer.Specifications;

namespace SdpSeriallizer.Benchmarks
{
    public class BenchmarkSettings
    {
        public SessionDescription TestData { get; }
        
        public BenchmarkSettings()
        {
            SessionDescription sd = new SessionDescription();
            sd.Origin =  new SessionOrigin("-", "1488967357", "1", "IN", "IP4", "91.241.11.242");
            sd.ConnectionData = new SessionConnectionData("IN", "IP4", "91.241.11.242");
            sd.Timing = new SessionTiming();

            sd.AddBandwidth(new SessionBandwidth(BandwithTypes.ApplicationSpecific, 4096));
            
            TestData = sd;
        }
        
        [Benchmark]
        public void SdpEncodingBenchmark()
        {
            string testData = TestData.Encode();
        }
    }
}