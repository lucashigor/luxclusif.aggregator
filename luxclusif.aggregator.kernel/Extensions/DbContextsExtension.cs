using luxclusif.aggregator.application.Interfaces;
using luxclusif.aggregator.domain.Repository;
using luxclusif.aggregator.infrastructure;
using luxclusif.aggregator.infrastructure.Repositories;
using luxclusif.aggregator.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace luxclusif.aggregator.kernel.Extensions;
public static class DbContextsExtension
{
    public static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("PrincipalDatabase");


        if (!string.IsNullOrEmpty(conn))
        {
            services.AddDbContext<PrincipalContext>(
                options => options.UseNpgsql(conn));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        return services;
    }
}
