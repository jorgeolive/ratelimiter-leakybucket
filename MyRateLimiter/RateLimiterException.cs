using System.Runtime.Serialization;

namespace MyRateLimiter
{
    [Serializable]
    internal class RateLimiterException : Exception
    {
        public RateLimiterException()
        {
        }

        public RateLimiterException(string? message) : base(message)
        {
        }

        public RateLimiterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RateLimiterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}