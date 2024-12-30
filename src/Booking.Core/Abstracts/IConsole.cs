using Spectre.Console.Rendering;

namespace Booking.Core.Abstracts;

public interface IConsole
{
    public void Write(string value);
    
    public void Write(IRenderable renderable);
    
    public void WriteLine();
    
    public void WriteLine(string value);
    
    public void WriteLine(IRenderable renderable);

    public void Markup(string value);
    
    public void MarkupLine(string value);
    
    public string? ReadLine();
}