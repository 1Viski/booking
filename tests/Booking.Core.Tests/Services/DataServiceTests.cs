using System.Text.Json;
using Booking.Core.Abstracts;
using Booking.Core.Enums;
using Booking.Core.Models;
using Booking.Core.Services;
using FluentAssertions;
using Moq;

namespace Booking.Core.Tests.Services;

public class DataServiceTests
{
    [Fact]
    public void GetDataTestGenericHotelClassShouldNotBeNullOrEmpty()
    {
        //Arrange
        
        //lang=JSON
        const string jsonHotels = """
                                  [
                                    {
                                      "id": "H1",
                                      "name": "Hotel California",
                                      "roomTypes": [
                                        {
                                          "code": "SGL",
                                          "description": "Single Room",
                                          "amenities": ["WiFi", "TV"],
                                          "features": ["Non-smoking"]
                                        },
                                        {
                                          "code": "DBL",
                                          "description": "Double Room",
                                          "amenities": ["WiFi", "TV", "Minibar"],
                                          "features": ["Non-smoking", "Sea View"]
                                        }
                                      ],
                                      "rooms": [
                                        {
                                          "roomType": "SGL",
                                          "roomId": "101"
                                        },
                                        {
                                          "roomType": "SGL",
                                          "roomId": "102"
                                        },
                                        {
                                          "roomType": "DBL",
                                          "roomId": "201"
                                        },
                                        {
                                          "roomType": "DBL",
                                          "roomId": "202"
                                        }
                                      ]
                                    }
                                  ] 
                                  """;

        var mockFileStream = new Mock<IStream>();
        
        mockFileStream
            .Setup(x => x.GetStream(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
            .Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonHotels)));

        //Act
        var dataService = new DataService(mockFileStream.Object);
        var result = dataService.GetData<Hotel>(It.IsAny<string>());
        
        //Assert
        result
            .Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.ContainItemsAssignableTo<Hotel>();
    }

    [Fact]
    public void GetDataTestBookingClassShouldNotBeNullOrEmpty()
    {
        //Arrange
        
        //Act
        
        //Arrange
    }
}