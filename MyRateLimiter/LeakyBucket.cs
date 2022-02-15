using System.Collections.Concurrent;
using System.Timers;

namespace MyRateLimiter
{
    public class LeakyBucket
    {
        private System.Timers.Timer _timer;
        private BlockingCollection<SemaphoreSlim> _tokens;
        private SemaphoreSlim _timerSemaphore; 

        public LeakyBucket(int bucketSize, decimal requestToDispatchBySecond)
        {
            _timerSemaphore = new SemaphoreSlim(0, 1);
            _tokens = new (bucketSize);
            InitTimer(requestToDispatchBySecond);

            Task.Run(async () =>
            {
                foreach(var requestSemaphore in _tokens.GetConsumingEnumerable())
                {
                    await _timerSemaphore.WaitAsync();
                    requestSemaphore.Release();
                }
            });
        }

        private void InitTimer(decimal requestToDispatchBySecond)
        {
            _timer = new System.Timers.Timer(Convert.ToDouble(1000/requestToDispatchBySecond));
            _timer.AutoReset = true;
            _timer.Elapsed += OnCountdown;
            _timer.Enabled = true;
        }

        private void OnCountdown(object? sender, ElapsedEventArgs e)
        {
            try
            {
                _timerSemaphore.Release();
            } 
            catch (Exception ex)
            {

            } 
        }

        public bool TryEnqueue(SemaphoreSlim semaphore)
        {
            return _tokens.TryAdd(semaphore);
        }
    }
}
