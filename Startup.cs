using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using crmApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;

namespace crmApi
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = Configuration.GetConnectionString("CrmDatabase");
            //var connStr = Configuration.GetSection("Data")
            //               .GetSection("DefaultConnection")["ConnectionString"];
            //services.AddDbContext<CrmContext>(opt => opt.UseSqlServer(connection));
            services.AddDbContext<CrmContext>(opt => opt.UseSqlServer("Server=DESKTOP-9HQUT28;Database=Crm.WebApi;User Id=ClubSked;Password=ClubSked;MultipleActiveResultSets=True;"));
            services.AddMvc();

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // https://hanson.io/bootstrapping-asp-net-core-week-4/
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<CrmContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {
            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");

            /*
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");   // I needed to add this otherwise in Angular I Would get "Response with status: 0 for URL"
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal Server Error");
                });
            });
            */

            // IMPORTANT: Make sure UseCors() is called BEFORE this
            app.UseMvc();
        }
    }
}
