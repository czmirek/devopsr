namespace Devopsr.Lib.Models;

public class NodeServiceModel
{
    public required string Id { get; set; }
    public List<NodeServiceModel> Children { get; set; } = new();
}
