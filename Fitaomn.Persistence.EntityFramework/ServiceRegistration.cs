using Fitamon.Domain.Bot.Contracts;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Persistence.EntityFramework.Bot.Services;
using Fitamon.Persistence.EntityFramework.Blog.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string? connectionString)
    {
        // ✅ بررسی null بودن Connection String
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string 'BotDbContext' is not configured.");

        services.AddDbContext<BotDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDbContext<PostsDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IBotServices, BotServices>();
        services.AddScoped<IPostServices, PostsServices>();
      
      
        return services;
    }
}