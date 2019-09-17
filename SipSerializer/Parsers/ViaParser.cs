using System;
using System.Net;
using Javor.SipSerializer;
using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;

namespace SipSerializer.Parsers
{
    /// <summary>
    ///     VIA parser
    /// </summary>
    public class ViaParser : IHeaderParser<Via>
    {
        /// <summary>
        ///     Get parser name
        /// </summary>
        public string GetParserName()
        {
            return HeaderName.Via;
        }

        /// <summary>
        ///     Convert string to the VIA class
        /// </summary>
        /// <param name="value">Header value</param>
        /// <returns>Parsed VIA</returns>
        public Via Parse(string value)
        {
            ReadOnlySpan<char> sSpan = value.AsSpan();
            ReadOnlySpan<char> vSpan = Constants.SipVersion.AsSpan();

            if (!sSpan.StartsWith(vSpan))
            {
                throw new SipException("Invalid sip version");
            }

            int elementsStartAt = sSpan.IndexOf(';');

            int connIpDelimIndex = sSpan.IndexOf(' ');
            string connectionProto = sSpan.Slice(vSpan.Length + 1, connIpDelimIndex - vSpan.Length).ToString();
            string[] ipAddr = sSpan.Slice(connIpDelimIndex + 1, elementsStartAt - connIpDelimIndex - 1)
                .ToString()
                .Split(':');

            int port = 0;
            if (ipAddr.Length > 1)
                int.TryParse(ipAddr[1], out port);

            Via via = new Via(
                IPAddress.Parse(ipAddr[0]),
                (TransportProtocol)Enum.Parse(typeof(TransportProtocol), connectionProto),
                port);

            string[] elements = sSpan.Slice(elementsStartAt + 1, sSpan.Length - elementsStartAt - 1)
                .ToString()
                .Split(';');

            foreach (string element in elements)
            {
                string[] nameValue = element.Split('=');
                if (nameValue.Length > 1)
                {
                    via.AddElement(nameValue[0], nameValue[1]);
                }
                else
                {
                    via.AddElement(nameValue[0]);
                }
            }

            return via;
        }
    }
}