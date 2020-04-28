namespace MiddlewareExercise.Middlewares
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class RedirectToGoogleMiddleware
    {
        private readonly RequestDelegate next;

        public RedirectToGoogleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                context.Response.StatusCode = 307;
                context.Response.Headers["Location"] = "https://google.com";
            }
            else
            {
                await this.next(context);

                await context.Response.WriteAsync("Inside custom middware (on way the back)! " + Environment.NewLine);
            }
        }
    }
}
