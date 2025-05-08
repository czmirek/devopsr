// filepath: c:\Users\lesar\source\repos\devopsr\src\Devopsr.Lib\ServiceCollectionExtensions.cs
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Repositories.Json.Project;
using Devopsr.Lib.Services.Project;
using Devopsr.Lib.Services.Project.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devopsr.Lib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDevopsrLib(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IProjectRepository, JsonProjectFileRepository>();
        services.AddSingleton<IProjectService, ProjectService>();
        return services;
    }
}
