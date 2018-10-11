using System.Collections.Generic;
using SipClient.Models;

namespace SipClient
{
    public interface ISipClient
    {
        IEnumerable<string> AllowedMethods { get; set; }
        SipClientAccount Account { get; }
        
        SipSession GetSipSession();
    }
}