using Booking.Core.Enums;

namespace Booking.Core.Models;

public class RoomType
{
    public RoomTypeCode Code { get; set; }

    public string? Description { get; set; }

    public IEnumerable<string> Amenities { get; set; } = [];

    public IEnumerable<string> Features { get; set; } = [];
}