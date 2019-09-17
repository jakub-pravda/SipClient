namespace SipSerializer.Parsers
{
    /// <summary>
    ///     Header parser
    /// </summary>
    public interface IHeaderParser<T>
    {
        /// <summary>
        ///     Get parser name
        /// </summary>
        string GetParserName();

        /// <summary>
        ///     Parse string to the T value
        /// </summary>
        /// <param name="headerValue">Header value in ascii form</param>
        /// <returns>Parsed T value</returns>
        T Parse(string headerValue);
    }
}