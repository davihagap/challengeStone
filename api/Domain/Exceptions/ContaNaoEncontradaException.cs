using System;
using System.Runtime.Serialization;

namespace api.Domain.Exceptions
{
    [Serializable]
    public class ContaNaoEncontradaException : Exception
    {
        public ContaNaoEncontradaException()
        {
        }

        public ContaNaoEncontradaException(string message) : base(message)
        {
        }

        public ContaNaoEncontradaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContaNaoEncontradaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}