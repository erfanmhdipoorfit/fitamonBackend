// Fitamon.Persistence.EntityFramework/ServiceRegistration.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Fitamon.Domain.Bot.Contracts;
using Fitamon.Persistence.EntityFramework.Bot;
using Fitamon.Persistence.EntityFramework.Bot.Services;

namespace Fitamon.Persistence.EntityFramework
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BotDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IBotServices, BotServices>();

            return services;
        }
    }
}