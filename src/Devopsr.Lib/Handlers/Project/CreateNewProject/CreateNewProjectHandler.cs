using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CreateNewProject;

public class CreateNewProjectHandler : IRequestHandler<CreateNewProjectRequest, Result<CreateNewProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectService _projectService;
    private readonly TimeProvider _timeProvider;

    public CreateNewProjectHandler(IProjectRepository projectRepository, IProjectService projectService, TimeProvider timeProvider)
    {
        _projectRepository = projectRepository;
        _projectService = projectService;
        _timeProvider = timeProvider;
    }

    public async Task<Result<CreateNewProjectResponse>> Handle(CreateNewProjectRequest request, CancellationToken cancellationToken)
    {
        if (!request.FilePath.EndsWith(".devopsr", StringComparison.OrdinalIgnoreCase))
        {
            return Result.Fail(ErrorCodes.InvalidProjectFileExtension);
        }

        if (File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileAlreadyExists);
        }

        var now = _timeProvider.GetLocalNow();
        var project = new ProjectServiceModel
        {
            Name = request.Name,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _projectRepository.SaveProject(project, request.FilePath);
        _projectService.SetCurrentProject(project, request.FilePath);

        var response = new CreateNewProjectResponse(project.Id);
        return Result.Ok(response);
    }
}