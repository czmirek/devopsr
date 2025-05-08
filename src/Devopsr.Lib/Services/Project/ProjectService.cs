using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Project.Interfaces;
using Devopsr.Lib.Services.Project.Models;
using FluentResults;

namespace Devopsr.Lib.Services.Project;

public class ProjectService(IProjectRepository projectRepository, TimeProvider timeProvider) : IProjectService
{
    private string? _currentFilePath;
    public ProjectServiceModel? Current { get; private set; }

    public async Task<Result<CreateNewProjectResponse>> CreateNewProject(CreateNewProjectRequest request)
    {
        if (!request.FilePath.EndsWith(".devopsr", StringComparison.OrdinalIgnoreCase))
        {
            return Result.Fail(ErrorCodes.InvalidProjectFileExtension);
        }

        if (File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileAlreadyExists);
        }

        var now = timeProvider.GetLocalNow();
        ProjectServiceModel project = new()
        {
            Created = now,
            LastUpdate = now
            // ...other default properties...
        };
        await projectRepository.SaveAsync(request.FilePath, project);
        return Result.Ok(new CreateNewProjectResponse());
    }

    public async Task<Result> Open(OpenProjectRequest request)
    {
        if (!File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        var projectModel = await projectRepository.LoadAsync(request.FilePath);
        
        if(projectModel is null)
        {
            return Result.Fail(ErrorCodes.ProjectFileDeserializationFailed);
        }

        Current = projectModel;
        _currentFilePath = request.FilePath;
        return Result.Ok();
    }

    public async Task<Result> Close()
    {
        if (Current == null || string.IsNullOrEmpty(_currentFilePath))
        {
            return Result.Fail(ErrorCodes.NoProjectLoaded);
        }

        // Update LastUpdate before saving
        var now = timeProvider.GetLocalNow();
        var updatedProject = Current with { LastUpdate = now };
        await projectRepository.SaveAsync(_currentFilePath, updatedProject);

        Current = null;
        _currentFilePath = null;
        return Result.Ok();
    }
}