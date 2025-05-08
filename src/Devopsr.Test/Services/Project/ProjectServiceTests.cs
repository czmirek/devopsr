using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using Devopsr.Lib.Services.Project;
using Devopsr.Lib.Services.Project.Models;
using Moq;

namespace Devopsr.Test.Services.Project;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _mockRepository;
    private readonly TimeProvider _timeProvider;
    private readonly ProjectService _service;
    private readonly DateTimeOffset _fixedTime = new(2025, 5, 8, 10, 0, 0, TimeSpan.Zero);

    public ProjectServiceTests()
    {
        _mockRepository = new Mock<IProjectRepository>();
        _timeProvider = TimeProvider.System;
        _service = new ProjectService(_mockRepository.Object, _timeProvider);
    }

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