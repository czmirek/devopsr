using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Repositories.Json.Project;
using Devopsr.Lib.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Devopsr.Lib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDevopsrLib(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IProjectRepository, JsonProjectFileRepository>();
        services.AddSingleton<CurrentProjectHolderService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        return services;
    }
}
