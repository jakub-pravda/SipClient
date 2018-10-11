using System;
using System.Collections.Generic;
using System.Net;
using SipClient.Helpers;
using Javor.SipSerializer;

namespace SipClient.Models
{
    /// <summary>
    ///     Sip session.
    /// </summary>
    public class SipSession : IDisposable
    {
        public int SequenceNumber { get; private set; } = 1;
        public string Username { get; set; }
        public Uri Destination { get; set; }
        public string SessionId { get; set; }
        public IEnumerable<string> AllowedMethods { get; set; }

        public SipSession()
        {
            SessionId = SipSessionHelpers.GenerateNewCallId();
        }

        public SipSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("Session ID cann't be null or emptty.");

            SessionId = sessionId;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISipSession
    {
        Result SendSipRequest(SipRequestMessage request);
        Result SendSipResponse(SipReponseMessage response);
    } 
}