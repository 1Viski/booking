using Booking.Core.Enums;

namespace Booking.Core.Models;

public class Room
{
    public string RoomId { get; set; } = null!;

    public RoomTypeCode RoomType { get; set; }
}