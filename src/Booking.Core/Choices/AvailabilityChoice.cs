using Booking.Core.Abstracts;
using Booking.Core.Enums;
using Booking.Core.Models;
using Spectre.Console;

namespace Booking.Core.Choices;

public class AvailabilityChoice : BaseChoice
{
    private readonly IDataService _dataService;

    public AvailabilityChoice(IDataService dataService) : base(dataService)
    {
        _dataService = dataService;
    }

    public override void OnChoice(string hotelsPath, string bookingsPath)
    {
        List<Hotel> hotelsData = [];
        List<Models.Booking> bookingsData = [];
        
        if (!TryGetData(hotelsPath, bookingsPath, ref hotelsData, ref bookingsData))
        {
            return;
        }
        
        AnsiConsole.Write("Hotel id: ");
        var hotelId = Console.ReadLine();
            
        AnsiConsole.Write("From (yyyy-mm-dd): ");
        var fromDateString = Console.ReadLine();
            
        AnsiConsole.Write("To (yyyy-mm-dd): ");
        var toDateString = Console.ReadLine();
            
        AnsiConsole.Write("Room type: ");
        var roomType = Console.ReadLine();

        DateOnly? fromDateOnly = DateOnly.TryParse(fromDateString, out var fromDate) ? fromDate : null;
        DateOnly? toDateOnly = DateOnly.TryParse(toDateString, out var toDate) ? toDate : null;
        
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
                AnsiConsole.MarkupLine("[red]No available hotels found.[/]");
                return;
            }

            var table = new Table();
            var titleStyle = new Style(foreground: Color.Blue);
            table.Title = new TableTitle("Rooms", titleStyle);
            table.Border(TableBorder.Square);
            table.BorderColor(Color.Blue);
            table.AddColumns("Number", "Room type");

            foreach (var room in hotelResponse.SelectMany(hotel => hotel.Rooms))
            {
                table.AddRow(room.RoomId, room.RoomType.ToString());
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }
        catch (FormatException exception)
        {
            AnsiConsole.MarkupLine("[red]Invalid format of date[/]. Please try again.");
            AnsiConsole.WriteLine(exception.Message);
        }
        catch (ArgumentException exception)
        {
            AnsiConsole.MarkupLine("[red]Invalid type of room[/]. Please try again.");
            AnsiConsole.WriteLine(exception.Message);
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteLine(exception.Message);
        }
    }
}