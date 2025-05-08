using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CloseProject;

public class CloseProjectHandler : IRequestHandler<CloseProjectRequest, Result<CloseProjectResponse>>
{
    private readonly IProjectService _projectService;

    public CloseProjectHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public Task<Result<CloseProjectResponse>> Handle(CloseProjectRequest request, CancellationToken cancellationToken)
    {
        if (_projectService.CurrentProject is null)
        {
            return Task.FromResult(Result.Fail<CloseProjectResponse>(ErrorCodes.NoProjectOpen));
        }

        _projectService.ClearCurrentProject();
        var response = new CloseProjectResponse();
        return Task.FromResult(Result.Ok(response));
    }
}