using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Core.Models.Responses;
using Core.Processors;
using SpecTests.Helpers;

namespace SpecTests
{
    public class AvailabilitySpec
    {
        public class GetAvailabilityBase : IAsyncLifetime
        {
            protected AvailabilityRequest Request;
            protected AvailabilityResponse Response;

            protected DatabaseMockBuilder DbMockBuilder = new();
            protected Mock<ICarParkRepository> DatabaseMock;

            public async Task InitializeAsync()
            {
                DatabaseMock = DbMockBuilder.Build();
                Response = await new AvailabilityProcessor(DatabaseMock.Object).GetAvailabilityForDates(Request);
            }

            public Task DisposeAsync()
                => Task.CompletedTask;
        }

        public class WhenSpaceIsAvailable : GetAvailabilityBase
        {
            private readonly ParkingSpace _parkingSpace;
            public WhenSpaceIsAvailable()
            {
                _parkingSpace = RandomData.ParkingSpace();
                DbMockBuilder.AddParkingSpace(_parkingSpace);
                Request = RandomData.AvailabilityRequest(startDate: DateTime.Parse("2023-09-04"), endDate: DateTime.Parse("2023-09-06"));
            }

            [Fact]
            public void ParkingSpacesAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllParkingSpaces(), Times.Once);

            [Fact]
            public void BookingsAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllBookings(), Times.Once);

            [Fact]
            public void ReturnedAvailabilityIsCorrect()
            {
                Response.StartDate.Should().Be(Request.StartDate.Date);
                Response.EndDate.Should().Be(Request.EndDate.Date);
                Response.Price.Should().Be(10m);
                Response.AvailableSpaces.Should().HaveCount(1);
                Response.AvailableSpaces.Should().ContainEquivalentOf(_parkingSpace);
            }
        }

        public class WhenSpaceIsNotAvailable : GetAvailabilityBase
        {
            private readonly ParkingSpace _parkingSpace;
            public WhenSpaceIsNotAvailable()
            {
                _parkingSpace = RandomData.ParkingSpace();
                DbMockBuilder.AddParkingSpace(_parkingSpace);
                DbMockBuilder.AddBooking(RandomData.Booking(startDate:DateTime.Parse("2023-09-02"),endDate:DateTime.Parse("2023-09-08"),parkingSpaceId:_parkingSpace.ParkingSpaceId));
                Request = RandomData.AvailabilityRequest(startDate: DateTime.Parse("2023-09-04"), endDate: DateTime.Parse("2023-09-06"));
            }

            [Fact]
            public void ParkingSpacesAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllParkingSpaces(), Times.Once);

            [Fact]
            public void BookingsAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllBookings(), Times.Once);

            [Fact]
            public void ReturnedAvailabilityIsCorrect()
            {
                Response.StartDate.Should().Be(Request.StartDate.Date);
                Response.EndDate.Should().Be(Request.EndDate.Date);
                Response.Price.Should().Be(10m);
                Response.AvailableSpaces.Should().HaveCount(0);
            }
        }

        public class WhenMultipleSpacesAreAvailable : GetAvailabilityBase
        {
            private readonly List<ParkingSpace> _parkingSpaces = new();
            public WhenMultipleSpacesAreAvailable()
            {
                for (int i = 0; i < RandomData.Number(5,20); i++)
                {
                    var randomParkingSpace = RandomData.ParkingSpace();
                    _parkingSpaces.Add(randomParkingSpace);
                    DbMockBuilder.AddParkingSpace(randomParkingSpace);
                }
                Request = RandomData.AvailabilityRequest(startDate: DateTime.Parse("2023-09-04"), endDate: DateTime.Parse("2023-09-06"));
            }

            [Fact]
            public void ParkingSpacesAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllParkingSpaces(), Times.Once);

            [Fact]
            public void BookingsAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllBookings(), Times.Once);

            [Fact]
            public void ReturnedAvailabilityIsCorrect()
            {
                Response.StartDate.Should().Be(Request.StartDate.Date);
                Response.EndDate.Should().Be(Request.EndDate.Date);
                Response.Price.Should().Be(10m);
                Response.AvailableSpaces.Should().HaveCount(_parkingSpaces.Count);
                Response.AvailableSpaces.Should().BeEquivalentTo(_parkingSpaces);
            }
        }
    }
}
