using System.Text.Json;
using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Project;
using FluentResults;

namespace Devopsr.Lib.Repositories.Json;

public class JsonProjectFileRepository : IProjectRepository
{
    public async Task<Result<ProjectInMemoryModel?>> LoadAsync(string filePath)
    {
        try
        {
            if(!File.Exists(filePath))
                return Result.Fail<ProjectInMemoryModel?>(ErrorCodes.ProjectFileDoesNotExist);

            await using var stream = File.OpenRead(filePath);
            var project = await JsonSerializer.DeserializeAsync<ProjectInMemoryModel>(stream);
            return Result.Ok<ProjectInMemoryModel?>(project);
        }
        catch(Exception ex)
        {
            return Result.Fail<ProjectInMemoryModel?>(ex.Message);
        }
    }

    public async Task<Result> SaveAsync(string filePath, ProjectInMemoryModel project)
    {
        try
        {
            await using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, project, new JsonSerializerOptions { WriteIndented = true });
            return Result.Ok();
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
