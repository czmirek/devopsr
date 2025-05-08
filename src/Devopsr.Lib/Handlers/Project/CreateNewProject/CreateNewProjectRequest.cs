using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.CreateNewProject;

public record CreateNewProjectRequest(string FilePath) : IRequest<Result>;
