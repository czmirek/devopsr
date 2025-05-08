using Devopsr.Lib.Services.Project;
using Devopsr.Lib.Services.Project.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devopsr.Lib;

public class DevopsrFacade : IDevopsrFacade
{
    public IProjectService ProjectService { get; }

    public DevopsrFacade(IProjectService projectService)
    {
        ProjectService = projectService;
    }

    public static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IProjectService, ProjectService>();
        services.AddSingleton<IDevopsrFacade, DevopsrFacade>(sp =>
            new DevopsrFacade(sp.GetRequiredService<IProjectService>())
        );
        return services.BuildServiceProvider();
    }
}