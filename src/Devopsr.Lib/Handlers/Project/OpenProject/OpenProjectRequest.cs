using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.OpenProject;

public record OpenProjectRequest(string FilePath) : IRequest<Result<OpenProjectResponse>>;
