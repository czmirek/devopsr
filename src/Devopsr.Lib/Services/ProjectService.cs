using System;
using System.IO;
using System.Text.Json;
using Devopsr.Lib.Interfaces.Project;

namespace Devopsr.Lib.Services;

public class ProjectService : IProjectService
{
    public string CreateNewProject(string filePath)
    {
        if (!filePath.EndsWith(".devopsr", StringComparison.OrdinalIgnoreCase))
        {
            return "Error: Project file must have a .devopsr extension.";
        }
        if (File.Exists(filePath))
        {
            return $"Error: File '{filePath}' already exists.";
        }
        var project = new { created = DateTime.UtcNow };
        var json = JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
        return $"Created new project file at '{filePath}'.";
    }
}
