using Booking.Core.Models;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Booking.Core;

public static class Creator
{
    public static Table CreateHotelsTable(List<Hotel> hotels)
    {
        var table = new Table();
        var titleStyle = new Style(foreground: Color.Blue);
        table.Title = new TableTitle("Hotels", titleStyle);
        table.Border(TableBorder.Square);
        table.BorderColor(Color.Blue);
        table.AddColumns("Id", "Name", "Room types");

        foreach (var hotel in hotels)
        {
            var roomTypes = GetRoomTypes(hotel.RoomTypes.ToArray());
            var panelRoomTypes = new Panel(roomTypes);
            panelRoomTypes.BorderColor(Color.Gold1);
            panelRoomTypes.Border = BoxBorder.Square;
            table.AddRow(new Markup(hotel.Id), new Markup(hotel.Name!), panelRoomTypes);
        }

        return table;
    }

    public static Table CreateRoomsTable(List<HotelResponse> hotelResponse)
    {
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

        return table;
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