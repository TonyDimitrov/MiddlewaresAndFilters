namespace FiltersExercise.Filters
{
    using System;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers["x-action-filter"] = "on action executing";
            Console.WriteLine("on action executing");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers["x-action-filter"] = "on action executed";
            Console.WriteLine("on action executed");
        }
    }
}
