namespace FiltersExercise.Filters
{
    using System;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomResorseFilter : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Headers["x-resource"] = "on-resource-executing";
            Console.WriteLine("on-resource-executing");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Cannot write to response after the request is sent
            // context.HttpContext.Response.Headers["x-resource"] = "on-resource-executing";
            Console.WriteLine("on-resource-executed");
        }
    }
}
