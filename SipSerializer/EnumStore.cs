namespace Javor.SipSerializer
{
    /// <summary>
    ///     SIP Request URI schemes
    /// </summary>
    public enum Scheme  // TODO rfc 3261 rika, ze muze byt vice "schemes" kdyz je potreba, viz str 36
    {
        sip,
        sips
    }
    /// <summary>
    ///     List of meaningful SIP transport protocols
    /// </summary>
    public enum TransportProtocol
    {
        TCP,
        UDP
    }
}