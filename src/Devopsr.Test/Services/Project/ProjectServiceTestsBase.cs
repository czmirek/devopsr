using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services.Project;
using Moq;

namespace Devopsr.Test.Services.Project;

public abstract class ProjectServiceTestsBase
{
    protected readonly Mock<IProjectRepository> _mockRepository;
    protected readonly TimeProvider _timeProvider;
    protected readonly ProjectService _service;
    protected readonly DateTimeOffset _fixedTime = new(2025, 5, 8, 10, 0, 0, TimeSpan.Zero);

    protected ProjectServiceTestsBase()
    {
        _mockRepository = new Mock<IProjectRepository>();
        _timeProvider = TimeProvider.System;
        _service = new ProjectService(_mockRepository.Object, _timeProvider);
    }
}