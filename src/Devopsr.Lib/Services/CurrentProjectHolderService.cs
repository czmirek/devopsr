using Devopsr.Lib.Models;

namespace Devopsr.Lib.Services;

public class CurrentProjectHolderService
{
    public ProjectServiceModel? CurrentProject { get; private set; }
    public string? CurrentFilePath { get; private set; }

    public void SetCurrentProject(ProjectServiceModel project, string filePath)
    {
        CurrentProject = project;
        CurrentFilePath = filePath;
    }

    public void ClearCurrentProject()
    {
        CurrentProject = null;
        CurrentFilePath = null;
    }
}
