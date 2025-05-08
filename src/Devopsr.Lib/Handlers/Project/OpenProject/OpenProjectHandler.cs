using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.OpenProject;

public class OpenProjectHandler : IRequestHandler<OpenProjectRequest, Result<OpenProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectService _projectService;

    public OpenProjectHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        _projectRepository = projectRepository;
        _projectService = projectService;
    }

    public async Task<Result<OpenProjectResponse>> Handle(OpenProjectRequest request, CancellationToken cancellationToken)
    {
        if (!File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        var project = await _projectRepository.GetProject(request.FilePath);
        if (project is null)
        {
            return Result.Fail<OpenProjectResponse>(ErrorCodes.ProjectNotFound);
        }

        _projectService.SetCurrentProject(project, request.FilePath);
        var response = new OpenProjectResponse(project);
        return Result.Ok(response);
    }
}