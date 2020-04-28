namespace FiltersExercise.Filters
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;
            context.HttpContext.Response.Headers["x-exception"] = exceptionMessage;

            await Task.FromResult(exceptionMessage);
            Console.WriteLine(exceptionMessage);
        }
    }
}
