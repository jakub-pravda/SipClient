using Javor.SipSerializer;
using Javor.SipSerializer.Schemes;
using System;
using System.Net;

namespace SipClient.Models
{
    public class SipClientAccount
    {
        // **** USER INFORMATION ****

        public string Name { get; set; }
        public string User { get; set; }
        public string AuthenticationUser { get; set; }
        public string Password { get; set; } // TODO dont save password purely in string

        // **** CONNECTION INFORMATION ****
        public SipUri RegistrarUri { get; set; }

        public TransportProtocol TransportProtocol { get; set; }
    }
}