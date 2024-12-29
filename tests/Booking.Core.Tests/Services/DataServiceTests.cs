using System.Text.Json;
using Booking.Core.Abstracts;
using Booking.Core.Models;
using Booking.Core.Services;
using Booking.Core.Tests.FakeData;
using FluentAssertions;
using Moq;

namespace Booking.Core.Tests.Services;

public class DataServiceTests
{
    private readonly Mock<IStream> _mockFileStream = new();

    [Fact]
    public void GetDataTestGenericHotelClassShouldNotBeNullOrEmpty()
    {
        //Arrange
        _mockFileStream
            .Setup(x => x.GetStream(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
            .Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(DataHelper.JsonHotels)));

        //Act
        var dataService = new DataService(_mockFileStream.Object);
        var result = dataService.GetData<Hotel>(It.IsAny<string>());
        
        //Assert
        result
            .Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.ContainItemsAssignableTo<Hotel>();
    }

    [Fact]
    public void GetDataTestGenericBookingClassShouldNotBeNullOrEmpty()
    {
        //Arrange
        _mockFileStream
            .Setup(x => x.GetStream(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
            .Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(DataHelper.JsonBookings)));
        
        //Act
        var dataService = new DataService(_mockFileStream.Object);
        var result = dataService.GetData<Models.Booking>(It.IsAny<string>());
        
        //Assert
        result
            .Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.ContainItemsAssignableTo<Models.Booking>();
    }

    [Theory]
    [ClassData(typeof(FakeJsonInvalidData))]
    public void GetDataTestGenericHotelClassShouldThrowJsonException(string json)
    {
        //Arrange
        _mockFileStream
            .Setup(x => x.GetStream(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
            .Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
        
        //Act
        var dataService = new DataService(_mockFileStream.Object);
        var func = () => dataService.GetData<Hotel>(It.IsAny<string>());
        
        //Assert
        func.Should().Throw<JsonException>();
    }

    [Theory]
    [ClassData(typeof(FakeJsonInvalidData))]
    public void GetDataTestGenericBookingClassShouldThrowJsonException(string json)
    {
        //Arrange
        _mockFileStream
            .Setup(x => x.GetStream(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
            .Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
        
        //Act
        var dataService = new DataService(_mockFileStream.Object);
        var func = () => dataService.GetData<Models.Booking>(It.IsAny<string>());
        
        //Assert
        func.Should().Throw<JsonException>();
    }

    [Theory]
    [ClassData(typeof(FakeBookingRequestValidData))]
    public void GetResponseTestShouldNotBeNull(BookingRequest request)
    {
        //Arrange
        var expected = DataHelper.GetHotelResponses().ToList();
        
        //Act
        var dataService = new DataService(_mockFileStream.Object);
        
        var actual = dataService.GetResponse(
            request,
            DataHelper.GetValidHotels(), 
            DataHelper.GetValidBookings());
        
        var responses = actual.ToList();
        var rooms = responses[0].Rooms;

        //Assert
        responses
            .Should().NotBeNull()
            .And.HaveCount(1)
            .And.ContainItemsAssignableTo<HotelResponse>()
            .And.BeEquivalentTo(expected);

        rooms
            .Should().NotBeNull()
            .And.HaveCount(2)
            .And.ContainItemsAssignableTo<Room>()
            .And.BeEquivalentTo(expected[0].Rooms);
    }

    [Theory]
    [ClassData(typeof(FakeBookingRequestInvalidData))]
    public void GetResponseTestShouldBeNull(BookingRequest request)
    {
        //Arrange
        
        //Act
        var dataService = new DataService(_mockFileStream.Object);
        
        var actual = dataService.GetResponse(
            request,
            DataHelper.GetValidHotels(),
            DataHelper.GetValidBookings());
        
        //Assert
        actual.Should().BeEmpty();
    }
}