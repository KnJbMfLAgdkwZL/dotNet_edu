using System;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.models;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/order/{id}", async context =>
                {
                    var sid = context.Request.RouteValues["id"];

                    Int64.TryParse(sid?.ToString(), out long id);

                    var order = new Order();
                    var data = order.SelectById(id);
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    var json = JsonSerializer.Serialize(data, options);

                    Console.WriteLine(json);
                    await context.Response.WriteAsync(json);
                });

                endpoints.MapPost("/order/create", async context =>
                {
                    var form = context.Request.Form;
                    if (form.ContainsKey("name") && form.ContainsKey("description"))
                    {
                        var name = form["name"];
                        var description = form["description"];

                        var order = new Order();
                        bool res = order.Insert(name, description);
                        if (res)
                        {
                            await context.Response.WriteAsync("OK");
                            return;
                        }
                    }

                    await context.Response.WriteAsync("WRONG DATA PARAM");
                });
            });
        }
    }
}