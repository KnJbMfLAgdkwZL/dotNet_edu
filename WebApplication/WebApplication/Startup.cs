using System;
using System.Collections.Generic;
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
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseEndpoints(endpoints =>
            {
                /*
                endpoints.MapGet("/order/{id}", async context =>
                {
                    object sid = context.Request.RouteValues["id"];
                    Int64.TryParse(sid?.ToString(), out long id);

                    OrderRepository orderRepository = new OrderRepository();
                    List<Order> data = orderRepository.SelectById(id);
                    string json = orderRepository.ToJson(data);

                    await context.Response.WriteAsync(json);
                });
                */
                /*
                endpoints.MapPost("/order/create", async context =>
                {
                    IFormCollection form = context.Request.Form;
                    if (form.ContainsKey("name") && form.ContainsKey("description"))
                    {
                        string name = form["name"];
                        string description = form["description"];
                        OrderRepository orderRepository = new OrderRepository();
                        Order order = new Order(name, description);
                        bool res = orderRepository.Insert(order);
                        if (res)
                        {
                            await context.Response.WriteAsync("OK");
                            return;
                        }
                    }
                    await context.Response.WriteAsync("WRONG DATA PARAM");
                });
                */
            });
        }
    }
}