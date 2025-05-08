using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Project;
using Moq;

namespace Devopsr.Test.Services.Project;

/// <summary>
/// This class serves as a container for very simple tests.
/// Tests with more than 10 lines are placed in separate files.
/// </summary>
public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _mockRepository;
    private readonly TimeProvider _timeProvider;
    private readonly ProjectService _service;

    public ProjectServiceTests()
    {
        _mockRepository = new Mock<IProjectRepository>();
        _timeProvider = TimeProvider.System;
        _service = new ProjectService(_mockRepository.Object, _timeProvider);
    }

    // Add any simple tests (< 10 lines) here if needed
}