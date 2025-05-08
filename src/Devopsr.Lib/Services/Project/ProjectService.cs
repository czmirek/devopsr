using System.Text.Json;
using Devopsr.Lib.Services.Project.Interfaces;
using Devopsr.Lib.Services.Project.Models;
using FluentResults;

namespace Devopsr.Lib.Services.Project;

public class ProjectService : IProjectService
{
    private string? _currentFilePath;

    public object? Current { get; private set; }

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

        var project = new { created = DateTime.UtcNow };
        string json = JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(request.FilePath, json);
        return Result.Ok(new CreateNewProjectResponse());
    }

    public async Task<Result> Open(OpenProjectRequest request)
    {
        if(!File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        try
        {
            string json = await File.ReadAllTextAsync(request.FilePath);
            Current = JsonSerializer.Deserialize<object>(json);
            _currentFilePath = request.FilePath;
            return Result.Ok();
        }
        catch(Exception ex)
        {
            return Result.Fail($"FailedToLoadProjectFile: {ex.Message}");
        }
    }

    public async Task<Result> Close()
    {
        if(Current == null || string.IsNullOrEmpty(_currentFilePath))
        {
            return Result.Fail(ErrorCodes.NoProjectLoaded);
        }

        try
        {
            string updatedJson = JsonSerializer.Serialize(Current, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_currentFilePath, updatedJson);
            Current = null;
            _currentFilePath = null;
            return Result.Ok();
        }
        catch(Exception ex)
        {
            return Result.Fail($"FailedToSaveProjectFile: {ex.Message}");
        }
    }
}