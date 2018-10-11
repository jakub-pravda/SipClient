using System;

namespace Javor.SdpSerializer.Exceptions
{
    public class SdpFormatException : Exception
    {
        public SdpFormatException()
        {
            
        }

        public SdpFormatException(string message)
            : base(message)
        {
            
        }

        public SdpFormatException(string message, Exception inner)
            : base (message, inner)
        {
            
        }
    }
}