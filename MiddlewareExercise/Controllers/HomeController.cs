namespace MiddlewareExercise.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Logging;
    using MiddlewareExercise.Models;
    using MiddlewareExercise.Services;
    using MiddlewareExercise.View_Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IWebHostEnvironment env;
        private readonly IFileProvider fileProvider;
        private readonly IInstanceService instances;

        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment env,
            IFileProvider fileProvider,
            IInstanceService instances)
        {
            this.logger = logger;
            this.env = env;
            this.fileProvider = fileProvider;
            this.instances = instances;
        }

        public IActionResult Index()
        {
            var members = this.GetAllEnvironmentMembers(this.env);

            this.logger.LogInformation(this.instances.Instances.ToString());

            return this.View(new MomeVM { Members = members });
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

        private ICollection<string> GetAllEnvironmentMembers(IWebHostEnvironment env)
        {
            var values = new List<string>();

            var properties = env.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var result = property.GetValue(this.env);

                if (result.GetType() == typeof(IEnumerable))
                {
                    values.Add(string.Join(Environment.NewLine + " -", property.Name + " - " + (IEnumerable)result));
                }
                else
                {
                    values.Add(property.Name + " - " + result.ToString());
                }
            }

            return values;
        }
    }
}
