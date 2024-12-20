using System.Text.Json;
using System.Text.Json.Serialization;
using Booking.Core.Abstracts;
using Booking.Core.Enums;
using Booking.Core.Models;

namespace Booking.Core.Services;

public class DataService : IDataService
{
    public IEnumerable<T>? GetData<T>(string path) where T : class
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter<RoomTypeCode>(),
                new JsonStringEnumConverter<RoomRate>()
            }
        };

        using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var data = JsonSerializer.Deserialize<IEnumerable<T>>(fileStream, jsonOptions);
        return data;
    }
    
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
            .Where(response => AvailableStatement(response, request));

        return response;
    }

    private static bool AvailableStatement(HotelResponse response, BookingRequest request)
    {
        var isAvailableData = false;
        
        if (request.FromDate is null && request.ToDate is not null)
        {
            isAvailableData = response.Arrival <= request.ToDate && response.Departure >= request.ToDate;
        }
        
        if (request.FromDate is not null && request.ToDate is null)
        {
            isAvailableData = response.Arrival <= request.FromDate && response.Departure >= request.FromDate;
        }

        if (request.FromDate is not null && request.ToDate is not null)
        {
            isAvailableData = response.Arrival <= request.FromDate && response.Departure >= request.ToDate;
        }

        return response.HotelId == request.HotelId && response.RoomType == request.RoomType && isAvailableData;
    }
}