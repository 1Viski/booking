using Booking.Core.Abstracts;
using Booking.Core.Choices;
using Booking.Core.Models;
using Booking.Core.Tests.FakeData;
using Moq;
using Spectre.Console;

namespace Booking.Core.Tests.Choices;

public class AvailabilityChoiceTests
{
    private readonly Mock<IDataService> _mockDataService = new();
    private readonly Mock<IConsole> _mockConsole = new();
    
    [Theory]
    [ClassData(typeof(FakeBookingRequestValidData))]
    public void OnChoiceTestValidDataShouldBeAvailable(BookingRequest request)
    {
        //Arrange
        _mockDataService
            .Setup(x => x.GetResponse(
                request,
                It.IsAny<IEnumerable<Hotel>>(),
                It.IsAny<IEnumerable<Models.Booking>>()))
            .Returns(DataHelper.GetHotelResponses);

        _mockConsole
            .SetupSequence(x => x.ReadLine())
            .Returns(request.HotelId)
            .Returns(() => request.FromDate is null ? string.Empty : request.FromDate.ToString())
            .Returns(() => request.ToDate is null ? string.Empty : request.ToDate.ToString())
            .Returns(request.RoomType.ToString);

        var expectedTable = Creator.CreateRoomsTable(DataHelper.GetHotelResponses().ToList());

        //Act
        var availabilityChoice = new AvailabilityChoice(_mockDataService.Object, _mockConsole.Object);
        availabilityChoice.OnChoice(It.IsAny<string>(), It.IsAny<string>());

        //Assert
        VerifyType(_mockConsole);
        _mockConsole.Verify(c => c.MarkupLine("[red]No available hotels found.[/]"), Times.Never);

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

    [Theory]
    [ClassData(typeof(FakeBookingRequestInvalidData))]
    public void OnChoiceTestInvalidDataShouldNotBeAvailable(BookingRequest request)
    {
        //Arrange
        _mockConsole
            .SetupSequence(x => x.ReadLine())
            .Returns(request.HotelId)
            .Returns(() => request.FromDate is null ? string.Empty : request.FromDate.ToString())
            .Returns(() => request.ToDate is null ? string.Empty : request.ToDate.ToString())
            .Returns(request.RoomType.ToString);

        //Act
        var availability = new AvailabilityChoice(_mockDataService.Object, _mockConsole.Object);
        availability.OnChoice(It.IsAny<string>(), It.IsAny<string>());

        //Assert
        VerifyType(_mockConsole);
        _mockConsole.Verify(c => c.MarkupLine("[red]No available hotels found.[/]"), Times.Once);
    }

    [Theory]
    [InlineData("20240901!", "20240903!")]
    [InlineData("", "")]
    public void OnChoiceTestInvalidFormatDate(string fromDate, string toDate)
    {
        //Arrange
        _mockConsole
            .SetupSequence(x => x.ReadLine())
            .Returns(It.IsAny<string>())
            .Returns(fromDate)
            .Returns(toDate)
            .Returns(It.IsAny<string>());

        //Act
        var availability = new AvailabilityChoice(_mockDataService.Object, _mockConsole.Object);
        availability.OnChoice(It.IsAny<string>(), It.IsAny<string>());

        //Assert
        VerifyType(_mockConsole);
        _mockConsole.Verify(c => c.MarkupLine("[red]Invalid format of date[/]. Please try again."), Times.Once);
    }
    
    [Theory]
    [InlineData("qwe")]
    [InlineData("ASD")]
    [InlineData("zxc")]
    public void OnChoiceTestInvalidEnum(string roomType)
    {
        //Arrange
        _mockConsole
            .SetupSequence(x => x.ReadLine())
            .Returns(It.IsAny<string>())
            .Returns("2024-09-01")
            .Returns("2024-09-03")
            .Returns(roomType);

        //Act
        var availability = new AvailabilityChoice(_mockDataService.Object, _mockConsole.Object);
        availability.OnChoice(It.IsAny<string>(), It.IsAny<string>());

        //Assert
        VerifyType(_mockConsole);
        _mockConsole.Verify(c => c.MarkupLine("[red]Invalid type of room[/]. Please try again."), Times.Once);
    }

    private static void VerifyType(Mock<IConsole> mockConsole)
    {
        mockConsole.Verify(c => c.Write("Hotel id: "), Times.Once);
        mockConsole.Verify(c => c.Write("From (yyyy-mm-dd): "), Times.Once);
        mockConsole.Verify(c => c.Write("To (yyyy-mm-dd): "), Times.Once);
        mockConsole.Verify(c => c.Write("Room type: "), Times.Once);
        mockConsole.Verify(c => c.ReadLine(), Times.Exactly(4));
    }
}