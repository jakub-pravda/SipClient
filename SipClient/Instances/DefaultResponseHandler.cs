using Javor.SipSerializer;
using SipClient.Logging;
using SipClient.Models;

namespace SipClient.Instances
{
    public class DefaultResponseHandler : ISipResponseHandler
    {
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();

        public bool ProcessSipMessage(SipResponseMessage sipResponse)
        {
            switch(sipResponse.StatusLine.StatusCode)
            {
                case ResponseCodes.StatusCode200Ok:
                    Process200Ok();
                    return true;

                default:
                    return false;
            }
        }

        private void Process200Ok()
        {
            _logger.Debug("Processing 200OK message via sip response handler.");
        }
    }
}