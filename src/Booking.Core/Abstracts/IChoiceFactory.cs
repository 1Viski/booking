namespace Booking.Core.Abstracts;

public interface IChoiceFactory
{
    public BaseChoice CreateChoice(string choice);
}