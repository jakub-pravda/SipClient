using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Javor.SipSerializer;
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

        public ISipTransactionUser TransactionUser { get; private set; }

        public IEnumerable<string> AllowedMethods { get; set; }

        public ISipTransactionLayer TransactionLayer { get; }

        public DefaultSipClient(IPAddress listenOn, int port, SipClientAccount account)
        {
            if (listenOn == null) throw new ArgumentNullException("Client ip address cann't be null.");
            if (account == null) throw new ArgumentNullException("Invalid account.");
            if (port < 1 || port > 65535) throw new ArgumentException($"Invalid port {port}.");

            Account = account;
            TransactionLayer = new TransactionAgent(listenOn, port, Account.RegistrarUri);
        }

        public DefaultSipClient(IPAddress listenOn, int port, SipClientAccount account, IEnumerable<string> allowedMethods)
            : this(listenOn, port, account)
        {
            AllowedMethods = allowedMethods ?? throw new ArgumentNullException("Allowed methods cann't be null.");
        }

        public SipDialogue GetNewDialogue()
        {
            SipDialogue sd = new SipDialogue(Guid.NewGuid(), TransactionLayer, Account.RegistrarUri);
            return sd;
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