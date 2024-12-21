namespace Booking.Core.Abstracts;

public interface IStream
{
    public Stream GetStream(
        string path, 
        FileMode mode = FileMode.Open, 
        FileAccess access = FileAccess.Read, 
        FileShare share = FileShare.Read);
}