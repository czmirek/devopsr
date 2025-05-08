using System.CommandLine;
using Devopsr.Lib.Handlers.Project.CreateNewProject;
using MediatR;
using Devopsr.CLI; // For Formatter

namespace Devopsr.CLI.Commands;

public static class NewCommandFactory
{
    public static Command Create(ISender sender)
    {
        var filePathArgument = new Argument<string>("filePath", "Path to the new project file");
        Command newCommand = new("new", "Create a new project file")
        {
            filePathArgument
        };

        newCommand.SetHandler(async (string filePathValue) =>
        {
            var result = await sender.Send(new CreateNewProjectRequest { FilePath = filePathValue });
            Formatter.PrintResult(result, $"Created new project file at '{filePathValue}'.");
        }, filePathArgument);

        return newCommand;
    }
}
