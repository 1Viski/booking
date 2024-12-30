using Booking.Core.Models;

namespace Booking.Core.Abstracts;

public abstract class BaseChoice
{
    private readonly IDataService _dataService;
    private readonly IConsole _console;

    protected BaseChoice(IDataService dataService, IConsole console)
    {
        _dataService = dataService;
        _console = console;
    }

    public abstract void OnChoice(string hotelsPath, string bookingsPath);
    
    protected bool TryGetData(
        string hotelsPath,
        ref List<Hotel> hotelsData)
    {
        try
        {
            hotelsData = _dataService.GetData<Hotel>(hotelsPath)!.ToList();
            return true;
        }
        catch (Exception exception)
        {
            WriteLineException(exception);
            return false;
        }
    }

    protected bool TryGetData(
        string hotelsPath,
        string bookingsPath,
        ref List<Hotel> hotelsData, 
        ref List<Core.Models.Booking> bookingsData)
    {
        if (!TryGetData(hotelsPath, ref hotelsData))
        {
            return false;
        }
        
        try
        {
            bookingsData = _dataService.GetData<Core.Models.Booking>(bookingsPath)!.ToList();
            return true;
        }
        catch (Exception exception)
        {
            WriteLineException(exception);
            return false;
        }
    }

    private void WriteLineException(Exception exception)
    {
        _console.MarkupLine("[red]Something goes wrong.[/] Please try again later.");
        _console.WriteLine(exception.Message);
    }
}