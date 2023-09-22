using Core;
using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Core.Models.Responses;
using Core.Processors;
using SpecTests.Helpers;

namespace SpecTests
{
    public class BookingSpec
    {
        public class CreateBookingBase : IAsyncLifetime
        {
            protected CreateBookingRequest Request;
            protected BookingResponse Response;
            protected string UnsuccessfulMessage;

            protected DatabaseMockBuilder DbMockBuilder = new();
            protected Mock<ICarParkRepository> DatabaseMock;

            public async Task InitializeAsync()
            {
                DatabaseMock = DbMockBuilder.Build();
                await new BookingProcessor(DatabaseMock.Object)
                    .CreateBooking(
                        Request,
                        onSuccess: booking => Response = booking,
                        onNoAvailability: message => UnsuccessfulMessage = message
                );
            }

            public Task DisposeAsync()
                => Task.CompletedTask;
        }

        public class WhenCreateBookingRequestIsValid : CreateBookingBase
        {
            private readonly ParkingSpace _parkingSpace;
            public WhenCreateBookingRequestIsValid()
            {
                _parkingSpace = RandomData.ParkingSpace();
                DbMockBuilder.AddParkingSpace(_parkingSpace);
                Request = RandomData.CreateBookingRequest();
            }

            [Fact]
            public void ParkingSpacesAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllParkingSpaces(), Times.Once);

            [Fact]
            public void BookingsAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllBookings(), Times.Once);

            [Fact]
            public void BookingIsCreated()
                => DatabaseMock.Verify(d => d.CreateBooking(It.IsAny<Booking>()), Times.Once);

            [Fact]
            public void ReturnedBookingIsCorrect()
            {
                Response.BookingId.Should().NotBeEmpty();
                Response.StartDate.Should().Be(Request.StartDate.Date);
                Response.EndDate.Should().Be(Request.EndDate.Date);
                Response.BayIdentifier.Should().Be(_parkingSpace.BayIdentifier);
                Response.BayWidthInMetres.Should().Be(_parkingSpace.Width);
            }

