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
        var mockJsonSerializer = new Mock<IJsonSerializer>();
        
        mockFileStream
            .Setup(x => x.GetStream(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
            .Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonHotels)));

        mockJsonSerializer
            .Setup(x => x.Deserialize<IEnumerable<Hotel>>(
                It.IsAny<Stream>(), It.IsAny<JsonSerializerOptions?>()))
            .Returns(
                [
                    new Hotel
                    {
                        Id = "H1",
                        Name = "Hotel California",
                        RoomTypes = 
                        [
                            new RoomType
                            {
                                Code = RoomTypeCode.SGL,
                                Description = "Single Room",
                                Amenities = ["WiFi", "TV"],
                                Features = ["Non-smoking"]
                            },
                            new RoomType
                            {
                                Code = RoomTypeCode.SGL,
                                Description = "Double Room",
                                Amenities = ["WiFi", "TV", "Minibar"],
                                Features = ["Non-smoking", "Sea View"]
                            }
                        ],
                        Rooms =
                        [
                            new Room
                            {
                                RoomType = RoomTypeCode.SGL,
                                RoomId = "101",
                            },
                            new Room
                            {
                                RoomType = RoomTypeCode.SGL,
                                RoomId = "102",
                            },
                            new Room
                            {
                                RoomType = RoomTypeCode.SGL,
                                RoomId = "201",
                            },
                            new Room
                            {
                                RoomType = RoomTypeCode.SGL,
                                RoomId = "202",
                            },
                        ]
                    }
                ]);

        //Act
        var dataService = new DataService(mockFileStream.Object, mockJsonSerializer.Object);
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