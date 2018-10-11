using System;
using Javor.SdpSerializer.Helpers;
using Javor.SdpSerializer.Specifications;
using Xunit;

namespace Javor.SdpSerializer.Tests
{
    public class HelpersTests
    {
        [Fact]
        public void SpecificationHelper_GetDescriptionField_Success()
        {
            // *** ARRANGE ***
            SdpVersion test1 = new SdpVersion(); // expected v=
            SessionOrigin test2 = new SessionOrigin(); // expected o=
            SessionTiming test3 = new SessionTiming(); // expected t=
            
            // *** ACT ***
            string result1 = SpecificationHelpers.GetDescriptionField(test1);
            string result2 = SpecificationHelpers.GetDescriptionField(test2);
            string result3 = SpecificationHelpers.GetDescriptionField(test3);
            
            // *** ASSERT ***
            Assert.Equal("v=", result1);
            Assert.Equal("o=", result2);
            Assert.Equal("t=", result3);
        }

        [Fact]
        public void Specificationhelper_CheckPortValidity_Success()
        {
            // *** ARRANGE ***
            int port1 = 22;
            int port2 = 1;
            int port3 = 65535;
            
            // *** ACT ***
            Boolean result1 = SpecificationHelpers.CheckPortValidity(port1);
            Boolean result2 = SpecificationHelpers.CheckPortValidity(port2);
            Boolean result3 = SpecificationHelpers.CheckPortValidity(port3);

            // *** ASSERT ***
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }
        
        [Fact]
        public void Specificationhelper_CheckPortValidity_InvalidPort()
        {
            // *** ARRANGE ***
            int port1 = -10;
            int port2 = 0;
            int port3 = 70000;
            
            // *** ACT ***
            Boolean result1 = SpecificationHelpers.CheckPortValidity(port1);
            Boolean result2 = SpecificationHelpers.CheckPortValidity(port2);
            Boolean result3 = SpecificationHelpers.CheckPortValidity(port3);

            // *** ASSERT ***
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }
        
        [Fact]
        public void Specificationhelper_CreateBandwidth_Success()
        {
            // *** ARRANGE ***
            string bwField = "b=AS:64";
            SessionBandwidth expected = new SessionBandwidth(BandwithTypes.ApplicationSpecific, 64);
            
            // *** ACT ***
            SessionBandwidth result = SpecificationHelpers.CreateBandwidth(bwField);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }

        [Fact]
        public void Specificationhelper_CreateConnectionData_Success()
        {
            // *** ARRANGE ***
            string cdField = "c=IN IP4 91.241.11.236";
            SessionConnectionData expected = new SessionConnectionData("IN", "IP4", "91.241.11.236");
            
            // *** ACT ***
            SessionConnectionData result = SpecificationHelpers.CreateConnectionData(cdField);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }

        [Fact]
        public void Specificationhelper_CreateSessionMedia_Success()
        {
            // *** ARRANGE ***
            string smField = "m=audio 49170 RTP/AVP 0 8 97";
            string[] smaFields = new string[]
            {
                "a=rtpmap:0 PCMU/8000",
                "a=rtpmap:8 PCMA/8000",
                "a=rtpmap:97 iLBC/8000"
            };
            
            SessionMedia expected = new SessionMedia("audio", 49170, "RTP/AVP", "0", "8", "97");
            expected.AddMediaValueAttribute("rtpmap", "0 PCMU/8000");
            expected.AddMediaValueAttribute("rtpmap", "8 PCMA/8000");
            expected.AddMediaValueAttribute("rtpmap", "97 iLBC/8000");
            
            // *** ACT ***
            SessionMedia result = SpecificationHelpers.CreateMedia(smField, smaFields);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }

        [Fact]
        public void Specificationhelper_CreateSessionOrigin_Success()
        {
            // *** ARRANGE ***
            string orField = "o=bob 2890844656 2890844656 IN IP4 bobpc.example.com";
            SessionOrigin expected = new SessionOrigin(
                "bob", 
                "2890844656", 
                "2890844656",
                "IN",
                "IP4",
                "bobpc.example.com");
            
            // *** ACT ***
            SessionOrigin result = SpecificationHelpers.CreateOrigin(orField);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }
        
        [Fact]
        public void Specificationhelper_CreateSessionAttribute_Success()
        {
            // *** ARRANGE ***
            string flagAtField = "a=recvonly";
            string varAtField = "a=path:msrp://bobpc.example.com:8888/9di4ea;tcp";

            SessionAttribute flagAtExpected = new SessionAttribute("recvonly");
            SessionAttribute varAtExpected = new SessionAttribute("path", "msrp://bobpc.example.com:8888/9di4ea;tcp");
            
            // *** ACT ***
            SessionAttribute flagAtResult = SpecificationHelpers.CreateAttribute(flagAtField);
            SessionAttribute varAtResult = SpecificationHelpers.CreateAttribute(varAtField);
            
            // *** ASSERT ***
            string efEncoded = flagAtExpected.Encode();
            string rfEncoded = flagAtResult.Encode();            
            Assert.Equal(efEncoded, rfEncoded);
            
            string evEncoded = varAtExpected.Encode();
            string rvEncoded = varAtResult.Encode();            
            Assert.Equal(evEncoded, rvEncoded);
        }
        
        [Fact]
        public void Specificationhelper_CreateSessionName_Success()
        {
            // *** ARRANGE ***
            string snField = "s=test";
            SessionName expected = new SessionName("test");
            
            // *** ACT ***
            SessionName result = SpecificationHelpers.CreateSessionName(snField);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }
        
        [Fact]
        public void Specificationhelper_CreateSessionTiming_Success()
        {
            // *** ARRANGE ***
            string tmField = "s=0 9999";
            SessionTiming expected = new SessionTiming(0, 9999);
            
            // *** ACT ***
            SessionTiming result = SpecificationHelpers.CreateTiming(tmField);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }
        
        [Fact]
        public void Specificationhelper_CreateSdpVersion_Success()
        {
            // *** ARRANGE ***
            string vrField = "v=1";
            SdpVersion expected = new SdpVersion("1");
            
            // *** ACT ***
            SdpVersion result = SpecificationHelpers.CreateVersion(vrField);

            // *** ASSERT ***
            string eEncoded = expected.Encode();
            string rEncoded = result.Encode();            
            Assert.Equal(eEncoded, rEncoded);
        }
    }
}