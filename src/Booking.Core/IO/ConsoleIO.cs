using Booking.Core.Abstracts;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Booking.Core.IO;

public class ConsoleIO : IConsole
{
    public void Write(string value)
    {
        AnsiConsole.Write(value);
    }

    public void Write(IRenderable renderable)
    {
        AnsiConsole.Write(renderable);
    }

    public void WriteLine()
    {
        AnsiConsole.WriteLine();
    }

    public void WriteLine(string value)
    {
        AnsiConsole.WriteLine(value);
    }

    public void WriteLine(IRenderable renderable)
    {
        AnsiConsole.Write(renderable);
        AnsiConsole.WriteLine();
    }

    public void Markup(string value)
    {
        AnsiConsole.Markup(value);
    }

    public void MarkupLine(string value)
    {
        AnsiConsole.MarkupLine(value);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}