using Booking.Core.Enums;

namespace Booking.Core.Models;

public record HotelResponse(
    string HotelId,
    string HotelName,
    DateOnly Arrival,
    DateOnly Departure,
    RoomTypeCode RoomType,
    RoomRate RoomRate,
    IEnumerable<Room> Rooms);