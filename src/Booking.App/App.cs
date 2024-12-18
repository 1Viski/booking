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
        
        var hotelsPath = string.Empty;
        var bookingsPath = string.Empty;
        List<Hotel> hotelsData = [];
        List<Core.Models.Booking> bookingsData = [];

        if (!IsValidArguments(args, ref hotelsPath, ref bookingsPath))
        {
            return;
        }

        if (!IsValidPaths(hotelsPath, bookingsPath, ref hotelsData, ref bookingsData))
        {
            return;
        }

        while (true)
        {
            var choices = Enum.GetValues<Choice>()
                .Select(x => x.ToString())
                .ToArray();
    
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Service menu:[/]")
                    .MoreChoicesText("[grey](Move up and down)[/]")
                    .AddChoices(choices));
    
            switch (Enum.Parse<Choice>(choice))
            {
                case Choice.Hotels:
                    OnChoiceHotels(hotelsData);
                    break;
                case Choice.Availability:
                    OnAvailabilityChoice(hotelsData, bookingsData);
                    break;
            }
        }
    }

    private static void OnChoiceHotels(IEnumerable<Hotel> hotelsData)
    {
        var table = new Table();
        var titleStyle = new Style(foreground: Color.Blue);
        table.Title = new TableTitle("Hotels", titleStyle);
        table.Border(TableBorder.Square);
        table.BorderColor(Color.Blue);
        table.AddColumns("Id", "Name", "Room types");

        foreach (var hotel in hotelsData)
        {
            var roomTypes = GetRoomTypes(hotel.RoomTypes.ToArray());
            var panelRoomTypes = new Panel(roomTypes);
            panelRoomTypes.BorderColor(Color.Gold1);
            panelRoomTypes.Border = BoxBorder.Square;
            table.AddRow(new Markup(hotel.Id), new Markup(hotel.Name!), panelRoomTypes);
        }
            
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static string GetRoomTypes(RoomType[] roomTypes)
    {
        var result = string.Empty;
        
        foreach (var roomType in roomTypes)
        {
            if (roomType == roomTypes.Last())
            {
                result += $"{roomType.Code}";
                break;
            }
            
            result += $"{roomType.Code}\n";
        }

        return result;
    }

    private void OnAvailabilityChoice(IEnumerable<Hotel> hotelsData, IEnumerable<Core.Models.Booking> bookingsData)
    {
        AnsiConsole.Write("Hotel id: ");
        var hotelId = Console.ReadLine();
            
        AnsiConsole.Write("From (yyyy-mm-dd): ");
        var fromDate = Console.ReadLine();
            
        AnsiConsole.Write("To (yyyy-mm-dd): ");
        var toDate = Console.ReadLine();
            
        AnsiConsole.Write("Room type: ");
        var roomType = Console.ReadLine();

        try
        {
            var bookingRequest = new BookingRequest(
                hotelId!,
                DateOnly.Parse(fromDate!),
                DateOnly.Parse(toDate!),
                Enum.Parse<RoomTypeCode>(roomType!));

            var hotelResponse =
                _queryService.GetResponse(bookingRequest, hotelsData, bookingsData).ToList();

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

    private static bool IsValidArguments(string[] args, ref string hotelsPath, ref string bookingsPath)
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
                if (args[i] == HotelsCommand)
                {
                    hotelsPath = args[i + 1];
                }

                if (args[i] == BookingsCommand)
                {
                    bookingsPath = args[i + 1];
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

    private bool IsValidPaths(
        string hotelsPath,
        string bookingsPath,
        ref List<Hotel> hotelsData, 
        ref List<Core.Models.Booking> bookingsData)
    {
        
        try
        {
            hotelsData = _dataService.GetData<Hotel>(hotelsPath)!.ToList();
            bookingsData = _dataService.GetData<Core.Models.Booking>(bookingsPath)!.ToList();
            return true;
        }
        catch (Exception exception)
        {
            AnsiConsole.MarkupLine("[red]Invalid path to data.[/] Please try again with valid data.");
            AnsiConsole.WriteLine(exception.Message);
            return false;
        }
    }
}