using Devopsr.Lib.Models;
using FluentResults;

namespace Devopsr.Lib.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<Result<ProjectInMemoryModel?>> LoadAsync(string filePath);
    Task<Result> SaveAsync(string filePath, ProjectInMemoryModel project);
}
