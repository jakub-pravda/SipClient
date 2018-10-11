namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     CSeq header field
    /// </summary>
    public struct CSeq
    {
        /// <summary>
        ///     CSeq method.
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        ///     CSeq sequence number.
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        ///     Initialize new CSeq structure.
        /// </summary>
        /// <param name="sequenceNumber">Sequence number.</param>
        /// <param name="method">Sequence method.</param>
        public CSeq(int sequenceNumber, string method)
        {
            SequenceNumber = sequenceNumber;
            Method = method;
        }
    }
}