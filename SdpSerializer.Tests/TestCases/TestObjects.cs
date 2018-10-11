using System.Collections.Generic;
using Javor.SdpSerializer.Specifications;

namespace Javor.SdpSerializer.Tests.TestCases
{
    /// <summary>
    ///     SDP specification orig - expected container.
    /// </summary>
    public class SdpSpecTestContainer
    {
        /// <summary>
        ///     Initialize new instance of orig - expected container,
        /// </summary>
        /// <param name="friendlyName">Friendly test name which is shown in the console when error occured,</param>
        /// <param name="field">Original SDP specification (non serialized).</param>
        /// <param name="expected">Expected SDP specification (serialized).</param>
        public SdpSpecTestContainer(string friendlyName, SdpField field, string expected)
        {
            FriendlyName = friendlyName;
            Field = field;
            Expected = expected;
        }

        /// <summary>
        ///     Friendly test name which is shown in the console when error occured,
        /// </summary>
        public string FriendlyName { get; set; }
        
        /// <summary>
        ///     Original SDP specification (non serialized).
        /// </summary>
        public SdpField Field { get; }
        
        /// <summary>
        ///     Expected SDP specification (serialized).
        /// </summary>
        public string Expected { get; }

        /// <summary>
        ///     Orig - expected container collection.
        /// </summary>
        public static IEnumerable<object> TestObjects = new SdpSpecTestContainer[]
        {
            // VERSION test cases
            new SdpSpecTestContainer(
                "Version encoding",
                field:    new SdpVersion(), 
                expected: "v=0"),
            
            // TIMING test cases
            new SdpSpecTestContainer(
                "Timing encoding",
                field:    new SessionTiming(0, 100), 
                expected: "t=0:100"),
            
            // BANDWITH test cases
            new SdpSpecTestContainer(
                "Bandwith encoding",
                field:    new SessionBandwidth(BandwithTypes.ApplicationSpecific, 64), 
                expected: "b=AS:64"),
        
            // CONNECTION DATA test cases
            new SdpSpecTestContainer(
                "Connection data #1 encoding",
                field:    new SessionConnectionData("IN", "IP4", "127.0.0.1"), 
                expected: "c=IN IP4 127.0.0.1"),
            new SdpSpecTestContainer(
                "Connection data #2 encoding",
                field:    new SessionConnectionData("IN", "IP4", "127.0.0.1"),
                expected: "c=IN IP4 127.0.0.1"),
        
            // MEDIA test cases
            new SdpSpecTestContainer(
                "Media encoding",
                field:    new SessionMedia("audio", 35062, "RTP/AVP", "93", "99", "9", "94", "102"), 
                expected: "m=audio 35062 RTP/AVP 93 99 9 94 102"),
        
            // ORIGIN test cases
            new SdpSpecTestContainer(
                "Origin encoding",
                field:    new SessionOrigin("-", "1528637752", "0", "IN", "IP4", "0.0.0.0"),
                expected: "o=- 1528637752 0 IN IP4 0.0.0.0"),
        
            // ATTRIBUTE test cases
            new SdpSpecTestContainer(
                "Attribute #flag encoding",
                field:    new SessionAttribute("sendonly"), 
                expected: "a=sendonly"),
            new SdpSpecTestContainer(
                "Attribute #value encoding",
                field:    new SessionAttribute("rtpmap" ,"31 h261/90000"), 
                expected: "a=rtpmap:31 h261/90000"),
        
            // SESSION NAME test cases
            new SdpSpecTestContainer(
                "Session name encoding",
                field:    new SessionName(), 
                expected: "s=-")
        };
    }

    public static class SessionDescription1
    {
        public static string Expected =           
            @"v=0
            o=- 1488967357 1 IN IP4 91.241.11.242
            s=-
            c=IN IP4 91.241.11.242
            b=AS:64
            t=0 0
            a=avf:avc=n prio=n
            a=csup:avf-v0
            m=audio 35430 RTP/AVP 8 0 120
            a=sendrecv
            a=rtpmap:8 PCMA/8000
            a=rtpmap:0 PCMU/8000
            a=rtpmap:120 telephone-event/8000
            a=ptime:20";

        public static SessionDescription GetObject()
        {
            SessionDescription sd = new SessionDescription();
            sd.Origin = new SessionOrigin("-", "1488967357", "1", "IN", "IP4", "91.241.11.242");
            sd.ConnectionData = new SessionConnectionData("IN", "IP4", "91.241.11.242");
            sd.Timing = new SessionTiming(0, 0);
            
            sd.AddBandwidth(new SessionBandwidth("AS", 64));
            
            return sd;
        }
    }
}