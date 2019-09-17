namespace SipClient.Instances
{
    /// <summary>
    ///     Network listener interface
    /// </summary>
    public interface IListener
    {
        /// <summary>
        ///     Start with listening on the designated ip address:port
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        void StartListening(string ipAddress, int port);
        void StopListening();
    }
}