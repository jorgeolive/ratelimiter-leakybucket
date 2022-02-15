namespace MyRateLimiter
{
    public class LeakyBucketRateLimiterMiddleware
    {
        private readonly RequestDelegate _next;

        public LeakyBucketRateLimiterMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, LeakyBucket bucket)
        {
            var semaphore = new SemaphoreSlim(0, 1);

            if (bucket.TryEnqueue(semaphore))
            {
                await semaphore.WaitAsync();
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 503;
            }
        }
    }
}
