using Devopsr.Lib;
using Devopsr.Lib.Services.Project.Models;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = DevopsrFacade.BuildServiceProvider();
var facade = serviceProvider.GetRequiredService<IDevopsrFacade>();

if(args.Length == 2 && args[0] == "new")
{
    string filePath = args[1];
    var resultTask = facade.ProjectService.CreateNewProject(new CreateNewProjectRequest
    {
        FilePath = filePath,
    });
    var result = await resultTask;
    Formatter.PrintResult(result, $"Created new project file at '{filePath}'.");
    return;
}

if(args.Length == 2 && args[0] == "open")
{
    string filePath = args[1];
    var openResult = await facade.ProjectService.Open(new OpenProjectRequest
    {
        FilePath = filePath
    });

    if(!openResult.IsSuccess)
    {
        Formatter.WriteErrorResult(openResult);
        return;
    }

    Console.WriteLine($"✅ Project file '{filePath}' loaded into memory. Type commands to modify, or 'close' to save and exit.");
    while(true)
    {
        Console.Write("devopsr> ");

        string? input = Console.ReadLine();

        if(input == null || input.Trim().ToLowerInvariant() == "close")
        {
            var closeResult = await facade.ProjectService.Close();
            Formatter.PrintResult(closeResult, $"Project file '{filePath}' closed and saved.");
            break;
        }

        // Here you can add more commands to manipulate the in-memory model via the service
        Console.WriteLine($"Unknown command: {input}");
    }

    return;
}

Console.WriteLine("Usage: devopsr new <path-to-project.devopsr>");