// Fitamon.Application/ServiceRegistration.cs
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace Fitamon.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // اگر AutoMapper در این لایه است:
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}