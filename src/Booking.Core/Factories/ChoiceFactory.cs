using Booking.Core.Abstracts;

namespace Booking.Core.Factories;

public class ChoiceFactory : IChoiceFactory
{
    private readonly IDataService _dataService;

    public ChoiceFactory(IDataService dataService)
    {
        _dataService = dataService;
    }

    public BaseChoice CreateChoice(string choice)
    {
        return (BaseChoice)Activator.CreateInstance(Type.GetType($"Booking.Core.Choices.{choice}Choice")!, _dataService)!;
    }
}