using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using ReportService.Entities;
using ReportService.Repositories;
using ReportService.Repositories.Interfaces;
using ReportService.Settings;
using System;

namespace ReportService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddHttpClient();

            services.AddScoped<IDatabaseSettings, DatabaseSettings>();
            services.AddSingleton<RabbitMQClientService>();
            services.AddSingleton<IRabbitMQPublisherService, RabbitMQPublisherService>();
            services.AddTransient<HttpClientService>();

            services.AddHostedService<ExcelReportBackgroundService>();

            services.Configure<MicroServices>(Configuration.GetSection("Microservices"));
            services.Configure<DatabaseSettings>(Configuration.GetSection("ReportingDatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);



            services.AddSingleton(sp =>
    new ConnectionFactory()
    {
        Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")),
        DispatchConsumersAsync = true,
    }
);


            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReportService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}