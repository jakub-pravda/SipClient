using System;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer.Helpers
{
    public static class DialogueHelpers
    {
        public static string GenerateCallId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
