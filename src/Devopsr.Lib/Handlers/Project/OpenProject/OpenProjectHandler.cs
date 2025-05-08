using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.OpenProject;

public class OpenProjectHandler(IProjectRepository projectRepository, CurrentProjectHolderService currentProjectHolderService)
    : IRequestHandler<OpenProjectRequest, Result<OpenProjectResponse>>
{
    public async Task<Result<OpenProjectResponse>> Handle(OpenProjectRequest request, CancellationToken cancellationToken)
    {
        if (!File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        var projectModel = await projectRepository.LoadAsync(request.FilePath);

        if (projectModel is null)
        {
            return Result.Fail(ErrorCodes.ProjectFileDeserializationFailed);
        }

        currentProjectHolderService.SetCurrentProject(projectModel, request.FilePath);
        return Result.Ok(new OpenProjectResponse());
    }
}