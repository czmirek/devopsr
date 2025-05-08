namespace Devopsr.Lib.Models;

public sealed class ProjectServiceModel
{
    internal ProjectServiceModel(DateTimeOffset created, DateTimeOffset lastUpdate)
    {
        Created = created;
        LastUpdate = lastUpdate;
        RootNode = new NodeServiceModel { Id = "root" };
    }

    public DateTimeOffset Created { get; }
    public DateTimeOffset LastUpdate { get; internal set; }
    public NodeServiceModel RootNode { get; internal set; }
}