using Booking.Core.Enums;

namespace Booking.Core.Models;

public class Booking
{
    public string HotelId { get; set; } = null!;

    public DateOnly Arrival { get; set; }

    public DateOnly Departure { get; set; }

    public RoomTypeCode RoomType { get; set; }

    public RoomRate RoomRate { get; set; }
}