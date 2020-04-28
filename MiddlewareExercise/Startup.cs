namespace MiddlewareExercise
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using MiddlewareExercise.Data;
    using MiddlewareExercise.Middlewears;
    using MiddlewareExercise.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IInstanceService, InstanceService>();

            IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            services.AddSingleton(physicalProvider);

            services.AddControllersWithViews();
            services.AddRazorPages()
            .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustom();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Map("/map", app =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Mat to address (branching)" + Environment.NewLine);
                });
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Before" + Environment.NewLine);

                await next();

                await context.Response.WriteAsync("After (on the way back)" + Environment.NewLine);
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Last terminating middleware " + Environment.NewLine);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
