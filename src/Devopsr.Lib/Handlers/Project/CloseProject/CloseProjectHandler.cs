using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CloseProject;

public class CloseProjectHandler(IProjectRepository projectRepository, TimeProvider timeProvider, CurrentProjectHolderService currentProjectHolderService)
    : IRequestHandler<CloseProjectRequest, Result<CloseProjectResponse>>
{
    public async Task<Result<CloseProjectResponse>> Handle(CloseProjectRequest request, CancellationToken cancellationToken)
    {
        if (currentProjectHolderService.CurrentProject == null || string.IsNullOrEmpty(currentProjectHolderService.CurrentFilePath))
        {
            return Result.Fail(ErrorCodes.NoProjectLoaded);
        }

        var projectToSave = currentProjectHolderService.CurrentProject;
        projectToSave.LastUpdate = timeProvider.GetLocalNow();
        await projectRepository.SaveAsync(currentProjectHolderService.CurrentFilePath, projectToSave);

        currentProjectHolderService.ClearCurrentProject();
        return Result.Ok(new CloseProjectResponse());
    }
}