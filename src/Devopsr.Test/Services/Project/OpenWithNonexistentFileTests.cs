using Devopsr.Lib.Services;
using Devopsr.Lib.Services.Project.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class OpenWithNonexistentFileTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task Open_WithNonexistentFile_ReturnsFailure()
    {
        // Arrange - Use a path that's very unlikely to exist
        string filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".devopsr");
        OpenProjectRequest request = new() { FilePath = filePath };

        // Act
        var result = await _service.Open(request);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e =>
        {
            return e.Message == ErrorCodes.ProjectFileDoesNotExist;
        });
        Assert.Null(_service.Current);
        _mockRepository.Verify(repo => repo.LoadAsync(It.IsAny<string>()), Times.Never);
    }
}