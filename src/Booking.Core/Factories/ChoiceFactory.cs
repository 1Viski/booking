using Booking.Core.Abstracts;

namespace Booking.Core.Factories;

public class ChoiceFactory : IChoiceFactory
{
    private readonly IDataService _dataService;
    private readonly IConsole _console;

    public ChoiceFactory(IDataService dataService, IConsole console)
    {
        _dataService = dataService;
        _console = console;
    }

    public BaseChoice CreateChoice(string choice)
    {
        return (BaseChoice)Activator.CreateInstance(
            Type.GetType($"Booking.Core.Choices.{choice}Choice")!, 
            _dataService,
            _console)!;
    }
}