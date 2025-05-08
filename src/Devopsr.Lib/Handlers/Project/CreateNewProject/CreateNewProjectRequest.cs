using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CreateNewProject;

public sealed class CreateNewProjectRequest : IRequest<Result>
{
    public required string FilePath { get; init; }
}
