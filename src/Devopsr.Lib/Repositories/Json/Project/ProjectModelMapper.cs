using Devopsr.Lib.Services.Models;

namespace Devopsr.Lib.Repositories.Json.Project;

public static class ProjectModelMapper
{
    public static ProjectJsonModel ToJsonModel(ProjectServiceModel model)
    {
        return new()
        {
            Created = model.Created,
            LastUpdate = model.LastUpdate
        };
    }

    public static ProjectServiceModel ToInMemoryModel(ProjectJsonModel model)
    {
        return new(model.Created, model.LastUpdate);
    }
}
