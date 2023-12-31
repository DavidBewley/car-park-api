﻿using Bogus;
using Core.Models;
using Core.Models.Requests;

namespace SpecTests.Helpers
{
    public static class RandomData
    {
        public static int Number(int min, int max)
            => new Faker().Random.Number(min, max);

        public static ParkingSpace ParkingSpace() => new Faker<ParkingSpace>()
            .RuleFor(p => p.ParkingSpaceId, Guid.NewGuid())
            .RuleFor(p => p.Width, f => f.Random.Decimal(2.4m, 5m))
            .RuleFor(p => p.BayIdentifier, f => f.Random.AlphaNumeric(4));

        public static Booking Booking(Guid? bookingId = null, DateTime? startDate = null, DateTime? endDate = null, Guid? parkingSpaceId = null) => new Faker<Booking>()
            .RuleFor(b => b.BookingId, bookingId ?? Guid.NewGuid())
            .RuleFor(b => b.StartDate, f => startDate ?? f.Date.Past())
            .RuleFor(b => b.EndDate, f => endDate ?? f.Date.Future())
            .RuleFor(b => b.ParkingSpaceId, parkingSpaceId ?? Guid.NewGuid());

        public static BookingRequest BookingRequest(DateTime? startDate = null, DateTime? endDate = null) => new Faker<BookingRequest>()
                .RuleFor(b => b.StartDate, f => startDate ?? f.Date.Past())
                .RuleFor(b => b.EndDate, f => endDate ?? f.Date.Future());

        public static AvailabilityRequest AvailabilityRequest(DateTime? startDate = null, DateTime? endDate = null) => new Faker<AvailabilityRequest>()
            .RuleFor(b => b.StartDate, f => startDate ?? f.Date.Past())
            .RuleFor(b => b.EndDate, f => endDate ?? f.Date.Future());
    }
}
