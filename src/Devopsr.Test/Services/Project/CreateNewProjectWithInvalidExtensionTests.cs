using Devopsr.Lib.Services;
using Devopsr.Lib.Services.Project.Models;
using Devopsr.Lib.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class CreateNewProjectWithInvalidExtensionTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task CreateNewProject_WithInvalidExtension_ReturnsFailure()
    {
        // Arrange
        string filePath = "test.txt";
        CreateNewProjectRequest request = new() { FilePath = filePath };

        // Act
        var result = await _service.CreateNewProject(request);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e =>
        {
            return e.Message == ErrorCodes.InvalidProjectFileExtension;
        });
        _mockRepository.Verify(repo => repo.SaveAsync(It.IsAny<string>(), It.IsAny<ProjectServiceModel>()), Times.Never);
    }
}