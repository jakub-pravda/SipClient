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
}