using System;
using System.Runtime.Serialization;

namespace Ays.PhysicalQuantities.Exceptions
{
    [Serializable]
    public class ConversionException : NotSupportedException
    {
        public ConversionException() : base() { }

        public ConversionException(string message) : base(message) { }

        public ConversionException(string message, Exception innerException) : base(message, innerException) { }

        public ConversionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
