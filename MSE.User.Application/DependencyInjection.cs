using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSE.Common.Configurations;
using MSE.User.Application.Services;

namespace MSE.User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // MediatR
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            //services.AddValidatorsFromAssembly(assembly);

            // AutoMapper
            services.AddAutoMapper(assembly);

            // RabbitMq
            var rabbitMqConfigs = new RabbitMqConfigs();
            configuration.GetSection(nameof(RabbitMqConfigs)).Bind(rabbitMqConfigs);
            services.AddSingleton(rabbitMqConfigs);

            // MassTransit
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri(rabbitMqConfigs.Host), h =>
                    {
                        h.Username(rabbitMqConfigs.Username);
                        h.Password(rabbitMqConfigs.Password);
                    });
                }));
            });
            services.AddMassTransitHostedService();

            // Add DI for services
            services.AddScoped<IProducerService, ProducerService>();

            return services;
        }
    }
}
