using Booking.Core.Abstracts;
using Booking.Core.Factories;
using FluentAssertions;
using Moq;

namespace Booking.Core.Tests.Factories;

public class ChoiceFactoryTests
{
    private readonly Mock<IDataService> _mockDataService = new();
    private readonly Mock<IConsole> _mockConsole = new();
    
    [Theory]
    [InlineData("Hotels")]
    [InlineData("Availability")]
    public void CreateChoiceTestShouldNotBeNull(string choiceName)
    {
        //Arrange

        //Act
        var factory = new ChoiceFactory(_mockDataService.Object, _mockConsole.Object);
        var choice = factory.CreateChoice(choiceName);

        //Assert
        choice
            .Should().NotBeNull()
            .And.BeAssignableTo<BaseChoice>();
    }

    [Theory]
    [InlineData("Hotel")]
    [InlineData("Availabilities")]
    [InlineData("Menu")]
    [InlineData(null)]
    public void CreateChoiceTestShouldThrowArgumentException(string choiceName)
    {
        //Arrange

        //Act
        var factory = new ChoiceFactory(_mockDataService.Object, _mockConsole.Object);
        var func = () => factory.CreateChoice(choiceName);

        //Assert
        func.Should().Throw<ArgumentException>();
    }
}