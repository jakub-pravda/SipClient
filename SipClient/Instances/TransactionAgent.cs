using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Helpers;
using Javor.SipSerializer.Schemes;
using SipClient.Logging;
using SipClient.Models;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Javor.SipSerializer.SipMessage;

namespace SipClient.Instances
{
    /// <summary>
    ///     Transaction complete event handler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TransactionCompleteHandler(object sender, TransactionCompleteEventArgs e);

    /// <summary>
    ///     Implementation of Sip transaction layer.
    /// </summary>
    public class TransactionAgent : ISipTransactionLayer, IDisposable
    {
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private bool _disposed = false;
        private Via _via;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     Remote connection info.
        /// </summary>
        public SipUri SipUri { get; }

        /// <summary>
        ///     Listening ip address.
        /// </summary>
        public string ListeningOnIp { get; private set; }

        /// <summary>
        ///     Transaction complete event.
        /// </summary>
        public event TransactionCompleteHandler TransactionComplete;

        /// <summary>
        ///    Transport protocol used for message transport.
        /// </summary>
        TransportProtocol TransportProtocol { get; } = TransportProtocol.UDP; // only UDP is valid at this time

        ConcurrentDictionary<string, Transaction> Transactions { get; }
            = new ConcurrentDictionary<string, Transaction>();

        /// <summary>
        ///     Initialize new transaction layer.
        /// </summary>
        /// <param name="connectionInfo"></param>
        public TransactionAgent(SipUri destinationSipUri)
        {
            SipUri = destinationSipUri;
        }

        /// <summary>
        ///     Send sip request as new transaction.
        /// </summary>
        /// <param name="request">Sip message.</param>
        /// <param name="destination">User agent server uri.</param>
        /// <param name="destinationPort">User agent server port.</param>
        /// <param name="waitForResponse"></param>
        /// <returns></returns>
        public async Task<bool> SendSipRequestAsync(SipRequestMessage request, bool waitForResponse = false)
        {
            if (request == null) throw new ArgumentNullException("Request message cann't be null.");


            SetNewTransaction(request);

            _logger.Debug(string.Format("Sending SIP message {0} to {1}", request.ToString(), SipUri.ToString()));
            bool sendResult = await SendData(Encoding.ASCII.GetBytes(request.ToString()), SipUri.Host, SipUri.Port); // only ip address is valid at this time

            return sendResult;
        }

        /// <summary>
        ///     Start listening.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public void StartListening(string ipAddress, int port)
        {
            ListeningOnIp = ipAddress;
            IPAddress ipAddr = IPAddress.Parse(ipAddress);
            IPEndPoint ep = new IPEndPoint(ipAddr, port);

            StartListeningAsync(ep);
        }

        /// <summary>
        ///     Start listening.
        /// </summary>
        /// <param name="localEP"></param>
        /// <returns></returns>
        public void StartListeningAsync(IPEndPoint localEP)
        {
            _logger.Debug("Starting listener.");

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            listenSocket.Bind(localEP);

            _via = new Via(localEP.Address.ToString(), localEP.Port, TransportProtocol);
            _cancellationTokenSource = new CancellationTokenSource();

            _logger.Info($"Listener started at {localEP.Address}:{localEP.Port}.");

            EndPoint ie = new IPEndPoint(IPAddress.Any, 0);
            BufferSegment segment = new BufferSegment() { Buffer = ArrayPool<byte>.Shared.Rent(1024) };

            Tuple<Socket, BufferSegment> container = new Tuple<Socket, BufferSegment>(listenSocket, segment);
            listenSocket.BeginReceiveFrom(segment.Buffer, 0, 1024, SocketFlags.None, ref ie, Receive, container);
        }

        public void StopListening()
        {
            _logger.Debug("Stopping listening agent.");
            _cancellationTokenSource.Cancel();
            _logger.Info("Listening agent stopped.");
        }

        protected virtual void OnTransactionComplete(Transaction transaction)
        {
            TransactionCompleteHandler del = TransactionComplete as TransactionCompleteHandler;
            if (del != null)
            {
                del(this, new TransactionCompleteEventArgs(transaction));
            }
        }

        private void SetNewTransaction(SipRequestMessage initialRequest)
        {
            if (_via == null) throw new ArgumentNullException("VIA SIP header cann't be empty. Listening agent was not properly started.");

            // generate new VIA message
            initialRequest.Headers.Via.Add(_via);

            // generate new transaction
            Transaction tr = new Transaction(initialRequest);

            bool result = Transactions.TryAdd(_via.Branch, tr);
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

        private void Receive(IAsyncResult result)
        {
            Tuple<Socket, BufferSegment> container = (Tuple<Socket, BufferSegment>)result.AsyncState;
            Socket listenSocket = container.Item1;
            BufferSegment bs = container.Item2;

            EndPoint clientEp = new IPEndPoint(IPAddress.Any, 0);
            int recvBytes = listenSocket.EndReceiveFrom(result, ref clientEp);

            string recvMsg;
            while (true)
            {
                _logger.Debug("Recieving new bunch of data from socket.");
                recvMsg = Encoding.ASCII.GetString(bs.Buffer, 0, recvBytes);

                if (ParsingHelpers.IsSipMessageComplete(recvMsg)) break;
                throw new NotImplementedException("Message wasn't full recieved at one method calling.");
            }

            // process sip message
            SipMessageType messageType = ParsingHelpers.GetSipMessageType(recvMsg);

            SipMessage sipMessage;
            if (messageType == SipMessageType.Request)
            {
                throw new NotImplementedException("SIP REQUEST decoder not yet implemented.");
            }
            else if (messageType == SipMessageType.Response)
            {
                sipMessage = SipResponseMessage.CreateSipResponse(recvMsg);
            }
            else
            {
                _logger.Warn($"Cann't recognize if incomming SIP message is type of REQUEST or RESPONSE:\n {recvMsg}");
                throw new ArgumentException("Incomming sip message is invalid... unknown message type.");
            }
            _logger.Debug($"New sip message received:\n{sipMessage}");

            Via transactionInfo = sipMessage.Headers.Via.First();
            if (transactionInfo.IpAddress == ListeningOnIp)
            {
                // handle local transaction
                bool transactionResult = Transactions.TryGetValue(transactionInfo.Branch, out Transaction transaction);
                if (!transactionResult)
                {
                    throw new NotImplementedException($"Cann't find transaction with transaction id {transactionInfo.Branch}.");
                }

                if (transaction.SetResponse((SipResponseMessage)sipMessage))
                {
                    _logger.Info($"Transacion {transactionInfo.Branch} complete.");
                    OnTransactionComplete(transaction);
                }
                else
                {
                    throw new NotImplementedException("Provisional response handling not yet implemented.");
                }
            }
            else
            {
                throw new NotImplementedException("Transaction is determined for different endpoint.");
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
                StopListening();
            }

            _disposed = true;
        }

        ~TransactionAgent()
        {
            Dispose(false);
        }
    }

    class BufferSegment
    {
        public byte[] Buffer { get; set; }
        public int Count { get; set; }

        public int Remaining
        {
            get
            {
                return Buffer.Length - Count;
            }
        }
    }

    public class TransactionCompleteEventArgs
    {
        public Transaction Transaction { get; private set; }

        public TransactionCompleteEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
