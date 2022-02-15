namespace MyRateLimiter
{
    public class TokenBucketRateLimiterMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenBucketRateLimiterMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, TokenBucket bucket)
        {
            try
            {
                bucket.UseToken();
                await _next(context);
            }
            catch ( RateLimiterException ex)
            {
                context.Response.StatusCode = 429;
            }
        }
    }
}
