using System.CommandLine;
using Devopsr.Lib;
using Devopsr.Lib.Handlers.Project.CloseProject;
using Devopsr.Lib.Handlers.Project.CreateNewProject;
using Devopsr.Lib.Handlers.Project.OpenProject;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Devopsr.CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var sender = CreateSender();

        RootCommand rootCommand = new("Devopsr CLI");
        Command newCommand = new("new", "Create a new project file")
        {
            new Argument<string>("filePath", "Path to the new project file")
        };
        newCommand.SetHandler(async (string filePath) =>
        {
            var result = await sender.Send(new CreateNewProjectRequest { FilePath = filePath });
            Formatter.PrintResult(result, $"Created new project file at '{filePath}'.");
        }, (System.CommandLine.Binding.IValueDescriptor<string>)newCommand.Arguments[0]);

        Command openCommand = new("open", "Open and interact with a project file")
        {
            new Argument<string>("filePath", "Path to the project file to open")
        };
        openCommand.SetHandler(async (string filePath) =>
        {
            var result = await sender.Send(new OpenProjectRequest { FilePath = filePath });
            if (!result.IsSuccess)
            {
                Formatter.WriteErrorResult(result);
                return;
            }

            Console.WriteLine($"✅ Project file '{filePath}' loaded into memory. Type commands to modify, or 'close' to save and exit.");
            while (true)
            {
                Console.Write("devopsr> ");
                string? input = Console.ReadLine();
                if (input == null || input.Trim().ToLowerInvariant() == "close")
                {
                    var closeResult = await sender.Send(new CloseProjectRequest());
                    Formatter.PrintResult(closeResult, $"Project file '{filePath}' closed and saved.");
                    break;
                }
                // Here you can add more commands to manipulate the in-memory model via the service
                Console.WriteLine($"Unknown command: {input}");
            }
        }, (System.CommandLine.Binding.IValueDescriptor<string>)openCommand.Arguments[0]);

        rootCommand.AddCommand(newCommand);
        rootCommand.AddCommand(openCommand);

        await rootCommand.InvokeAsync(args);
    }

    private static ISender CreateSender()
    {
        var services = new ServiceCollection();
        services.AddDevopsrLib();
        var serviceProvider = services.BuildServiceProvider();
        var sender = serviceProvider.GetRequiredService<ISender>();
        return sender;
    }
}