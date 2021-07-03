using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Configs;
using WebApplication.Tools;
using WebApplication.middleware;
using WebApplication.models;

namespace WebApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { });
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.Configure<ClientsBlacklistConfig>(
                ConfigurationManager.AppSetting.GetSection(nameof(ClientsBlacklistConfig)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestHandlerMiddleware>();
            app.UseMiddleware<ResponseHandlerMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => { });
            if (env.IsDevelopment())
            {
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseEndpoints(endpoints => { });
        }
    }
}