using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Repositories.Json.Project;
using Devopsr.Lib.Services.Project;
using Devopsr.Lib.Services.Project.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devopsr.Lib;

public class DevopsrFacade(IProjectService projectService) : IDevopsrFacade
{
    public IProjectService ProjectService { get; } = projectService;

    public static IServiceProvider BuildServiceProvider()
    {
        ServiceCollection services = new();
        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IProjectRepository, JsonProjectFileRepository>();
        services.AddSingleton<IProjectService, ProjectService>();
        services.AddSingleton<IDevopsrFacade, DevopsrFacade>(sp =>
        {
            return new DevopsrFacade(sp.GetRequiredService<IProjectService>());
        });
        return services.BuildServiceProvider();
    }
}