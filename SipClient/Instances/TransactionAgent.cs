using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Schemes;
using SipClient.Logging;
using SipClient.Models;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SipClient.Instances
{
    /// <summary>
    ///     Implementation of Sip transaction layer.
    /// </summary>
    public class TransactionAgent : ISipTransactionLayer, IDisposable
    {
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private bool _disposed = false;

        /// <summary>
        ///     Remote connection info.
        /// </summary>
        public SipUri SipUri { get; }

        /// <summary>
        ///     Local ip address on which is SIP client listening.
        /// </summary>
        public IPAddress LocalIpAddress { get; }

        /// <summary>
        ///     Local port on which is sip clilent listening
        /// </summary>
        public int LocalPort { get; }

        /// <summary>
        ///    Transport protocol used for message transport.
        /// </summary>
        TransportProtocol TransportProtocol { get; } = TransportProtocol.TCP; // only TCP is valid at this time

        ConcurrentDictionary<string, Transaction> Transactions { get; }
            = new ConcurrentDictionary<string, Transaction>();

        /// <summary>
        ///     Initialize new transaction layer.
        /// </summary>
        /// <param name="connectionInfo"></param>
        public TransactionAgent(IPAddress localIpAddress, int localPort, SipUri destinationSipUri)
        {
            SipUri = destinationSipUri;
            LocalPort = localPort;
            LocalIpAddress = localIpAddress;
        }

        /// <summary>
        ///     Send sip request as new transaction.
        /// </summary>
        /// <param name="request">Sip message.</param>
        /// <param name="destination">User agent server uri.</param>
        /// <param name="destinationPort">User agent server port.</param>
        /// <param name="waitForResponse"></param>
        /// <returns></returns>
        public async Task<bool> SendSipRequest(SipRequestMessage request, bool waitForResponse = false)
        {
            if (request == null) throw new ArgumentNullException("Request message cann't be null.");

            byte[] data = Encoding.ASCII.GetBytes(request.ToString());

            SetNewTransaction(request);

            _logger.Debug(string.Format("Sending SIP message {0} to {1}", request.ToString(), SipUri.ToString()));

            bool sendResult = await SendData(data, SipUri.Host, SipUri.Port); // only ip address is valid at this time

            return sendResult;
        }

        private void SetNewTransaction(SipRequestMessage initialRequest)
        {
            Via via = new Via(LocalIpAddress.ToString(), LocalPort, TransportProtocol);
            
            // generate new VIA message
            initialRequest.Headers.Via.Add(via);

            // generate new transaction
            Transaction tr = new Transaction(initialRequest);

            bool result = Transactions.TryAdd(via.Branch, tr);
        }

        private async Task<bool> SendData(byte[] data, string domainAddress, int destinationPort)
        {
            bool parseResult = IPAddress.TryParse(domainAddress, out IPAddress destinationIp);

            if (!parseResult)
            {
                // domain address is uri
                destinationIp = (await Dns.GetHostAddressesAsync(domainAddress))[0];
            }
            return await SendData(data, destinationIp, destinationPort);
        }

        private async Task<bool> SendData(byte[] data, IPAddress destinationIpAddress, int destinationPort)
        {
            UdpClient udpClient = new UdpClient();

            try
            {
                udpClient.Connect(destinationIpAddress, destinationPort);
                await udpClient.SendAsync(data, data.Length);

                return true;
            }
            catch (SocketException e)
            {
                _logger.Error($"Error occured during connection to ip address {destinationIpAddress.ToString()}:\n {e.StackTrace}");

                return false;
            }
        }

        /// <summary>
        ///     Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // if disposing is true, method has been called directly by user code
            // if disposing is false, method has been called by the runtime from insade the finalizer

            if (_disposed)
                return;

            if (disposing)
            {
                // do something on dispose
            }

            _disposed = true;
        }

        ~TransactionAgent()
        {
            Dispose(false);
        }
    }
}
