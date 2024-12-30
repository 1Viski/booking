using Booking.Core.Abstracts;
using Booking.Core.Enums;
using Spectre.Console;

namespace Booking.App;

public class App
{
    private const string AppName = "Booking";
    
    private readonly string[] _commands = ["--hotels", "--bookings"];
    private readonly IChoiceFactory _choiceFactory;

    public App(IChoiceFactory choiceFactory)
    {
        _choiceFactory = choiceFactory;
    }

    public void Run(string[] args)
    {
        AnsiConsole.Write(
            new FigletText(AppName)
                .LeftJustified()
                .Color(Color.Blue));

        List<string> paths = [];

        if (!Arguments.TryGetPaths(args, _commands, ref paths))
        {
            return;
        }
        
        var hotelsPath = paths[0];
        var bookingsPath = paths[1];

        if (!Path.Exists(hotelsPath) || !Path.Exists(bookingsPath))
        {
            AnsiConsole.MarkupLine("[red]Invalid path to data.[/] Please try again with valid data.");
            return;
        }

        while (!Console.KeyAvailable)
        {
            var choices = Enum.GetValues<StringChoice>()
                .Select(x => x.ToString())
                .ToArray();
    
            var prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Service menu:[/]")
                    .MoreChoicesText("[grey](Move up and down)[/]")
                    .AddChoices(choices));
            
            var choice = _choiceFactory.CreateChoice(prompt);
            choice.OnChoice(hotelsPath, bookingsPath);
            
            AnsiConsole.MarkupLine("[blue]Press <ENTER> to continue or <Q> to exit...[/]");
            
            if (Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}