namespace FiltersExercise.Filters
{
    using System;
    using System.Security.Authentication;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomAutorizationFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException();
            }

            await Task.FromResult(typeof(AuthenticationException).Name);
        }
    }
}
