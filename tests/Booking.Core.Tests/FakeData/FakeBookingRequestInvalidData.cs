using System.Collections;
using Booking.Core.Enums;
using Booking.Core.Models;

namespace Booking.Core.Tests.FakeData;

public class FakeBookingRequestInvalidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return 
            [
                new BookingRequest(
                    "H1",
                    new DateOnly(2024, 9, 1), 
                    new DateOnly(2024, 9, 4), 
                    RoomTypeCode.DBL)
            ];
        
        yield return 
            [
                new BookingRequest(
                    "H1",
                    new DateOnly(2024, 9, 2), 
                    new DateOnly(2024, 9, 6), 
                    RoomTypeCode.SGL)
            ];
        
        yield return 
            [
                new BookingRequest(
                    "H2",
                    new DateOnly(2024, 9, 1), 
                    new DateOnly(2024, 9, 3), 
                    RoomTypeCode.SGL)
            ];
        
        yield return 
            [
                new BookingRequest(
                    "H1",
                    null, 
                    null, 
                    RoomTypeCode.SGL)
            ];
        
        yield return 
        [
            new BookingRequest(
                "H2",
                new DateOnly(2024, 9, 1), 
                null, 
                RoomTypeCode.DBL)
        ];
        
        yield return 
        [
            new BookingRequest(
                "H2",
                null, 
                new DateOnly(2025, 9, 2), 
                RoomTypeCode.SGL)
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}