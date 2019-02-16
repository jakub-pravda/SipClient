using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Schemes;
using SipClient.Models;

namespace SipClient.Extensions
{
    /// <summary>
    ///     Sip account extensions.
    /// </summary>
    public static class AccountExtensions
    {
        /// <summary>
        ///     Creates user identification from sip account.
        /// </summary>
        /// <param name="account">Sip client account.</param>
        /// <returns>Identification.</returns>
        public static Identification GetAccountIdentification(this SipClientAccount account)
        {
            SipUri identification = new SipUri(account.RegistrarUri.Host, account.User);
            return new Identification(identification, null);
        }
    }
}