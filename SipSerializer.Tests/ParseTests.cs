using System.IO;
using System.Threading.Tasks;
using Javor.SipSerializer.Exceptions;
using System.Diagnostics;
using Javor.SipSerializer.HeaderFields;
using Xunit;
using System;
using Javor.SipSerializer.Schemes;
using SipSerializer.Parsers;

namespace Javor.SipSerializer.Tests
{
    public class ParseTests
    {
        [Fact]
        public void Read_Sip_Message_Success()
        {
            // *** ARRANGE ***
            string sipTest = @"INVITE sip: 80362@algocloud.net SIP/ 2.0\r\n" 
                            + "FROM: \"Test JPR\" < sip:+420225006110@algocloud.net >; tag = 80eea88d4a11e71dda58b0b03400\r\n"
                            + "To: < sip:80362@algocloud.net >\r\n"
                            + "CSeq: 1 INVITE\r\n"
                            + "Call-ID: 9255ba2465765c49339cde4c46086852\r\n"
                            + "Contact: \"Test JPR\" < sip:+420225006110@91.241.11.242:5060; transport = udp; gsid = 80eea88d - 4a11 - 4701 - 8bda - 58b0b0340000 >\r\n"
                            + "Allow: INVITE, ACK, OPTIONS, BYE, CANCEL, SUBSCRIBE, NOTIFY, REFER, INFO, PRACK, PUBLISH, UPDATE\r\n"
                            + "Supported: 100rel, histinfo, join, replaces, timer\r\n"
                            + "Max-Forwards: 66\r\n"
                            + "Via: SIP / 2.0 / UDP 91.241.11.242:5060; branch = z9hG4bK - s1632 - 001300246567 - 1--s1632 -\r\n"
                            + "Via: SIP / 2.0 / TCP 10.10.11.215:15060; rport = 53669; ibmsid = local.1476854311683_977646_977657; branch = z9hG4bK663015852070684\r\n"
                            + "Via: SIP / 2.0 / TCP 10.10.11.215:15060; rport; ibmsid = local.1476854311683_977645_977656; branch = z9hG4bK767685979626846\r\n"
                            + "Accept-Language: en\r\n"
                            + "Alert-Info: < cid:internal @algocloud.net>;avaya-cm-alert-type=internal\r\n"
                            + "P-Asserted-Identity: \"Test JPR\" <sip:+420225006110@algocloud.net>\r\n"
                            + "Session-Expires: 1800;refresher=uac\r\n"
                            + "Min-SE: 1800\r\n"
                            + "Remote-LegId: 462\r\n"
                            + "Content-Type: application/sdp\r\n"
                            + "User-to-User: 00FA080001008858BFD6BD;encoding=hex\r\n"
                            + "P-AV-Message-Id: 1_1\r\n"
                            + "Max-Breadth: 60\r\n"
                            + "P-Charging-Vector: icid-value=\"AAS:50-8da8ee801e7114ab058da0c34b0\"\r\n"
                            + "Content-Length: 258\r\n";

            SipMessage roSipMessage = new SipMessage(sipTest);

            // *** ACT ***
            string resultFrom = roSipMessage.GetHeaderValue("FROM")[0];
            string[] resultVia = roSipMessage.GetHeaderValue("Via");
            string resultChargingVector = roSipMessage.GetHeaderValue("P-Charging-Vector")[0];
            string resultContentLength = roSipMessage.GetHeaderValue("Content-Length")[0];

            // *** ACT ***
            Assert.Equal(" \"Test JPR\" < sip:+420225006110@algocloud.net >; tag = 80eea88d4a11e71dda58b0b03400", resultFrom);
            Assert.Equal(" icid-value=\"AAS:50-8da8ee801e7114ab058da0c34b0\"", resultChargingVector);
            Assert.Equal(" 258", resultContentLength);
        }

        [Fact]
        public void Create_Lazy_Sip_Message_Success()
        {
            // *** ARRANGE ***
            string sipTest = @"INVITE sip: 80362@algocloud.net SIP/ 2.0\r\n" 
                            + "FROM: \"Test JPR\" < sip:+420225006110@algocloud.net >; tag = 80eea88d4a11e71dda58b0b03400\r\n"
                            + "To: < sip:80362@algocloud.net >\r\n"
                            + "CSeq: 1 INVITE\r\n"
                            + "Call-ID: 9255ba2465765c49339cde4c46086852\r\n"
                            + "Contact: \"Test JPR\" < sip:+420225006110@91.241.11.242:5060; transport = udp; gsid = 80eea88d - 4a11 - 4701 - 8bda - 58b0b0340000 >\r\n"
                            + "Allow: INVITE, ACK, OPTIONS, BYE, CANCEL, SUBSCRIBE, NOTIFY, REFER, INFO, PRACK, PUBLISH, UPDATE\r\n"
                            + "Supported: 100rel, histinfo, join, replaces, timer\r\n"
                            + "Max-Forwards: 66\r\n"
                            + "Via: SIP / 2.0 / UDP 91.241.11.242:5060; branch = z9hG4bK - s1632 - 001300246567 - 1--s1632 -\r\n"
                            + "Accept-Language: en\r\n"
                            + "Alert-Info: < cid:internal @algocloud.net>;avaya-cm-alert-type=internal\r\n"
                            + "P-Asserted-Identity: \"Test JPR\" <sip:+420225006110@algocloud.net>\r\n"
                            + "Session-Expires: 1800;refresher=uac\r\n"
                            + "Min-SE: 1800\r\n"
                            + "Remote-LegId: 462\r\n"
                            + "Content-Type: application/sdp\r\n"
                            + "User-to-User: 00FA080001008858BFD6BD;encoding=hex\r\n"
                            + "P-AV-Message-Id: 1_1\r\n"
                            + "Max-Breadth: 60\r\n"
                            + "P-Charging-Vector: icid-value=\"AAS:50-8da8ee801e7114ab058da0c34b0\"\r\n"
                            + "Content-Length: 258\r\n";

            LazySipMessage lazyMessage = new LazySipMessage(sipTest);

            // *** ACT ***
            string resultFrom = lazyMessage.getFromHeaderValue();
            string resultVia = lazyMessage.getViaHeaderValue();
            string resultContentLength = lazyMessage.getContentLengthHeaderValue();

            // *** ACT ***
            Assert.Equal(" \"Test JPR\" < sip:+420225006110@algocloud.net >; tag = 80eea88d4a11e71dda58b0b03400", resultFrom);
            Assert.Equal(" SIP / 2.0 / UDP 91.241.11.242:5060; branch = z9hG4bK - s1632 - 001300246567 - 1--s1632 -", resultVia);
            Assert.Equal(" 258", resultContentLength);
        }

