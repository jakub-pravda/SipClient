using System;

namespace SipClient.Helpers
{
    public static class SipSessionHelpers
    {
        public static string GenerateNewCallId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}