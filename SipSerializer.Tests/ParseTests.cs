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
        // [Theory]
        // [MemberData(nameof(TestCases.TestData_SipRequests), MemberType = typeof(TestCases))]
        // public void Deserialize_Sip_Request_Success(string sipRequest)
        // {
        //     // *** ARRANGE ***
        //     var sw = new Stopwatch();

        //     // *** ACT ***
        //     sw.Start();
        //     SipRequestMessage result = SipRequestMessage.CreateSipRequest(sipRequest);
        //     sw.Stop();

        //     var test = sw.ElapsedMilliseconds;

        //     // *** ASSERT ***
        // }

        [Theory]
        [MemberData(nameof(TestCases.TestData_SipResponses), MemberType = typeof(TestCases))]
        public void Deserialize_Sip_Response_Success(string sipResponse)
        {
            // *** ARRANGE ***

            // *** ACT ***
            var sw = new Stopwatch();

            // *** ACT ***
            sw.Start();
            SipReponseMessage result = SipReponseMessage.CreateSipResponse(sipResponse);
            sw.Stop();

            var test = sw.ElapsedMilliseconds;
        }

        [Fact]
        public void Serialize_Sip_Response_Success()
        {
            // *** ARRANGE ***
            StatusLine sl = new StatusLine(StatusCode.Ok);
            SipReponseMessage sr = new SipReponseMessage(sl);
            sr.AddHeader(@"From: ""100, 100"" <sip:+420225006100@algocloud.net>;tag=808849de3db6e61a53657e99dc300");
            sr.AddHeader("Call-ID: 2bbb9f20629f720f74adca5da4ad4052");
            sr.AddHeader("CSeq: 1 INVITE");
            sr.AddHeader("Via: SIP/2.0/UDP 91.241.11.242:5060;branch=z9hG4bK-s1632-001810362697-1--s1632-");
            sr.AddHeader("Via: SIP/2.0/TCP 192.168.26.91;branch=z9hG4bK808849de3db6e61a73657e99dc300");
            sr.AddHeader("To: <sip:80362@algocloud.net>;tag=808849de3db6e61d4225811465a00");

            // *** ACT ***
            string result = sr.ToString();
        }

        [Theory]
        [InlineData("INVITE sip:80362@algocloud.net SIP/2.0")]
        public void Parse_RequestLine_Success(string requestLineString)
        {
            Assert.NotNull(new RequestLine(requestLineString));
        }

        [Theory]
        [InlineData("SIP/2.0 200 OK")]
        public void Parse_RequestLine_Failed_Invalid_Input(string badRequestLineString)
        {
            Assert. Throws<SipParsingException>(() => new RequestLine(badRequestLineString));
        }
    }
}