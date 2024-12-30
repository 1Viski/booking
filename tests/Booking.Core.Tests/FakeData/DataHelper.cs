using Booking.Core.Enums;
using Booking.Core.Models;

namespace Booking.Core.Tests.FakeData;

public static class DataHelper
{
    public static string JsonHotels { get; private set; } =
        //lang=JSON
        """
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

    public static string JsonBookings { get; private set; } =
        //lang=JSON
        """
        [
          {
            "hotelId": "H1",
            "arrival": "2024-09-01",
            "departure": "2024-09-03",
            "roomType": "DBL",
            "roomRate": "Prepaid"
          },
          {
            "hotelId": "H1",
            "arrival": "2024-09-02",
            "departure": "2024-09-05",
            "roomType": "SGL",
            "roomRate": "Standard"
          }
        ]
        """;

    public static string NotJsonEmpty { get; private set; } = "";

    public static string JsonEmptyCollection { get; private set; } = "[]";

    public static string JsonEmptyCollectionObject { get; private set; } = "[{}]";

    public static string NotJson { get; private set; } = 
        """
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore 
        magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo 
        consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla 
        pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est 
        laborum.
        """;

    public static IEnumerable<Hotel> GetValidHotels()
    {
        Hotel[] hotels = 
            [
                new()
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
                                Code = RoomTypeCode.DBL,
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
                                RoomId = "101"
                            },
                            new Room
                            {
                                RoomType = RoomTypeCode.SGL,
                                RoomId = "101"
                            },
                            new Room
                            {
                                RoomType = RoomTypeCode.DBL,
                                RoomId = "201"
                            },
                            new Room
                            {
                                RoomType = RoomTypeCode.DBL,
                                RoomId = "202"
                            }
                        ]
                }
            ];
        
        return hotels;
    }
    
    public static IEnumerable<Models.Booking> GetValidBookings()
    {
        Models.Booking[] bookings =
            [   
                new()
                {
                    HotelId = "H1",
                    Arrival = new DateOnly(2024, 9, 1),
                    Departure = new DateOnly(2024, 9, 3),
                    RoomType = RoomTypeCode.DBL,
                    RoomRate = RoomRate.Prepaid
                },
                new()
                {
                    HotelId = "H1",
                    Arrival = new DateOnly(2024, 9, 2),
                    Departure = new DateOnly(2024, 9, 5),
                    RoomType = RoomTypeCode.SGL,
                    RoomRate = RoomRate.Standard
                }
            ];
        
        return bookings;
    }

    public static IEnumerable<HotelResponse> GetHotelResponses()
    {
        HotelResponse[] responses =
            [
                new(
                    "H1", 
                    "Hotel California", 
                    new DateOnly(2024, 9, 1), 
                    new DateOnly(2024, 9, 3),
                    RoomTypeCode.DBL,
                    RoomRate.Prepaid,
                    [
                        new Room
                        {
                            RoomType = RoomTypeCode.DBL,
                            RoomId = "201"
                        },
                        new Room
                        {
                            RoomType = RoomTypeCode.DBL,
                            RoomId = "202"
                        }
                    ])
            ];

        return responses;
    }
}