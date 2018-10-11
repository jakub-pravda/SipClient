using SipClient.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace SipClient
{
    public interface ISipBuilder
    {
        IEnumerable<string> AllowedMethods { get; set; }
        Uri RegistrarUri { get; set; }
        SipClientAccount ClientAccount { get; set; }
    }

    public class SipBuilder : ISipBuilder
    {
        public IEnumerable<string> AllowedMethods { get; set; }
        public Uri RegistrarUri { get; set; }
        public SipClientAccount ClientAccount { get; set; }
    }
}