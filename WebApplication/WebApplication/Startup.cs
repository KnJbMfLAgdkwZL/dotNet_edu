using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Configs;
using WebApplication.Tools;
using WebApplication.middleware;
using WebApplication.models;
using WebApplication.Repositories;
using WebApplication.Tools;

namespace WebApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { });
            services.AddScoped<IOrderRepository, OrderRepositoryV2>();
            services.ConfigureOptions<DataBaseConfig>(_configuration);
            services.ConfigureOptions<ClientsBlacklistConfig>(_configuration);
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