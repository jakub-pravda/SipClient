using System;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer.Extensions
{
    /// <summary>
    ///     String builder extensions.
    /// </summary>
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendSipLine(this StringBuilder sb, string headerName, string value)
        {
            sb.Append(headerName);
            sb.Append(':');
            sb.Append(value);
            sb.AppendLine();

            return sb;
        }
    }
}
