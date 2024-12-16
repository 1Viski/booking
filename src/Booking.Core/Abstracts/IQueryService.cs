using Booking.Core.Models;

namespace Booking.Core.Abstracts;

public interface IQueryService
{
    public IEnumerable<HotelResponse> GetResponse(
        BookingRequest request, 
        IEnumerable<Hotel> hotels, 
        IEnumerable<Models.Booking> bookings);
}