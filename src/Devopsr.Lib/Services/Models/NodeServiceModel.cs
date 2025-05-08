namespace Devopsr.Lib.Services.Models;

public class NodeServiceModel
{
    public required string Id { get; init; }
    public IReadOnlyList<NodeServiceModel> Children { get; set; } = new List<NodeServiceModel>();
}
