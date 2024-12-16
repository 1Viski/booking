namespace Booking.Core.Abstracts;

public interface IDataService
{
    public IEnumerable<T>? GetData<T>(string path) where T : class;
}