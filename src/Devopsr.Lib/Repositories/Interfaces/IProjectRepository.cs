using Devopsr.Lib.Services.Models;

namespace Devopsr.Lib.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<ProjectServiceModel?> LoadAsync(string filePath);
    Task SaveAsync(string filePath, ProjectServiceModel project);
}
