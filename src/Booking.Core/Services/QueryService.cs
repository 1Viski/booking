using Booking.Core.Abstracts;
using Booking.Core.Models;

namespace Booking.Core.Services;

public class QueryService : IQueryService
{
    public IEnumerable<HotelResponse> GetResponse(
        BookingRequest request, 
        IEnumerable<Hotel> hotels, 
        IEnumerable<Models.Booking> bookings)
    {
        var response = hotels
            .Join(
                bookings, 
                hotel => hotel.Id, 
                booking => booking.HotelId, 
                (hotel, booking) => 
                    new HotelResponse(
                        hotel.Id,
                        hotel.Name!,
                        booking.Arrival,
                        booking.Departure,
                        booking.RoomType,
                        booking.RoomRate,
                        hotel.Rooms
                            .Where(room => room.RoomType == request.RoomType).ToList()))
            .Where(response => 
                response.HotelId == request.HotelId &&
                response.RoomType == request.RoomType &&
                response.Arrival <= request.FromDate &&
                response.Departure >= request.ToDate);

        return response;
    }
}