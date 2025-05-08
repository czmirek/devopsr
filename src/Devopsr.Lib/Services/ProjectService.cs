// filepath: c:\Users\lesar\source\repos\devopsr\src\Devopsr.Lib\Services\ProjectService.cs
using Devopsr.Lib.Models;

namespace Devopsr.Lib.Services;

public class ProjectService : IProjectService
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
