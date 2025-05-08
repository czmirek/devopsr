using Devopsr.Lib.Models;

namespace Devopsr.Lib.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<ProjectInMemoryModel?> LoadAsync(string filePath);
    Task SaveAsync(string filePath, ProjectInMemoryModel project);
}
