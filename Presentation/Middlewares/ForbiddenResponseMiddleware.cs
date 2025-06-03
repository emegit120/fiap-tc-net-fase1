using System.Net;
using System.Text.Json;

namespace FIAPTechChallenge.Presentation.Middlewares
{
    public class ForbiddenResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "requisição proibida" });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
