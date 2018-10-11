using System;

namespace Javor.SdpSerializer.Exceptions
{
    public class InitailizationException : Exception
    {
        public InitailizationException()
        {
            
        }        
        
        public InitailizationException(string message)
            : base(message)
        {
            
        }        
        
        public InitailizationException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}