using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Json.Project;

namespace Devopsr.Test.Repositories.Json.Project;

public sealed class JsonProjectFileRepositoryTests
{
    private static ProjectServiceModel CreateSampleProject()
    {
        // Use internal constructor with sample DateTimeOffset values
        return new ProjectServiceModel(DateTimeOffset.Now, DateTimeOffset.Now);
    }

    [Fact]
    public async Task SaveAsync_And_LoadAsync_RoundTrip_Works()
    {
        JsonProjectFileRepository repository = new();
        string tempFile = Path.GetTempFileName();
        try
        {
            var project = CreateSampleProject();
            await repository.SaveAsync(tempFile, project);
            var loaded = await repository.LoadAsync(tempFile);
            Assert.NotNull(loaded);
            // Add more property assertions as needed
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task LoadAsync_ReturnsNull_WhenFileDoesNotExist()
    {
        JsonProjectFileRepository repository = new();
        var result = await repository.LoadAsync("nonexistentfile.json");
        Assert.Null(result);
    }
}
