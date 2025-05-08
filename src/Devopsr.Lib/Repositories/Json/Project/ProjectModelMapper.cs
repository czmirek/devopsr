using Devopsr.Lib.Models;

namespace Devopsr.Lib.Repositories.Json.Project;

public static class ProjectModelMapper
{
    public static ProjectJsonModel ToJsonModel(ProjectInMemoryModel model)
    {
        return new()
        { Created = model.Created };
    }

    public static ProjectInMemoryModel ToInMemoryModel(ProjectJsonModel model)
    {
        return new()
        { Created = model.Created };
    }
}
