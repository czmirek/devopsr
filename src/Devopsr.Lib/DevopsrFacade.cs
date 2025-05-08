using Devopsr . Lib . Services . Project;
using Devopsr . Lib . Services . Project . Interfaces;
using Microsoft . Extensions . DependencyInjection;

namespace Devopsr . Lib;

public class DevopsrFacade ( IProjectService projectService ) : IDevopsrFacade
    {
    public IProjectService ProjectService { get; } = projectService;

    public static IServiceProvider BuildServiceProvider ( )
        {
        ServiceCollection services = new();
        _ = services . AddSingleton<IProjectService , ProjectService> ( );
        _ = services . AddSingleton<IDevopsrFacade , DevopsrFacade> ( sp =>
            new DevopsrFacade ( sp . GetRequiredService<IProjectService> ( ) )
        );
        return services . BuildServiceProvider ( );
        }
    }