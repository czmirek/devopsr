using Devopsr.Lib.Repositories.Interfaces;
using Devopsr.Lib.Services;
using Devopsr.Lib.Services.Models;
using FluentResults;
using MediatR;

namespace Devopsr.Lib.Handlers.Project.AddNode;

internal class AddNodeHandler(
    ILoadedProject loadedProject,
    IProjectRepository projectRepository,
    TimeProvider timeProvider) : IRequestHandler<AddNodeRequest, Result>
{
    public async Task<Result> Handle(AddNodeRequest request, CancellationToken cancellationToken)
    {
        if (loadedProject.CurrentProject is null || string.IsNullOrEmpty(loadedProject.CurrentFilePath))
        {
            return Result.Fail(ErrorCodes.NoProjectOpen);
        }

        var project = loadedProject.CurrentProject;

        var parentNode = FindNode(project.RootNode, request.ParentNodeId);
        if (parentNode is null)
        {
            return Result.Fail(ErrorCodes.ParentNodeNotFound);
        }

        if (NodeExists(project.RootNode, request.NewNodeId))
        {
            return Result.Fail(ErrorCodes.NodeIdAlreadyExists);
        }

        var newNode = new NodeServiceModel { Id = request.NewNodeId };

        // Since NodeServiceModel.Children is IReadOnlyList, we need to create a new list
        var children = parentNode.Children.ToList();
        children.Add(newNode);
        parentNode.Children = children;

        project.LastUpdate = timeProvider.GetUtcNow();

        await projectRepository.SaveAsync(loadedProject.CurrentFilePath, project);

        return Result.Ok();
    }

    private NodeServiceModel? FindNode(NodeServiceModel currentNode, string nodeId)
    {
        if (currentNode.Id == nodeId)
        {
            return currentNode;
        }

        foreach (var child in currentNode.Children)
        {
            var found = FindNode(child, nodeId);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }

    private bool NodeExists(NodeServiceModel currentNode, string nodeId)
    {
        if (currentNode.Id == nodeId)
        {
            return true;
        }
        return currentNode.Children.Any(child => NodeExists(child, nodeId));
    }
}
