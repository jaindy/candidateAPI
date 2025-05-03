namespace CandidateAPI.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "XApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            if (path.StartsWithSegments("/swagger") ||
                path.StartsWithSegments("/openapi") ||
                path.StartsWithSegments("/scalar") ||
                path.StartsWithSegments("/index.html") ||
                path.StartsWithSegments("/favicon.ico"))
            {
                await _next(context);
                return;
            }
            if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key was not provided ");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var configuredApiKey = appSettings.GetValue<string>("XApiKey");
            if (string.IsNullOrWhiteSpace(configuredApiKey) || !configuredApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; ;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);


        }
    }
}
