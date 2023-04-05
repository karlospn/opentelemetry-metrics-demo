namespace BookStore.WebApi.Middleware
{
    public class SimulatedLatencyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _minDelayInMs;
        private readonly int _maxDelayInMs;
        private readonly ThreadLocal<Random> _random;

        public SimulatedLatencyMiddleware(
            RequestDelegate next,
            TimeSpan min,
            TimeSpan max
        )
        {
            _next = next;
            _minDelayInMs = (int)min.TotalMilliseconds;
            _maxDelayInMs = (int)max.TotalMilliseconds;
            _random = new ThreadLocal<Random>(() => new Random());
        }

        public async Task Invoke(HttpContext context)
        {
            if (_random.Value != null)
            {
                var delayInMs = _random.Value.Next(
                    _minDelayInMs,
                    _maxDelayInMs
                );

                await Task.Delay(delayInMs);
            }

            await _next(context);
        }
    }
}
