namespace FiltersExercise.Filters
{
    using System;
    using System.Threading.Tasks;

    using FiltersExercise.Data;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomResultFilterAttribute : Attribute, IAsyncResultFilter
    {
        // just for test to create filter with service injection
        private readonly ApplicationDbContext dbContext;

        public CustomResultFilterAttribute(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            context.HttpContext.Response.Headers["x-result"] = "on result executing";
            Console.WriteLine("on result executing");

            await next();

            context.HttpContext.Response.Headers["x-result"] = "on result executed";
            Console.WriteLine("on result executed");
        }
    }
}
