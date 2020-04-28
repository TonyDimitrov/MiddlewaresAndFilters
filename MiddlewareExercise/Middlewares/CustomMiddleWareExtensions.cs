namespace MiddlewareExercise.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustom(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedirectToGoogleMiddleware>();
        }
    }
}
