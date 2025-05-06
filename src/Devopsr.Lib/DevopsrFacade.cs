using Devopsr.Lib.Interfaces.Project;
using Devopsr.Lib.Services;

namespace Devopsr.Lib;

public class DevopsrFacade : IDevopsrFacade
{
    public IProjectService ProjectService { get; }

    public DevopsrFacade()
    {
        ProjectService = new ProjectService();
    }
}
