using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSE.User.Domain;
using MSE.User.Infrastructure.Data;
using System.Reflection;

namespace MSE.User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            IServiceCollection serviceCollection = services.AddDbContext<UserDbContext>(item => item.UseSqlServer(configuration.GetConnectionString("Db")));

            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                .Where(type => type.IsClass
                    && !type.IsAbstract
                    && type.GetInterfaces()
                        .Any(i => i.IsGenericType
                                && i.GetGenericTypeDefinition() == typeof(IRepository<>)));
            foreach (var type in types)
            {
                foreach (var iface in type.GetInterfaces())
                {
                    services.AddScoped(iface, type);
                }
            }

            // Use Scrutor library
            //services.Scan(s => s
            //    .FromApplicationDependencies()
            //    .AddClasses(c => c
            //        .Where(t => t.IsClass
            //        && !t.IsAbstract
            //        && t.GetInterfaces().Any(i =>
            //            i.IsGenericType
            //            && i.GetGenericTypeDefinition() == typeof(IRepository<>))))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            //services.AddSingleton<SqlConnectionFactory>();
            //services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();

            return services;
        }
    }
}
