using Booking.Core.Abstracts;

namespace Booking.Core.IO;

public class FileStreamIO : IStream
{
    public Stream GetStream(
        string path, 
        FileMode mode = FileMode.Open, 
        FileAccess access = FileAccess.Read,
        FileShare share = FileShare.Read)
    {
        return new FileStream(path, mode, access, share);
    }
}