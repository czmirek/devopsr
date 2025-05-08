using Devopsr.Lib.Models;
using Devopsr.Lib.Services.Project.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class CreateNewProjectWithValidPathTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task CreateNewProject_WithValidPath_ReturnsSuccess()
    {
        // Arrange
        string filePath = "test.devopsr";
        CreateNewProjectRequest request = new() { FilePath = filePath };

        // Use a file path that doesn't actually exist for testing
        _mockRepository.Setup(repo => repo.SaveAsync(It.IsAny<string>(), It.IsAny<ProjectServiceModel>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateNewProject(request);

        // Assert
        Assert.True(result.IsSuccess);
        _mockRepository.Verify(repo => repo.SaveAsync(
            filePath,
            It.IsAny<ProjectServiceModel>()),
            Times.Once);
    }
}