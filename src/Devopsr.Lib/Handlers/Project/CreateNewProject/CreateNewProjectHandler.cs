using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using Devopsr.Lib.Services.Models;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CreateNewProject;

internal class CreateNewProjectHandler(IProjectRepository projectRepository, ILoadedProject loadedProject, TimeProvider timeProvider) 
    : IRequestHandler<CreateNewProjectRequest, Result>
{
    public async Task<Result> Handle(CreateNewProjectRequest request, CancellationToken cancellationToken)
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
        var project = new ProjectServiceModel(now, now);

        await projectRepository.SaveAsync(request.FilePath, project);
        loadedProject.SetCurrentProject(project, request.FilePath);

        return Result.Ok();
    }
}