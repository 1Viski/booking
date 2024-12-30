using Booking.Core.Abstracts;
using Booking.Core.Choices;
using Booking.Core.Models;
using Booking.Core.Tests.FakeData;
using Moq;
using Spectre.Console;

namespace Booking.Core.Tests.Choices;

public class HotelsChoiceTests
{
    private readonly Mock<IDataService> _mockDataService = new();
    private readonly Mock<IConsole> _mockConsole = new();

    [Fact]
    public void OnChoiceTest()
    {
        //Arrange
        _mockDataService
            .Setup(x => x.GetData<Hotel>(It.IsAny<string>()))
            .Returns(DataHelper.GetValidHotels);
        
        var expectedTable = Creator.CreateHotelsTable(DataHelper.GetValidHotels().ToList());
        
        //Act
        var hotelsChoice = new HotelsChoice(_mockDataService.Object, _mockConsole.Object);
        hotelsChoice.OnChoice(It.IsAny<string>(), It.IsAny<string>());

        //Assert
        _mockConsole.Verify(
            c => c.WriteLine(It.Is<Table>(t => 
                t.Title!.Text == expectedTable.Title!.Text &&
                t.Title.Style!.Foreground == expectedTable.Title.Style!.Foreground &&
                t.Border == expectedTable.Border &&
                t.BorderStyle!.Foreground == expectedTable.BorderStyle!.Foreground &&
                t.Rows.Count == expectedTable.Rows.Count &&
                t.Columns.Count == expectedTable.Columns.Count)), 
            Times.Once);
    }
}