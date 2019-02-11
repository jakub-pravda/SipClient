using Javor.SipSerializer;

namespace SipClient.Models
{
    /// <summary>
    ///     Sip response handler representation.
    /// </summary>
    public interface ISipResponseHandler
    {
        bool ProcessSipMessage(SipResponseMessage sipResponse);
    }
}