        [Theory]
        [ClassData(typeof(SipMessageTypeTestData))]
        public void Get_Raw_Sip_Message_Type_Success(string sipMessage, SipMessageType messageTypeExpected)
        {
            // *** ARRANGE ***
            SipMessage rawMessage = new SipMessage(sipMessage);

            // *** ACT ***
            SipMessageType messageTypeResult = rawMessage.GetMessageType();

            // *** ASSERT ***
            Assert.Equal(messageTypeExpected, messageTypeResult);
        }

        [Theory]
        [InlineData("\"Test JPR\" <sip:+420225006110@algocloud.net>;tag=80eea88d4a11e71dda58b0b03400", "Test JPR", "sip:+420225006110@algocloud.net", "80eea88d4a11e71dda58b0b03400")]
        [InlineData("\"100, 100\" <sip:+420225006100@algocloud.net>;tag=808849de3db6e61a53657e99dc300", "100, 100", "sip:+420225006100@algocloud.net", "808849de3db6e61a53657e99dc300")]
        public void Create_Sip_Identification_Success(string identification, string expectedName, string expectedUri, string expectedTag)
        {
            // *** ACT ***
            Identification id = Identification.Parse(identification);

            // *** ASSERT ***
            Assert.Equal(expectedName, id.DisplayName);
            Assert.Equal(expectedUri, id.Uri.ToString());
            Assert.Equal(expectedTag, id.Tag);
        }

        [Theory]
        [InlineData("1 INVITE", 1, "INVITE")]
        [InlineData("5 BYE", 5, "BYE")]
        public void Create_Sip_Cseq_Success(string cseq, int expectedSqNumber, string expectedMethod)
        {
            // *** ACT ***
            CSeq cs = CSeq.Parse(cseq);

            // *** ASSERT ***
            Assert.Equal(expectedSqNumber, cs.SequenceNumber);
            Assert.Equal(expectedMethod, cs.Method);
        }

        [Theory]
        [InlineData("1INVITE")]
        [InlineData("")]
        public void Create_Sip_Cseq_Unsuccess(string cseq)
        {
            // *** ACT ***
            Action act = () => CSeq.Parse(cseq);

            // *** ACT ***
            Assert.Throws<SipParsingException>(act);
        }

        [Theory]
        [InlineData("sip:+420225006110@algocloud.net", "sip", "+420225006110", "algocloud.net", 5060)]
        [InlineData("sips:80362@algocloud.net:6666", "sips", "80362", "algocloud.net", 6666)]
        public void Create_Sip_SipUri_Success(string sipUri, string expectedScheme, string expectedUser, string expectedHost, int expectedPort)
        {
            // *** ACT ***
            SipUri su = SipUri.Parse(sipUri);

            // *** ASSERT ***
            Assert.Equal(expectedScheme, su.Scheme);
            Assert.Equal(expectedHost, su.Host);
            Assert.Equal(expectedPort, su.Port);
            Assert.Equal(expectedUser, su.User);
        }

        [Theory]
        [InlineData("SIP/2.0/TCP 192.168.26.91;branch=z9hG4bK808849de3db6e61a73657e99dc300")]
        [InlineData("SIP/2.0/UDP 91.241.11.242:5060;branch=z9hG4bK-s1632-001810362697-1--s1632-")]
        [InlineData("SIP/2.0/TCP 10.10.11.216;branch=z9hG4bK663015852070684-AP;ft=16")]
        [InlineData("SIP/2.0/TCP 10.10.11.216;branch=z9hG4bK808849de3db6e61a73657e99dc300-AP;ft=21;received=10.10.11.216;rport=60664")]
        public void Create_Sip_Via_Success(string via)
        {
            // *** ACT ***
            Via v = new ViaParser().Parse(via);
            string vs = v.ToString();

            // *** ASSERT ***
            Assert.Equal(via, vs);
        }

        [Fact]
        public void Test()
        {
            SipHeader<CSeq> test = SipHeader<CSeq>.CreateHeader(() => CSeq.Parse("1 INVITE"));
        }
    }
}