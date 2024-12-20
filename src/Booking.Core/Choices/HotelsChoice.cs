using Booking.Core.Abstracts;
using Booking.Core.Models;
using Spectre.Console;

namespace Booking.Core.Choices;

public class HotelsChoice : BaseChoice
{
    public HotelsChoice(IDataService dataService) : base(dataService) { }

    public override void OnChoice(string hotelsPath, string bookingsPath)
    {
        List<Hotel> hotelsData = [];
        
        if (!TryGetData(hotelsPath, ref hotelsData))
        {
            return;
        }
        
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
}