using System.Net.Http.Headers;
using System.Text;

namespace SimpleBlog.API.Configuration
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string? authHeader = context.Request.Headers["Authorization"];

                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var headerValue = AuthenticationHeaderValue.Parse(authHeader);
                    var inBytes = Convert.FromBase64String(headerValue.Parameter ?? "");
                    var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                    var username = credentials[0];
                    var password = credentials[1];

                    var configUser = _configuration["SwaggerAuth:Username"];
                    var configPass = _configuration["SwaggerAuth:Password"];

                    if (username == configUser && password == configPass)
                    {
                        await _next(context);
                        return;
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
