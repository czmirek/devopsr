using Devopsr.Lib.Services.Models;

namespace Devopsr.Lib.Services;

internal interface ILoadedProject
{
    ProjectServiceModel? CurrentProject { get; }
    string? CurrentFilePath { get; }

    void SetCurrentProject(ProjectServiceModel project, string filePath);
    void ClearCurrentProject();
}