            [Fact]
            public void UnsuccessfulMessageIsNotReturned()
                => UnsuccessfulMessage.Should().BeNull();
        }

        public class WhenCreateBookingRequestIsValidWithBookingsAlreadyPresent : CreateBookingBase
        {
            private readonly ParkingSpace _parkingSpace;
            public WhenCreateBookingRequestIsValidWithBookingsAlreadyPresent()
            {
                _parkingSpace = RandomData.ParkingSpace();
                DbMockBuilder.AddParkingSpace(_parkingSpace);
                Request = RandomData.CreateBookingRequest(startDate: DateHelper.AddDaysToToday(0), endDate: DateHelper.AddDaysToToday(3));

                DbMockBuilder.AddBooking(RandomData.Booking(startDate: DateHelper.AddDaysToToday(-3), endDate: DateHelper.AddDaysToToday(-1), parkingSpaceId: _parkingSpace.ParkingSpaceId));
                DbMockBuilder.AddBooking(RandomData.Booking(startDate: DateHelper.AddDaysToToday(4), endDate:DateHelper.AddDaysToToday(7), parkingSpaceId: _parkingSpace.ParkingSpaceId));
            }

            [Fact]
            public void ParkingSpacesAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllParkingSpaces(), Times.Once);

            [Fact]
            public void BookingsAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllBookings(), Times.Once);

            [Fact]
            public void BookingIsCreated()
                => DatabaseMock.Verify(d => d.CreateBooking(It.IsAny<Booking>()), Times.Once);

            [Fact]
            public void ReturnedBookingIsCorrect()
            {
                Response.BookingId.Should().NotBeEmpty();
                Response.StartDate.Should().Be(Request.StartDate.Date);
                Response.EndDate.Should().Be(Request.EndDate.Date);
                Response.BayIdentifier.Should().Be(_parkingSpace.BayIdentifier);
                Response.BayWidthInMetres.Should().Be(_parkingSpace.Width);
            }

            [Fact]
            public void UnsuccessfulMessageIsNotReturned()
                => UnsuccessfulMessage.Should().BeNull();
        }

        public class WhenCreateBookingRequestIsInvalid : CreateBookingBase
        {
            public WhenCreateBookingRequestIsInvalid()
            {
                var parkingSpace = RandomData.ParkingSpace();
                DbMockBuilder.AddParkingSpace(parkingSpace);
                DbMockBuilder.AddBooking(RandomData.Booking(startDate: DateHelper.AddDaysToToday(-2), endDate: DateHelper.AddDaysToToday(3), parkingSpaceId:parkingSpace.ParkingSpaceId));

                Request = RandomData.CreateBookingRequest(startDate: DateHelper.AddDaysToToday(-1), endDate: DateTime.Now.AddDays(1));
            }

            [Fact]
            public void ParkingSpacesAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllParkingSpaces(), Times.Once);

            [Fact]
            public void BookingsAreRetrieved()
                => DatabaseMock.Verify(d => d.GetAllBookings(), Times.Once);

            [Fact]
            public void BookingIsNotCreated()
                => DatabaseMock.Verify(d => d.CreateBooking(It.IsAny<Booking>()), Times.Never);

            [Fact]
            public void MessageIsReturned() 
                => UnsuccessfulMessage.Should().Be(Constants.Messages.NoSpacesAvailable);
        }

        public class GetBookingBase : IAsyncLifetime
        {
            protected Guid BookingId;
            protected BookingResponse Response;
            protected string UnsuccessfulMessage;

            protected DatabaseMockBuilder DbMockBuilder = new();
            protected Mock<ICarParkRepository> DatabaseMock;

            public async Task InitializeAsync()
            {
                DatabaseMock = DbMockBuilder.Build();
                await new BookingProcessor(DatabaseMock.Object)
                    .GetBooking(
                        BookingId,
                        onFound: booking => Response = booking,
                        onNotFound: message => UnsuccessfulMessage = message
                    );
            }

            public Task DisposeAsync()
                => Task.CompletedTask;
        }

        public class WhenBookingIsInDatabase : GetBookingBase
        {
            private readonly Booking _booking;
            private readonly ParkingSpace _parkingSpace;
            public WhenBookingIsInDatabase()
            {
                _parkingSpace = RandomData.ParkingSpace();
                _booking = RandomData.Booking(parkingSpaceId: _parkingSpace.ParkingSpaceId);
                BookingId = _booking.BookingId;
                DbMockBuilder.AddBookingGetById(_booking);
                DbMockBuilder.AddParkingSpaceGetById(_parkingSpace);
            }

            [Fact]
            public void BookingIsRetrievedFromDatabase()
                => DatabaseMock.Verify(d => d.GetBooking(BookingId), Times.Once);

            [Fact]
            public void BookingReturnedMatchesDatabase()
            {
                Response.BookingId.Should().Be(_booking.BookingId);
                Response.StartDate.Should().Be(_booking.StartDate);
                Response.EndDate.Should().Be(_booking.EndDate);
                Response.ParkingSpaceId.Should().Be(_parkingSpace.ParkingSpaceId);
                Response.BayIdentifier.Should().Be(_parkingSpace.BayIdentifier);
                Response.BayWidthInMetres.Should().Be(_parkingSpace.Width);
            }

            [Fact]
            public void UnsuccessfulMessageIsNotReturned()
                => UnsuccessfulMessage.Should().BeNull();
        }

        public class WhenBookingIsNotInDatabase : GetBookingBase
        {
            public WhenBookingIsNotInDatabase()
            {
                BookingId = Guid.NewGuid();
            }

            [Fact]
            public void BookingIsRetrievedFromDatabase()
                => DatabaseMock.Verify(d => d.GetBooking(BookingId), Times.Once);

            [Fact]
            public void UnsuccessfulMessageIsReturned() 
                => UnsuccessfulMessage.Should().Be(Constants.Messages.BookingNotFound);
        }
    }
}