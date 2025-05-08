using Devopsr.Lib.Models;
using Devopsr.Lib.Services.Project.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class OpenLoadsProjectSuccessfullyTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task Open_LoadsProjectSuccessfully()
    {
        // Arrange
        string filePath = Path.GetTempFileName(); // Create a temporary file that actually exists
        try
        {
            OpenProjectRequest request = new() { FilePath = filePath };
            ProjectServiceModel projectModel = new(_fixedTime, _fixedTime);

            _mockRepository.Setup(repo => repo.LoadAsync(filePath))
                .ReturnsAsync(projectModel);

            // Act
            var result = await _service.Open(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(projectModel, _service.Current);
            _mockRepository.Verify(repo => repo.LoadAsync(filePath), Times.Once);
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