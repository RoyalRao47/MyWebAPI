namespace MyWebAPI.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Dictionary<string, DateTime> _requestTimes = new();

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var now = DateTime.UtcNow;

            lock (_requestTimes)
            {
                var path = context.Request.Path.ToString();
                if (path.Contains("api") &&  _requestTimes.ContainsKey(ip))
                {
                    var lastRequestTime = _requestTimes[ip];
                    if ((now - lastRequestTime).TotalSeconds < 10) // 60-second rate limit
                    {
                        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        return;
                    }
                }

                _requestTimes[ip] = now;
            }

            await _next(context);
        }
    }
}
