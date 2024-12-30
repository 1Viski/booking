using Booking.Core.Abstracts;
using Booking.Core.Models;

namespace Booking.Core.Choices;

public class HotelsChoice : BaseChoice
{
    private readonly IConsole _console;
    public HotelsChoice(IDataService dataService, IConsole console) : base(dataService, console)
    {
        _console = console;
    }

    public override void OnChoice(string hotelsPath, string bookingsPath)
    {
        List<Hotel> hotelsData = [];
        
        if (!TryGetData(hotelsPath, ref hotelsData))
        {
            return;
        }

        var table = Creator.CreateHotelsTable(hotelsData);
        _console.WriteLine(table);
    }
}