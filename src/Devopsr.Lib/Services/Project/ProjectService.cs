using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Project.Interfaces;
using Devopsr.Lib.Services.Project.Models;
using FluentResults;

namespace Devopsr.Lib.Services.Project;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private string? _currentFilePath;
    public ProjectInMemoryModel? Current { get; private set; }

    public async Task<Result<CreateNewProjectResponse>> CreateNewProject(CreateNewProjectRequest request)
    {
        if(!request.FilePath.EndsWith(".devopsr", StringComparison.OrdinalIgnoreCase))
        {
            return Result.Fail(ErrorCodes.InvalidProjectFileExtension);
        }

        if(File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileAlreadyExists);
        }

        ProjectInMemoryModel project = new()
        {
            Created = DateTime.UtcNow
            // ...other default properties...
        };
        var saveResult = await projectRepository.SaveAsync(request.FilePath, project);
        return saveResult.IsFailed ? (Result<CreateNewProjectResponse>)Result.Fail(saveResult.Errors.First().Message) : Result.Ok(new CreateNewProjectResponse());
    }

    public async Task<Result> Open(OpenProjectRequest request)
    {
        if(!File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        var loadResult = await projectRepository.LoadAsync(request.FilePath);
        if(loadResult.IsFailed || loadResult.Value == null)
        {
            return Result.Fail(loadResult.Errors.First().Message);
        }

        Current = loadResult.Value;
        _currentFilePath = request.FilePath;
        return Result.Ok();
    }

    public async Task<Result> Close()
    {
        if(Current == null || string.IsNullOrEmpty(_currentFilePath))
        {
            return Result.Fail(ErrorCodes.NoProjectLoaded);
        }

        var saveResult = await projectRepository.SaveAsync(_currentFilePath, Current);
        if(saveResult.IsFailed)
        {
            return Result.Fail(saveResult.Errors.First().Message);
        }

        Current = null;
        _currentFilePath = null;
        return Result.Ok();
    }
}