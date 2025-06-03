using System.Net;
using System.Text.Json;

namespace FIAPTechChallenge.Presentation.Middlewares
{
    public class UnauthorizedResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { erro = "não autorizado" });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
