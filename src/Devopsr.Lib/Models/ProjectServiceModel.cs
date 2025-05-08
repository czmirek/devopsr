namespace Devopsr.Lib.Models;

public sealed class ProjectServiceModel
{
    internal ProjectServiceModel(DateTimeOffset created, DateTimeOffset lastUpdate)
    {
        Created = created;
        LastUpdate = lastUpdate;
    }

    public DateTimeOffset Created { get; }
    public DateTimeOffset LastUpdate { get; internal set; }
}