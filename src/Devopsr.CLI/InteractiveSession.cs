using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devopsr.Lib.Handlers.Project.CloseProject;
using MediatR;

namespace Devopsr.CLI;

internal class InteractiveSession
{
    private readonly ISender _sender;
    private readonly string _filePath;
    private readonly Dictionary<string, Func<string[], Task<bool>>> _commandHandlers; // bool indicates if session should continue

    public InteractiveSession(ISender sender, string filePath)
    {
        _sender = sender;
        _filePath = filePath;
        _commandHandlers = new Dictionary<string, Func<string[], Task<bool>>>(StringComparer.OrdinalIgnoreCase)
        {
            { "close", HandleClose }
            // Future commands like "add-node", "list-nodes" can be added here
            // Example: { "add-node", HandleAddNode }
        };
    }

    public async Task Start()
    {
        Console.WriteLine($"✅ Project file '{_filePath}' loaded into memory. Type commands to modify, or 'close' to save and exit.");
        bool continueSession = true;
        while (continueSession)
        {
            Console.Write("devopsr> ");
            string? line = Console.ReadLine();
            if (line == null) // Treat Ctrl+C or EOF as a "close"
            {
                continueSession = await HandleClose([]);
                break;
            }

            var parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                continue;
            }

            var commandName = parts[0];
            var args = parts.Skip(1).ToArray();

            if (_commandHandlers.TryGetValue(commandName, out var handler))
            {
                continueSession = await handler(args);
            }
            else
            {
                Console.WriteLine($"Unknown command: {commandName}. Available commands: {string.Join(", ", _commandHandlers.Keys)}");
            }
        }
    }

    private async Task<bool> HandleClose(string[] args)
    {
        var closeResult = await _sender.Send(new CloseProjectRequest());
        Formatter.PrintResult(closeResult, $"Project file '{_filePath}' closed and saved.");
        return false; // Signal to end the session
    }

    // Example placeholder for a future command handler
    // private async Task<bool> HandleAddNode(string[] args)
    // {
    //     if (args.Length < 1)
    //     {
    //         Console.WriteLine("Usage: add-node <node-name>");
    //         return true; // Continue session
    //     }
    //     var nodeName = args[0];
    //     // var request = new AddNodeRequest { Name = nodeName /*, other properties */ };
    //     // var result = await _sender.Send(request);
    //     // Formatter.PrintResult(result, $"Node '{nodeName}' added.");
    //     Console.WriteLine($"Placeholder for adding node: {nodeName}. This command is not yet implemented.");
    //     return true; // Continue session
    // }
}
