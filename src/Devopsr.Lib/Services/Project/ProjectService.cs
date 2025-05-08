using System.Text.Json;
using Devopsr.Lib.Services.Project.Interfaces;
using Devopsr.Lib.Services.Project.Models;
using FluentResults;

namespace Devopsr.Lib.Services.Project;

public class ProjectService : IProjectService {
    public async Task<Result<CreateNewProjectResponse>> CreateNewProject(CreateNewProjectRequest request) {
        if (!request.FilePath.EndsWith(".devopsr", StringComparison.OrdinalIgnoreCase)) {
            return Result.Fail(ErrorCodes.InvalidProjectFileExtension);
        }

        if (File.Exists(request.FilePath)) {
            return Result.Fail(ErrorCodes.ProjectFileAlreadyExists);
        }

        var project = new { created = DateTime.UtcNow };
        string json = JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(request.FilePath, json);
        return Result.Ok(new CreateNewProjectResponse {
            Success = true,
            Message = $"Created new project file at '{request.FilePath}'."
        });
    }
}