using System.Collections.Generic;
using System.Linq;

namespace SipSerializer.Parsers
{
    /// <summary>
    ///     Parser service
    /// </summary>
    public sealed class ParserService
    {
        private static volatile ParserService _instance;
        private static readonly object _syncLock = new object();

        private ParserService()
        {
        }

        /// <summary>
        ///     Get instance of parser service.
        /// </summary>
        /// <value>Parser service instance</value>
        public static ParserService Instance
        {
            get 
            {
                if (_instance != null)
                    return _instance;

                lock(_syncLock)
                {
                    if (_instance == null)
                        _instance = new ParserService();
                }

                return _instance;
            }
        }

        List<IHeaderParser<object>> _parsers;

        /// <summary>
        ///     Parse header value. Appropriate value is returned when parser exist for provided header name.
        ///      String is returned by default.
        /// </summary>
        /// <param name="headerName">Header (parser) name</param>
        /// <param name="headerValue">Header value</param>
        /// <returns>Appropriate value according to the parser or string when parser doesn`t exist</returns>
        public object Parse(string headerName, string headerValue)
        {
            IHeaderParser<object> parser = _parsers.FirstOrDefault(p => p.GetParserName() == headerName);

            if (parser != null)
                return parser.Parse(headerValue);

            return headerValue;
        }
    }
}