namespace Booking.Core.Models;

public class Hotel
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public IEnumerable<RoomType> RoomTypes { get; set; } = [];

    public IEnumerable<Room> Rooms { get; set; } = [];
}