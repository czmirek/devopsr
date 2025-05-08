using System.Text.Json;
using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Project;
using FluentResults;

namespace Devopsr.Lib.Repositories.Json.Project;

public class JsonProjectFileRepository : IProjectRepository
{
    public async Task<Result<ProjectInMemoryModel?>> LoadAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return Result.Fail<ProjectInMemoryModel?>(ErrorCodes.ProjectFileDoesNotExist);

            await using var stream = File.OpenRead(filePath);
            var jsonModel = await JsonSerializer.DeserializeAsync<ProjectJsonModel>(stream);
            if (jsonModel is null)
                return Result.Fail<ProjectInMemoryModel?>(ErrorCodes.ProjectFileDeserializationFailed);
            var project = ProjectModelMapper.ToInMemoryModel(jsonModel);
            return Result.Ok<ProjectInMemoryModel?>(project);
        }
        catch (Exception ex)
        {
            return Result.Fail<ProjectInMemoryModel?>(ex.Message);
        }
    }

    public async Task<Result> SaveAsync(string filePath, ProjectInMemoryModel project)
    {
        try
        {
            await using var stream = File.Create(filePath);
            var jsonModel = ProjectModelMapper.ToJsonModel(project);
            await JsonSerializer.SerializeAsync(stream, jsonModel, new JsonSerializerOptions { WriteIndented = true });
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
