using System;

namespace Javor.SipSerializer.Attributes
{
    /// <summary>
    ///     Header name attribute.
    /// </summary>
    public class SipHeaderAttribute : Attribute
    {
        /// <summary>
        ///     Initialize new header name attribute.
        /// </summary>
        /// <param name="longName">Header full name.</param>
        public SipHeaderAttribute(string longName)
        {
            if (string.IsNullOrEmpty(longName)) throw new ArgumentNullException("Header name cannot be null.");

            _name = longName;
        }

        /// <summary>
        ///     Initialize new header name attribute.
        /// </summary>
        /// <param name="longName">Header full name.</param>
        /// <param name="compactFormName">Header compact name.</param>
        public SipHeaderAttribute(string longName, string compactFormName)
            : this(longName)
        {
            if (string.IsNullOrEmpty(compactFormName)) throw new ArgumentNullException("Header compact form name cannot be null.");

            if (compactFormName.Length >= longName.Length)
            {
                throw new ArgumentException("Header compact name must be shorter than header long name.");
            }
            _compactFormName = compactFormName;
        }

        private string _name;
        private string _compactFormName;

        /// <summary>
        ///     Get full header name.
        /// </summary>
        /// <returns>Full header name.</returns>
        public string GetHeaderFullName()
        {
            return _name;
        }

        /// <summary>
        ///     Get compact header name.
        /// </summary>
        /// <returns>Compact header name.</returns>
        public string GetHeaderCompactFormName()
        {
            return _compactFormName;
        }
    }
}