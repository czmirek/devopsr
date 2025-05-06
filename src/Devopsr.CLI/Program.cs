using System.Text.Json;
using Devopsr.Lib;
using Devopsr.Lib.Interfaces.Project;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = DevopsrFacade.BuildServiceProvider();
var facade = serviceProvider.GetRequiredService<IDevopsrFacade>();

if (args.Length == 2 && args[0] == "new")
{
    var filePath = args[1];
    var result = facade.ProjectService.CreateNewProject(filePath);
    Console.WriteLine(result);
    return;
}

Console.WriteLine("Usage: devopsr new <path-to-project.devopsr>");
