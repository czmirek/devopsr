using Devopsr.Lib.Models;
using Devopsr.Lib.Services;
using Moq;

namespace Devopsr.Test.Services.Project;

public class CloseWithNoLoadedProjectTests : ProjectServiceTestsBase
{
    [Fact]
    public async Task Close_WithNoLoadedProject_ReturnsFailure()
    {
        // Arrange - service initialized with no project loaded

        // Act
        var result = await _service.Close();

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e =>
        {
            return e.Message == ErrorCodes.NoProjectLoaded;
        });
        _mockRepository.Verify(repo => repo.SaveAsync(It.IsAny<string>(), It.IsAny<ProjectServiceModel>()), Times.Never);
    }
}