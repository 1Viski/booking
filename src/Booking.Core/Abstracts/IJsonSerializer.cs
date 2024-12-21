using System.Text.Json;

namespace Booking.Core.Abstracts;

public interface IJsonSerializer
{
    public string Serialize(Stream stream, JsonSerializerOptions? options = null);
    
    public T? Deserialize<T>(Stream stream, JsonSerializerOptions? options = null);
}