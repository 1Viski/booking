using Booking.Core.Abstracts;
using Booking.Core.Enums;
using Booking.Core.Models;
using Spectre.Console;

namespace Booking.App;

public class App
{
    private const string AppName = "Booking";
    private const string HotelsCommand = "--hotels";
    private const string BookingsCommand = "--bookings";
    
    private readonly IDataService _dataService;
    private readonly IQueryService _queryService;

    public App(IDataService dataService, IQueryService queryService)
    {
        _dataService = dataService;
        _queryService = queryService;
    }

    public void Run(string[] args)
    {
        AnsiConsole.Write(
            new FigletText(AppName)
                .LeftJustified()
                .Color(Color.Blue));
        
        while (true)
        {
            var hotelsPath = string.Empty;
            var bookingsPath = string.Empty;
    
            var choices = Enum.GetValues<Choice>()
                .Select(x => x.ToString())
                .ToArray();
    
            if (args.Length == 0)
            {
                AnsiConsole.WriteLine("[red]Please specify an argument.[/]");
                return;
            }
            
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == HotelsCommand)
                {
                    hotelsPath = args[i + 1];
                }
    
                if (args[i] == BookingsCommand)
                {
                    bookingsPath = args[i + 1];
                }
            }
            
            var hotelsData = _dataService.GetData<Hotel>(hotelsPath)!.ToList();
            var bookingsData = _dataService.GetData<Core.Models.Booking>(bookingsPath)!.ToList();
    
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Service menu:")
                    .MoreChoicesText("[grey](Move up and down)[/]")
                    .AddChoices(choices));
    
            switch (Enum.Parse<Choice>(choice))
            {
                case Choice.Hotels:
                    ForChoiceHotels(hotelsData);
                    break;
                case Choice.Availability:
                    ForAvailabilityChoice(hotelsData, bookingsData);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    private static void ForChoiceHotels(IEnumerable<Hotel> hotelsData)
    {
        var table = new Table();
        var titleStyle = new Style(foreground: Color.Blue);
        table.Title = new TableTitle("Hotels", titleStyle);
        table.Border(TableBorder.Square);
        table.BorderColor(Color.Blue);
        table.AddColumns("Id", "Name");

        foreach (var hotel in hotelsData)
        {
            table.AddRow(hotel.Id, hotel.Name!);
        }
            
        AnsiConsole.Write(table);
    }

    private void ForAvailabilityChoice(IEnumerable<Hotel> hotelsData, IEnumerable<Core.Models.Booking> bookingsData)
    {
        AnsiConsole.Write("Hotel id: ");
        var hotelId = Console.ReadLine();
            
        AnsiConsole.Write("From (yyyy-mm-dd): ");
        var fromDate = Console.ReadLine();
            
        AnsiConsole.Write("To (yyyy-mm-dd): ");
        var toDate = Console.ReadLine();
            
        AnsiConsole.Write("Room type: ");
        var roomType = Console.ReadLine();

        var bookingRequest = new BookingRequest(
            hotelId!, 
            DateOnly.Parse(fromDate!), 
            DateOnly.Parse(toDate!), 
            Enum.Parse<RoomTypeCode>(roomType!));
            
        var hotelResponse = _queryService.GetResponse(bookingRequest, hotelsData, bookingsData).ToList();
    }
}