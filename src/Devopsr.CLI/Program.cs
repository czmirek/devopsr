using Devopsr.Lib;
using Devopsr.Lib.Services.Project.Models;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = DevopsrFacade.BuildServiceProvider();
var facade = serviceProvider.GetRequiredService<IDevopsrFacade>();

if (args.Length == 2 && args[0] == "new") {
    string filePath = args[1];
    var result = facade.ProjectService.CreateNewProject(new CreateNewProjectRequest {
        FilePath = filePath,
    });
    Console.WriteLine(result);
    return;
}

Console.WriteLine("Usage: devopsr new <path-to-project.devopsr>");