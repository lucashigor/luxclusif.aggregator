using luxclusif.aggregator.application.Models;
using luxclusif.aggregator.application.UseCases.Order.CreateUserTotalExpendedInOrdersAggregated;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace luxclusif.aggregator.kernel.Extensions;
public static class UseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var assembly1 = Assembly.GetExecutingAssembly();

        Assembly configurationAppAssembly = typeof(CreateNewUser).Assembly;

        services.AddMediatR(configurationAppAssembly,assembly1);

        services.AddScoped<Notifier>();

        return services;
    }
}
