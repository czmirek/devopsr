// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Devopsr.Lib;
using Devopsr.Lib.Interfaces.Project;

var facade = new DevopsrFacade();

if (args.Length == 2 && args[0] == "new")
{
    var filePath = args[1];
    var result = facade.ProjectService.CreateNewProject(filePath);
    Console.WriteLine(result);
    return;
}

Console.WriteLine("Usage: devopsr new <path-to-project.devopsr>");
