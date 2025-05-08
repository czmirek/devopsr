// filepath: c:\Users\lesar\source\repos\devopsr\src\Devopsr.Lib\Services\IProjectService.cs
using Devopsr.Lib.Models;

namespace Devopsr.Lib.Services;

public interface IProjectService
{
    ProjectServiceModel? CurrentProject { get; }
    string? CurrentFilePath { get; }

    void SetCurrentProject(ProjectServiceModel project, string filePath);
    void ClearCurrentProject();
}
