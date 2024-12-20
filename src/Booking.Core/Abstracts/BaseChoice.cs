using Booking.Core.Models;
using Spectre.Console;

namespace Booking.Core.Abstracts;

public abstract class BaseChoice
{
    private readonly IDataService _dataService;

    protected BaseChoice(IDataService dataService)
    {
        _dataService = dataService;
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

    private static void WriteLineException(Exception exception)
    {
        AnsiConsole.MarkupLine("[red]Something goes wrong.[/] Please try again later.");
        AnsiConsole.WriteLine(exception.Message);
    }
}