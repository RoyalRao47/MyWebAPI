namespace MyWebAPI.Middleware
{
    public class SwaggerAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (IsSwaggerUI(context.Request.Path))
            {
                if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
                {
                    context.Response.Redirect($"https://localhost:7187/Login");

                    return;
                }
            }
			if (context.Request.Path.StartsWithSegments("/api/webhook"))
			{
				if (!context.Request.Headers.TryGetValue("X-Webhook-Secret", out var secret) || secret != "your-secret-token")
				{
					context.Response.StatusCode = StatusCodes.Status403Forbidden;
					return;
				}
			}
			await _next(context);
        }

        public bool IsSwaggerUI(PathString pathString)
        {
            return pathString.StartsWithSegments("/swagger");
        }
    }
    public static class ExtentionMethod
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerAuthorizeMiddleware>();
        }
    }
}
