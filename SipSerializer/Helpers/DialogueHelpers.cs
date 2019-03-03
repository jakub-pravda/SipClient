using System;

namespace Javor.SipSerializer.Helpers
{
    /// <summary>
    ///     Collection of sip dialogue helpers.
    /// </summary>
    public static class DialogueHelpers
    {
        /// <summary>
        ///     Generates unique call id for unambigious dialogue identification.
        /// </summary>
        /// <returns>Call id.</returns>
        public static string GenerateCallId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        ///     This method generates unique tag designated for identification headers.
        /// </summary>
        /// <returns>Unique identification tag.</returns>
        public static string GenerateIdentificationTag()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
