using Booking.Core.Models;

namespace Booking.Core.Abstracts;

public interface IDataService
{
    public IEnumerable<T>? GetData<T>(string path) where T : class;

    public IEnumerable<HotelResponse> GetResponse(
        BookingRequest request,
        IEnumerable<Hotel> hotels,
        IEnumerable<Models.Booking> bookings);

}