using System.Text.Json;
using System.Text.Json.Serialization;
using Booking.Core.Abstracts;
using Booking.Core.Enums;

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
}