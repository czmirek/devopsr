using Devopsr.Lib.Models;
using Devopsr.Lib.Services.Project.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class CloseWithLoadedProjectTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task Close_WithLoadedProject_SavesAndClosesProject()
    {
        // Arrange
        string filePath = Path.GetTempFileName(); // Create a temporary file that actually exists
        try
        {
            ProjectServiceModel projectModel = new(_fixedTime, _fixedTime);

            // Setup the service with a loaded project
            _mockRepository.Setup(repo => repo.LoadAsync(filePath)).ReturnsAsync(projectModel);
            await _service.Open(new OpenProjectRequest { FilePath = filePath });

            // Reset mock to track new invocations
            _mockRepository.Invocations.Clear();

            // Setup for Save method
            _mockRepository.Setup(repo => repo.SaveAsync(It.IsAny<string>(), It.IsAny<ProjectServiceModel>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.Close();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(_service.Current);
            _mockRepository.Verify(repo => repo.SaveAsync(
                filePath,
                It.IsAny<ProjectServiceModel>()));
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}