using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSE.Common.Configurations;
using MSE.SendMail.Application.Communicate;
using MSE.SendMail.Application.Communicate.Users;
using MSE.SendMail.Application.Configurations;
using MSE.SendMail.Application.Services;
using MSE.SendMail.Application.Services.Imp;
using System.Net.Http;

namespace MSE.SendMail.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // RabbitMq
            var rabbitMqConfigs = new RabbitMqConfigs();
            configuration.GetSection(nameof(RabbitMqConfigs)).Bind(rabbitMqConfigs);
            services.AddSingleton(rabbitMqConfigs);

            // MassTransit
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserAccountConsumerService>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri(rabbitMqConfigs.Host), h =>
                    {
                        h.Username(rabbitMqConfigs.Username);
                        h.Password(rabbitMqConfigs.Password);
                    });
                    cfg.ReceiveEndpoint("UserAccount", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<UserAccountConsumerService>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();

            // Email configs
            var emailConfigs = new EmailConfigs();
            configuration.GetSection(nameof(EmailConfigs)).Bind(emailConfigs);
            services.AddSingleton(emailConfigs);

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();

            // HttpClient
            services.AddHttpClient("UserServiceClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44395/");
            });


            return services;
        }
    }
}
