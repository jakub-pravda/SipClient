using Javor.SipSerializer;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Javor.SipSerializer.Tests
{
    public class TestCases
    {
        /// <summary>
        ///     Sip request message collection for testing.
        /// </summary>
        public static IEnumerable<object[]> TestData_SipRequests =>
            new List<string[]>
            {
                new string[] { File.ReadAllText("./TestCases/req_sip_INVITE_source_avaya_sbc") }
            };
        
        /// <summary>
        ///     Sip response message collection for testing.
        /// </summary>
        public static IEnumerable<object[]> TestData_SipResponses =>
            new List<string[]>
            {
                new string[] { File.ReadAllText("./TestCases/res_sip_200Ok_source_avaya_sbc") }
            };
    }

    /// <summary>
    ///  Sip message type test cases
    /// </summary>
    public class SipMessageTypeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { File.ReadAllText("./TestCases/res_sip_200Ok_source_avaya_sbc"), SipMessageType.Response };
            yield return new object[] { File.ReadAllText("./TestCases/req_sip_INVITE_source_avaya_sbc"), SipMessageType.Request };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}