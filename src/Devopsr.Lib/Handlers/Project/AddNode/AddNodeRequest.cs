using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.AddNode;

public sealed class AddNodeRequest : IRequest<Result>
{
    public required string ParentNodeId { get; init; }
    public required string NewNodeId { get; init; }
}
