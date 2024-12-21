using System.Text.Json;
using Booking.Core.Abstracts;

namespace Booking.Core.Serializers;

public class JsonSerializer : IJsonSerializer
{
    public string Serialize(Stream stream, JsonSerializerOptions? options = null)
    {
        return System.Text.Json.JsonSerializer.Serialize(stream, options);
    }

    public T? Deserialize<T>(Stream stream, JsonSerializerOptions? options = null)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(stream, options);
    }
}