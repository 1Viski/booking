using Spectre.Console;

namespace Booking.App;

public static class Arguments
{
    public static bool TryGetPaths(
        string[] args, 
        string[] commands,
        ref List<string> paths)
    {
        if (args.Length == 0)
        {
            AnsiConsole.MarkupLine("[red]Please specify an argument.[/]");
            return false;
        }

        try
        {
            for (var i = 0; i < args.Length; i++)
            {
                foreach (var command in commands)
                {
                    if (args[i] == command)
                    {
                        paths.Add(args[i + 1]);
                    }
                }
            }

            return true;
        }
        catch (IndexOutOfRangeException exception)
        {
            AnsiConsole.MarkupLine("[red]Invalid argument.[/] Please try again with valid arguments.");
            AnsiConsole.WriteLine(exception.Message);
            return false;
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteLine(exception.Message);
            return false;
        }
    }
}