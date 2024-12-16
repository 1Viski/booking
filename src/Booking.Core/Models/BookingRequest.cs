using Booking.Core.Enums;

namespace Booking.Core.Models;

public record BookingRequest(
    string HotelId,
    DateOnly FromDate,
    DateOnly ToDate,
    RoomTypeCode RoomType);