using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using SipClient.Extensions;
using SipClient.Instances;
using SipClient.Logging;
using SipClient.Models;

[assembly: InternalsVisibleTo("SipClient.Instances.LogProvider")]
namespace SipClient
{
    /// <summary>
    ///     Default instance of the sip client.
    /// </summary>
    public class DefaultSipClient : ISipClient, IDisposable
    {
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();

        public string SipVersion { get; } = Constants.SipVersion;

        public SipClientAccount Account { get; private set; }

        public IEnumerable<string> AllowedMethods { get; set; }

        public ISipTransactionLayer TransactionLayer { get; }

        public DefaultSipClient(SipClientAccount account)
        {
            if (account == null) throw new ArgumentNullException("Invalid account.");

            Account = account;
            TransactionLayer = new TransactionAgent(Account.RegistrarUri);
        }

        public DefaultSipClient(SipClientAccount account, IEnumerable<string> allowedMethods)
            : this(account)
        {
            AllowedMethods = allowedMethods ?? throw new ArgumentNullException("Allowed methods cann't be null.");
        }

        /// <summary>
        ///     Creates new dialogue for communication with registrar server.
        /// </summary>
        /// <returns>New sip dialogue.</returns>
        public SipDialogue GetNewDialogue(string initRequest)
        {
            SipDialogue sd = new SipDialogue(
                initRequest, 
                Account.RegistrarUri, 
                Account.GetAccountIdentification(),
                TransactionLayer);
            return sd;
        }

        /// <summary>
        ///     Creates new dialogue for communication with sip endpoints.
        /// </summary>
        /// <param name="to">Endpoint identification.</param>
        /// <returns>New sip dialogue.</returns>
        public SipDialogue GetNewDialogue(Identification to)
        {
            // in case that i would need call to some number, sip client doesnt know TO destination
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    // implementace - budu mit klasicky sip client - gro sip clienta bude sendMessage metoda,
    // ktera umozni poslat SipMessage a zaroven Function(), ve ktere bude nadefinovano co bude
    // se stane, kdyz prijde nejaky response, pokdu funkce nebude definovana, tak bude existovat
    // sada defaultnich response handleru 201 - nativne proste znamena OK - vymyslet jak metody
    // resposnsu nejak galantne dedit od tech "nativnich"... moznost pouze rozsirit funkcionalitu
    // Veskera lofika (REGISTER, TW handshake) bude definovana v ISipCLient Extensions... ale v ramci toho
    // budu volat SipOperation (nebudu mit pro kazdou logiku separatni class)
    // PROBLEM - bude se neztratit u vice rozvetvenych struktur => resp jak treba handlovat call (ktery je slozen
    // z vice metod...)
}