using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.OpenProject;

internal class OpenProjectHandler(IProjectRepository projectRepository, ILoadedProject projectService) 
    : IRequestHandler<OpenProjectRequest, Result>
{
    public async Task<Result> Handle(OpenProjectRequest request, CancellationToken cancellationToken)
    {
        if (!File.Exists(request.FilePath))
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        var project = await projectRepository.LoadAsync(request.FilePath);
        if (project is null)
        {
            return Result.Fail(ErrorCodes.ProjectFileDoesNotExist);
        }

        projectService.SetCurrentProject(project, request.FilePath);
        return Result.Ok();
    }
}