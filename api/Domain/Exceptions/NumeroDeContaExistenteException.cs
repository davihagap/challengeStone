using System;
using System.Runtime.Serialization;

namespace api.Domain.Exceptions
{
    [Serializable]
    public class NumeroDeContaExistenteException : Exception
    {
        public NumeroDeContaExistenteException()
        {
        }

        public NumeroDeContaExistenteException(string message) : base(message)
        {
        }

        public NumeroDeContaExistenteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NumeroDeContaExistenteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}