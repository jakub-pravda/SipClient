using System;
using System.Collections.Generic;
using System.Text;
using Javor.SdpSerializer;
using Javor.SdpSerializer.Extensions;
using Javor.SdpSerializer.Specifications;
using Javor.SdpSerializer.Tests.TestCases;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Xunit;

namespace SdpSerializer.Tests
{
    public class SdpSeserializationTests
    {
        /// <summary>
        ///     Simple decoding test.
        /// </summary>
        [Fact]
        public void Decode_With_Check_Success()
        {
            // *** ARRANGE ***
            StringBuilder sb = new StringBuilder();
            // Create test sdp message
            sb.AppendLine("v=0");
            sb.AppendLine("o=- 1488967357 1 IN IP4 91.241.11.242");
            sb.AppendLine("s=-");
            sb.AppendLine("c=IN IP4 91.241.11.242");
            sb.AppendLine("b=AS:64");
            sb.AppendLine("t=0 0");
            sb.AppendLine("a=avf:avc=n prio=n");
            sb.AppendLine("a=csup:avf-v0");
            sb.AppendLine("m=audio 35430 RTP/AVP 8 0 120");
            sb.AppendLine("a=sendrecv");
            sb.AppendLine("a=rtpmap:8 PCMA/8000");
            sb.AppendLine("a=rtpmap:0 PCMU/8000");
            sb.AppendLine("a=rtpmap:120 telephone-event/8000");
            sb.AppendLine("a=ptime:20");
            string sdpMessage = sb.ToString();
            
            // *** ACT ***
            //SessionDescription result = SessionDescription.Deserialize(sdpMessage);
        }

        /// <summary>
        ///     Simple encoding test. Encoded SDP is compared with original class.
        /// </summary>
        [Fact]
        public void Encode_With_Check_Success()
        {
            // *** ARRANGE ***
            SessionDescription sd = new SessionDescription();
            sd.Origin =  new SessionOrigin("-", "1488967357", "1", "IN", "IP4", "91.241.11.242");
            sd.ConnectionData = new SessionConnectionData("IN", "IP4", "91.241.11.242");
            sd.Timing = new SessionTiming();
            sd.AddBandwidth(new SessionBandwidth(BandwithTypes.ApplicationSpecific, 4096));
            
            // *** ACT ***
            string result = sd.Encode();
        }

        /// <summary>
        ///     Sdp specifications success test.
        /// </summary>
        [Fact]
        public void Encode_Single_Sdp_Specifications()
        {
            // *** ARRANGE ***
            foreach (SdpSpecTestContainer testObject in SdpSpecTestContainer.TestObjects)
            {
                // *** ACT ***
                string result = testObject.Field.Encode();
                
                // *** ASSERT ***
                Assert.Equal(testObject.Expected, result);
            }
        }
    }
}