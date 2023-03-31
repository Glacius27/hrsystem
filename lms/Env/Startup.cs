﻿using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Prometheus;
using MassTransit;
using lms.Logic;
using shraredclasses.Commands;
using lms.MassTransit;

namespace salary.Env
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
            services.AddSingleton<LmsService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateLearningTrackConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("rabbitmq://rabbit-rabbitmq"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("createLearningCourse", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<CreateLearningTrackConsumer>(provider);
                    });
                }));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMetricServer();
            app.UseHttpMetrics();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllerRoute(name: "employee", pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
