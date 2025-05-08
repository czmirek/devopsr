using Devopsr.Lib.Services.Project.Models;
using FluentResults;

namespace Devopsr.Lib.Services.Project.Interfaces;

public interface IProjectService {
    /// <summary>
    /// Creates a new project file at the specified path.
    /// </summary>
    /// <param name="request">The request model containing the file path.</param>
    /// <returns>A result containing the response model or errors.</returns>
    Task<Result<CreateNewProjectResponse>> CreateNewProject(CreateNewProjectRequest request);
}