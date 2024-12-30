using System.Collections;

namespace Booking.Core.Tests.FakeData;

public class FakeJsonInvalidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [DataHelper.NotJsonEmpty];
        yield return [DataHelper.NotJson];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}