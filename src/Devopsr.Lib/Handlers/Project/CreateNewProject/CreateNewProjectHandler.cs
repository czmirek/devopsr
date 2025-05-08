using Devopsr.Lib.Models;
using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CreateNewProject;

public class CreateNewProjectHandler(IProjectRepository projectRepository, TimeProvider timeProvider, CurrentProjectHolderService currentProjectHolderService)
    : IRequestHandler<CreateNewProjectRequest, Result<CreateNewProjectResponse>>
{
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

        var now = timeProvider.GetLocalNow();
        ProjectServiceModel project = new(now, now);
        await projectRepository.SaveAsync(request.FilePath, project);
        // Optionally, set the newly created project as the current project
        // currentProjectHolderService.SetCurrentProject(project, request.FilePath);
        return Result.Ok(new CreateNewProjectResponse());
    }
}