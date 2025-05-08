using System.CommandLine;
using Devopsr.Lib.Handlers.Project.OpenProject;
using MediatR;
using Devopsr.CLI; // For Formatter and InteractiveSession

namespace Devopsr.CLI.Commands;

public static class OpenCommandFactory
{
    public static Command Create(ISender sender)
    {
        var filePathArgument = new Argument<string>("filePath", "Path to the project file to open");
        Command openCommand = new("open", "Open and interact with a project file")
        {
            filePathArgument
        };

        openCommand.SetHandler(async (string filePathValue) =>
        {
            var result = await sender.Send(new OpenProjectRequest { FilePath = filePathValue });
            if (!result.IsSuccess)
            {
                Formatter.WriteErrorResult(result);
                return;
            }

            var session = new InteractiveSession(sender, filePathValue);
            await session.Start();
        }, filePathArgument);

        return openCommand;
    }
}
