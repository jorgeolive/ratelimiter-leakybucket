namespace Api
{
    public class LeakyBucketRateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        public LeakyBucketRateLimiterMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<LeakyBucketRateLimiterMiddleware> logger, LeakyBucket bucket)
        {
            var semaphore = new SemaphoreSlim(0, 1);

            if (bucket.TryEnqueue(semaphore))
            {
                await semaphore.WaitAsync();
                await _next(context);
            }
            else
            {
                logger.LogInformation("Rejected request");
                context.Response.StatusCode = 503;
            }
        }
    }
}
