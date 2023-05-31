using System;

namespace Tsutaeru.Common
{
    public sealed class RetryException : Exception
    {
        public RetryException(string message) : base(message)
        {
        }
    }

    public sealed class RebootException : Exception
    {
        public RebootException(string message) : base(message)
        {
        }
    }
}