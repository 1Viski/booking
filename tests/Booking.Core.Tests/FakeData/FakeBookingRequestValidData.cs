using System.Collections;
using Booking.Core.Enums;
using Booking.Core.Models;

namespace Booking.Core.Tests.FakeData;

public class FakeBookingRequestValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [GetValid()];
        yield return [GetValidFromDateOnly()];
        yield return [GetValidToDateOnly()];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private static BookingRequest GetValid()
    {
        var request = new BookingRequest(
            "H1",
            new DateOnly(2024, 9, 1), 
            new DateOnly(2024, 9, 3), 
            RoomTypeCode.DBL);
        
        return request; 
    }

    private static BookingRequest GetValidFromDateOnly()
    {
        var request = new BookingRequest(
            "H1",
            new DateOnly(2024, 9, 1), 
            null, 
            RoomTypeCode.DBL);
        
        return request;
    }
    
    private static BookingRequest GetValidToDateOnly()
    {
        var request = new BookingRequest(
            "H1",
            null, 
            new DateOnly(2024, 9, 3), 
            RoomTypeCode.DBL);
        
        return request; 
    }
}