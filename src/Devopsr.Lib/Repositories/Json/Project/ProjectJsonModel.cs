namespace Devopsr.Lib.Repositories.Json.Project;

public sealed class ProjectJsonModel
{
    public required DateTimeOffset Created { get; init; }
    public required DateTimeOffset LastUpdate { get; init; }
}