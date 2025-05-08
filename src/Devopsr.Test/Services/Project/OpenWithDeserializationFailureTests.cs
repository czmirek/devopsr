using Devopsr.Lib.Services;
using Devopsr.Lib.Services.Project.Models;
using Devopsr.Lib.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class OpenWithDeserializationFailureTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task Open_WithDeserializationFailure_ReturnsFailure()
    {
        // Arrange
        string filePath = Path.GetTempFileName(); // Create a temporary file that actually exists
        try
        {
            OpenProjectRequest request = new() { FilePath = filePath };

            _mockRepository.Setup(repo => repo.LoadAsync(filePath))
                .ReturnsAsync((ProjectServiceModel?)null);

            // Act
            var result = await _service.Open(request);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains(result.Errors, e =>
            {
                return e.Message == ErrorCodes.ProjectFileDeserializationFailed;
            });
            Assert.Null(_service.Current);
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