using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TempConverter
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("Cache-Control", $"public, max-age={86400 * 365}");
                    return Task.FromResult(0);
                });
                await next.Invoke();

            });
            app.UseEndpoints(endpoints =>
            {


                endpoints.MapGet("/temp/{temp}/f", async (context) =>
                {
                    var routeData = context.GetRouteData();
                    var temp = routeData?.Values["temp"]?.ToString();
                    if (temp == null)
                    {
                        context.Response.StatusCode = 404;

                    }
                    else
                    {
                        var t = double.Parse(temp);
                        var celsius = (t - 32) * 5 / 9;
                        var response = new { tempInF = t, tempInC = celsius };
                        var responseJson = JsonSerializer.Serialize(response);
                        context.Response.Headers.Add("content-type", "application/json");
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(responseJson);
                    }
                });

                endpoints.MapGet("/temp/{temp}/c", async (context) =>
                {
                    var routeData = context.GetRouteData();
                    var temp = routeData?.Values["temp"]?.ToString();
                    if (temp == null)
                    {
                        context.Response.StatusCode = 404;

                    }
                    else
                    {
                        var t = double.Parse(temp);
                        var fahrenheit = (t * 9) / 5 + 32;
                        var response = new { tempInF = fahrenheit, tempInC = t };
                        var responseJson = JsonSerializer.Serialize(response);
                        context.Response.Headers.Add("content-type", "application/json");
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(responseJson);
                    }
                });
            });
        }
    }
}
