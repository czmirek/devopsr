using System.CommandLine;
using Devopsr.Lib;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Devopsr.CLI.Commands;

namespace Devopsr.CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var sender = CreateSender();

        RootCommand rootCommand = new("Devopsr CLI");

        Command newCommand = NewCommandFactory.Create(sender);
        Command openCommand = OpenCommandFactory.Create(sender);

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