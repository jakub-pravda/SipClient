using System.Collections.Generic;
using SipClient.Models;

namespace SipClient
{
    public class DefaultSipClient : ISipClient
    {
        public SipClientAccount Account { get; private set; }

        public DefaultSipClient(SipClientAccount account)
        {
            Account = account;
        }

        public IEnumerable<string> AllowedMethods { get; set; }

        private SipSession _sipSession;
        public SipSession GetSipSession()
        {
            throw new System.NotImplementedException();
        }
    }
}