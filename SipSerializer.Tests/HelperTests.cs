using Javor.SipSerializer.Extensions;
using Javor.SipSerializer.Models;
using System;
using Xunit;

namespace Javor.SipSerializer.Tests
{
    public class HelperTests
    {
        [Fact]
        public void Required_ModelValidation_Success()
        {
            // *** ARRANGE ***
            SipRegisterOptions registerOptions = new SipRegisterOptions()
            {
                CallId = "9255ba2465765c49339cde4c46086852",
                To = @"""Test JPR"" <sip:+420225006110@algocloud.net>;tag=80eea88d4a11e71dda58b0b03400",
                RequestUri = new Uri("sip:80362@algocloud.net"),
                SequenceNumber = 1
            };

            // *** ACT ***
            bool result = registerOptions.IsValid();

            // *** ASSERT ***
            Assert.True(result);
        }

        [Fact]
        public void Required_ModelValidation_Failed_Missing_Required()
        {
            // *** ARRANGE ***
            SipRegisterOptions registerOptions = new SipRegisterOptions()
            {
                CallId = "9255ba2465765c49339cde4c46086852",
                SequenceNumber = 1
            };

            // *** ACT ***
            bool result = registerOptions.IsValid();

            // *** ASSERT ***
            Assert.False(result);
        }
    }
}
