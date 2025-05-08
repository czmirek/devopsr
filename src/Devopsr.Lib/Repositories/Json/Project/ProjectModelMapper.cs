using Devopsr.Lib.Models;

namespace Devopsr.Lib.Repositories.Json.Project;

public static class ProjectModelMapper
{
    public static ProjectJsonModel ToJsonModel(ProjectInMemoryModel model)
        => new ProjectJsonModel { Created = model.Created };

    public static ProjectInMemoryModel ToInMemoryModel(ProjectJsonModel model)
        => new ProjectInMemoryModel { Created = model.Created };
}
