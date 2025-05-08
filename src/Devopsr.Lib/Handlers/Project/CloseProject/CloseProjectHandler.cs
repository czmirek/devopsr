using Devopsr.Lib.Services;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CloseProject;

internal class CloseProjectHandler(ILoadedProject loadedProject) : IRequestHandler<CloseProjectRequest, Result>
{
    public Task<Result> Handle(CloseProjectRequest request, CancellationToken cancellationToken)
    {
        if (loadedProject.CurrentProject is null)
        {
            return Task.FromResult(Result.Fail(ErrorCodes.NoProjectOpen));
        }

        loadedProject.ClearCurrentProject();
        return Task.FromResult(Result.Ok());
    }
}