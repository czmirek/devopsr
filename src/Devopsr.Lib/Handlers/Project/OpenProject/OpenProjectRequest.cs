using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.OpenProject;

public sealed class OpenProjectRequest : IRequest<Result>
{
    public required string FilePath { get; init; }
}
