using Devopsr.Lib.Models;
using Devopsr.Lib.Services.Project.Models;
using FluentResults;

namespace Devopsr.Lib.Services.Project.Interfaces;

public interface IProjectService
{
    /// <summary>
    /// Creates a new project file at the specified path.
    /// </summary>
    /// <param name="request">The request model containing the file path.</param>
    /// <returns>A result containing the response model or errors.</returns>
    Task<Result<CreateNewProjectResponse>> CreateNewProject(CreateNewProjectRequest request);

    /// <summary>
    /// Loads a project file into memory and sets the Current property.
    /// </summary>
    /// <param name="request">The request model containing the file path.</param>
    /// <returns>A result indicating success or error.</returns>
    Task<Result<OpenProjectResponse>> Open(OpenProjectRequest request);

    /// <summary>
    /// Saves the current in-memory model to the file and clears the Current property.
    /// </summary>
    /// <returns>A result indicating success or error.</returns>
    Task<Result<CloseProjectResponse>> Close();

    /// <summary>
    /// Gets the current in-memory project model.
    /// </summary>
    ProjectServiceModel? Current { get; }
}