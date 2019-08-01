using System.IO;
using System.Threading.Tasks;
using Javor.SipSerializer.Exceptions;
using System.Diagnostics;
using Javor.SipSerializer.HeaderFields;
using Xunit;

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

            RawSipMessage roSipMessage = new RawSipMessage(sipTest);

            // *** ACT ***
            string resultFrom = roSipMessage.GetHeaderValue("FROM");
            string resultVia = roSipMessage.GetHeaderValue("Via");
            string resultChargingVector = roSipMessage.GetHeaderValue("P-Charging-Vector");
            string resultContentLength = roSipMessage.GetHeaderValue("Content-Length");

            // *** ACT ***
            Assert.Equal(" \"Test JPR\" < sip:+420225006110@algocloud.net >; tag = 80eea88d4a11e71dda58b0b03400", resultFrom);
            Assert.Equal(" SIP / 2.0 / UDP 91.241.11.242:5060; branch = z9hG4bK - s1632 - 001300246567 - 1--s1632 -", resultVia);
            Assert.Equal(" icid-value=\"AAS:50-8da8ee801e7114ab058da0c34b0\"", resultChargingVector);
            Assert.Equal(" 258", resultContentLength);
        }

        [Theory]
        [MemberData(nameof(TestCases.TestData_SipResponses), MemberType = typeof(TestCases))]
        public void Deserialize_Sip_Response_Success(string sipResponse)
        {
            // *** ARRANGE ***

            // *** ACT ***
            var sw = new Stopwatch();

            // *** ACT ***
            sw.Start();
            SipResponse result = SipResponse.CreateSipResponse(sipResponse);
            sw.Stop();

            var test = sw.ElapsedMilliseconds;
        }

        [Fact]
        public void Serialize_Sip_Response_Success()
        {
            // *** ARRANGE ***
            StatusLine sl = new StatusLine(StatusCode.Ok);
            SipResponse sr = new SipResponse(sl);
            sr.AddHeader(@"From: ""100, 100"" <sip:+420225006100@algocloud.net>;tag=808849de3db6e61a53657e99dc300");
            sr.AddHeader("Call-ID: 2bbb9f20629f720f74adca5da4ad4052");
            sr.AddHeader("CSeq: 1 INVITE");
            sr.AddHeader("Via: SIP/2.0/UDP 91.241.11.242:5060;branch=z9hG4bK-s1632-001810362697-1--s1632-");
            sr.AddHeader("Via: SIP/2.0/TCP 192.168.26.91;branch=z9hG4bK808849de3db6e61a73657e99dc300");
            sr.AddHeader("To: <sip:80362@algocloud.net>;tag=808849de3db6e61d4225811465a00");

            // *** ACT ***
            string result = sr.ToString();
        }
    }
}