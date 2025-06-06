using System.Text.Json;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Models;

namespace Devopsr.Lib.Repositories.Json.Project;

public class JsonProjectFileRepository : IProjectRepository
{
    public async Task<ProjectServiceModel?> LoadAsync(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        await using var stream = File.OpenRead(filePath);
        var jsonModel = await JsonSerializer.DeserializeAsync<ProjectJsonModel>(stream);
        if (jsonModel is null)
            return null;
        var project = ProjectModelMapper.ToInMemoryModel(jsonModel);
        return project;
    }

    public async Task SaveAsync(string filePath, ProjectServiceModel project)
    {
        await using var stream = File.Create(filePath);
        var jsonModel = ProjectModelMapper.ToJsonModel(project);
        await JsonSerializer.SerializeAsync(stream, jsonModel, new JsonSerializerOptions { WriteIndented = true });
    }
}
