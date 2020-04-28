namespace FiltersExercise.Controllers
{
    using System.Diagnostics;

    using FiltersExercise.Filters;
    using FiltersExercise.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        [CustomActionFilter]
        [ServiceFilterAttribute(typeof(CustomResultFilterAttribute))]
        public IActionResult Index()
        {
            return this.View();
        }

        [CustomAutorizationFilter]
        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
