using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Devopsr.Lib.Interfaces;

namespace Devopsr.Lib.Services.Project;

public class ProjectService : IProjectService
{
    public async Task<CreateNewProjectResponse> CreateNewProject(CreateNewProjectRequest request)
    {
        if (!request.FilePath.EndsWith(".devopsr", StringComparison.OrdinalIgnoreCase))
        {
            return new CreateNewProjectResponse(false, "Project file must have a .devopsr extension.");
        }
        if (File.Exists(request.FilePath))
        {
            return new CreateNewProjectResponse(false, $"File '{request.FilePath}' already exists.");
        }
        var project = new { created = DateTime.UtcNow };
        var json = JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(request.FilePath, json);
        return new CreateNewProjectResponse(true, $"Created new project file at '{request.FilePath}'.");
    }
}
