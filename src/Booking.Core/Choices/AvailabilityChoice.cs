using Booking.Core.Abstracts;
using Booking.Core.Enums;
using Booking.Core.Models;

namespace Booking.Core.Choices;

public class AvailabilityChoice : BaseChoice
{
    private readonly IDataService _dataService;
    private readonly IConsole _console;

    public AvailabilityChoice(IDataService dataService, IConsole console) : base(dataService, console)
    {
        _dataService = dataService;
        _console = console;
    }

    public override void OnChoice(string hotelsPath, string bookingsPath)
    {
        List<Hotel> hotelsData = [];
        List<Models.Booking> bookingsData = [];
        
        if (!TryGetData(hotelsPath, bookingsPath, ref hotelsData, ref bookingsData))
        {
            return;
        }
        
        _console.Write("Hotel id: ");
        var hotelId = _console.ReadLine();
            
        _console.Write("From (yyyy-mm-dd): ");
        var fromDateString = _console.ReadLine();
            
        _console.Write("To (yyyy-mm-dd): ");
        var toDateString = _console.ReadLine();
            
        _console.Write("Room type: ");
        var roomType = _console.ReadLine();

        DateOnly? fromDateOnly = DateOnly.TryParse(fromDateString, out var fromDate) ? fromDate : null;
        DateOnly? toDateOnly = DateOnly.TryParse(toDateString, out var toDate) ? toDate : null;

        if (fromDateOnly is null && toDateOnly is null)
        {
            _console.MarkupLine("[red]Invalid format of date[/]. Please try again.");
            return;
        }
        
        try
        {
            var bookingRequest = new BookingRequest(
                hotelId!,
                fromDateOnly,
                toDateOnly,
                Enum.Parse<RoomTypeCode>(roomType!));

            var hotelResponse =
                _dataService.GetResponse(bookingRequest, hotelsData, bookingsData).ToList();

            if (hotelResponse.Count == 0)
            {
                _console.MarkupLine("[red]No available hotels found.[/]");
                return;
            }

            var table = Creator.CreateRoomsTable(hotelResponse);
            _console.WriteLine(table);
        }
        catch (FormatException exception)
        {
            _console.MarkupLine("[red]Invalid format of date[/]. Please try again.");
            _console.WriteLine(exception.Message);
        }
        catch (ArgumentException exception)
        {
            _console.MarkupLine("[red]Invalid type of room[/]. Please try again.");
            _console.WriteLine(exception.Message);
        }
        catch (Exception exception)
        {
            _console.WriteLine(exception.Message);
        }
    }
}