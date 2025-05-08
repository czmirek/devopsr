using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CloseProject;

public record CloseProjectRequest : IRequest<Result>;
