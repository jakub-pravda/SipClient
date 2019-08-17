using Javor.SipSerializer.Exceptions;
using System;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     CSeq header field
    /// </summary>
    public class CSeq
    {
        /// <summary>
        ///     CSeq method.
        /// </summary>
        public string Method { get; private set; }
        /// <summary>
        ///     CSeq sequence number.
        /// </summary>
        public int SequenceNumber { get; private set; }

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

        /// <summary>
        ///     Convert CSeq into the ascii form.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{SequenceNumber} {Method}";
        }

        /// <summary>
        ///     Create new cseq from raw cseq value
        /// </summary>
        /// <param name="s">Raw cseq value</param>
        /// <returns>New cseq</returns>
        public static CSeq Parse(string s)
        {
            if (s == null) return null;

            string[] splitted = s.Split( new char[] {' '}, 2);

            if (splitted.Length != 2)
                throw new SipParsingException($"Invalid cseq line: {s}");

            return new CSeq(int.Parse(splitted[0]), splitted[1]);
        }
    }
}